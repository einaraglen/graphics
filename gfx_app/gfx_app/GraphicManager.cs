using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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

        private int amount;
        private int from;
        private int to;


        public GraphicManager(Canvas main_canvas, TextManager textManager) {

            this.main_canvas = main_canvas;
            this.txtManager = textManager;

            this.roster = new HashSet<Shape>();

            this.amount = 5;
            this.from = 20;
            this.to = 200;

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
            this.amount = amount;
            SetUpVariables();
        }

        private void SetUpVariables() {

            this.roster.Clear();
            this.main_canvas.Children.Clear();
            
            this.max = 400;
            this.offset = max / this.amount;

            this.prev_x = new double[this.max * 2];
            this.prev_y = new double[this.max * 2];

            this.counter = 0;

            this.roster = CreateEllipseStack(this.amount, this.from, this.to);
        }

        private HashSet<Shape> CreateEllipseStack(int amount, int min, int max) {

            HashSet<Shape> ellipses = new HashSet<Shape>();

            int range = max - min;

            int step = range / amount;

            for(int i = 0; i < amount; i++) {

                Ellipse ellipse = CreateEllipse(min + (step * i), min + (step * i), 10, 10);

                ellipses.Add(ellipse);

                this.main_canvas.Children.Add(ellipse);

            }

            return ellipses;
        }

        public void UpdateCursonPoint(double x, double y) {

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

                n++;

            }

            this.txtManager.setStatus(update);

            this.counter++;
        }

        private void MoveEllipse(Ellipse ellipse, double x, double y) {
            Canvas.SetLeft(ellipse, x - ellipse.ActualHeight/2);
            Canvas.SetTop(ellipse, y - ellipse.ActualWidth/2);
        }

        private Ellipse CreateEllipse(int height, int width, double x, double y) {
            Ellipse current = new Ellipse();
            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color.FromArgb(255, 255, 255, 255);

            current.Opacity = 0.2;

            current.Height = height;
            current.Width = width;
            current.Fill = brush;
            /*
            Canvas.SetLeft(current, x - height/2);
            Canvas.SetTop(current, y - width/2);
            */
            return current;
        }

    }

}
