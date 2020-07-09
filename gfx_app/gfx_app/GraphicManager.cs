using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gfx_app {

    class GraphicManager {

        private readonly Canvas main_canvas;
        private readonly TextManager txtManager;

        private double[] prev_x;
        private double[] prev_y;

        private int counter;

        private int max;
        private int offset;

        private HashSet<Shape> roster;
        private HashSet<Shape> wireframe;

        private int amount;
        private int from;
        private int to;

        public GraphicManager(Canvas main_canvas, TextManager textManager) {

            this.main_canvas = main_canvas;
            this.txtManager = textManager;

            this.roster = new HashSet<Shape>();
            this.wireframe = new HashSet<Shape>();

            this.amount = 5;
            this.from = 20;
            this.to = 200;
            this.max = 200;

            SetUpVariables();

        }

        public void ChangeMax(int newMax) {
            int _max = newMax;
            if(newMax % 2 > 0) {
                //cannot have odd nr, max/2 has to be even
                _max++;
            }

            this.max = _max;
            SetUpVariables();
            
        }

        public void ChangeTo(int to) {
            this.to = to;
            SetUpVariables();
        }

        public void ChangeFrom(int from) {
            this.from = from;
            SetUpVariables();
        }

        public void ChangeAmount(int amount) {
            if(amount > 300) {
                Console.WriteLine("overload");
            }

            else {
                this.amount = amount;
                SetUpVariables();
            }
            
        }

        private void SetUpVariables() {

            //has to be twice the size at all times
            this.prev_x = new double[this.max * 2];
            this.prev_y = new double[this.max * 2];

            this.roster.Clear();
            this.main_canvas.Children.Clear();
            
            this.offset = this.max / this.amount;


            this.counter = 0;

            this.roster = CreateEllipseStack(this.amount, this.from, this.to);
            this.wireframe = CreateWireFrame(this.amount);
        }

        private HashSet<Shape> CreateWireFrame(int amount) {

            HashSet<Shape> wires = new HashSet<Shape>();

            for(int i = 0; i < amount - 1; i++) {
                Line line = CreateLine(0, 0, 0, 0);

                wires.Add(line);

                this.main_canvas.Children.Add(line);
            }


            return wires;

        }

        private HashSet<Shape> CreateEllipseStack(int amount, int min, int max) {

            HashSet<Shape> ellipses = new HashSet<Shape>();

            int range = max - min;

            int step = range / amount;

            for(int i = 0; i < amount; i++) {

                Ellipse ellipse = CreateEllipse(min + (step * i), min + (step * i));

                ellipses.Add(ellipse);

                this.main_canvas.Children.Add(ellipse);

            }

            return ellipses;
        }

        public void UpdateCursonPoint(double x, double y) {

            Point[] points = new Point[this.roster.Count()];

            if (!(this.counter < (this.max * 2))) {
                this.counter = 0;
            }
            
            this.prev_x[this.counter] = x;
            this.prev_y[this.counter] = y;

            int n = 0;

            String update = null;

            foreach (Ellipse e in this.roster) {

                int index = 0;

                int currentOff = this.offset * n;

                if (this.counter < currentOff) {
                    index = (this.max * 2) - (currentOff - this.counter);

                } 
                
                else {
                    index = this.counter - currentOff;
                }

                update += "E" + (n + 1) + "(" + (index) + ") ";
                MoveEllipse(e, this.prev_x[index], this.prev_y[index]);

                points[n] = new Point(this.prev_x[index], this.prev_y[index]);
                
                n++;

            }

            int c = 0;

            foreach(Line line in this.wireframe) {

                MoveLine(line, points[c], points[c + 1]);

                c++;
            }

            Console.WriteLine(c);

            this.txtManager.setStatus(update);

            this.counter++;
        }

        private void MoveEllipse(Ellipse ellipse, double x, double y) {
            Canvas.SetLeft(ellipse, x - ellipse.ActualHeight/2);
            Canvas.SetTop(ellipse, y - ellipse.ActualWidth/2);
        }

        private void MoveLine(Line line, Point start, Point end) {
            line.X1 = start.X;
            line.Y1 = start.Y;

            line.X2 = end.X;
            line.Y2 = end.Y;
        }

        private Ellipse CreateEllipse(int height, int width) {
            Ellipse current = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color.FromArgb(255, 255, 255, 255);

            current.Opacity = 0.2;

            current.Height = height;
            current.Width = width;
            current.Fill = brush;
          
            return current;
        }

        private Line CreateLine(int x1, int y1, int x2, int y2) {
            Line line = new Line();

            line.X1 = x1;
            line.Y1 = y1;

            line.X2 = x2;
            line.Y2 = y2;

            line.StrokeThickness = 1;
            line.Stroke = System.Windows.Media.Brushes.LightSteelBlue;

            return line;
        }

    }

}
