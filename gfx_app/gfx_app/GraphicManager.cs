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

    public enum GShape {
        Rectangle,
        Ellipse
    }

    public enum GAction {
        ChangeShape,
        ChangeMax, 
        ChangeCount,
        ChangeFrom,
        ChangeTo,
        Wireframe
    }

    class GraphicManager {

        private readonly Canvas main_canvas;
        private readonly TextManager txtManager;

        private readonly Sequencer sequencer;

        private Profile profile;

        private double[] prev_x;
        private double[] prev_y;

        private int counter;

        private HashSet<Shape> roster;
        private HashSet<Shape> wireframe;

        private Point currentPoint;
        private Point[] oldPoints;

        public GraphicManager(Canvas main_canvas, TextManager textManager, Profile profile) {

            this.main_canvas = main_canvas;
            this.txtManager = textManager;

            this.roster = new HashSet<Shape>();
            this.wireframe = new HashSet<Shape>();

            this.sequencer = new Sequencer(this.main_canvas.ActualHeight, this.main_canvas.ActualWidth);

            this.profile = profile;
            this.currentPoint = new Point();
            this.oldPoints = new Point[1];

            SetUpVariables();

        }

        public void DoChange(GAction action, int change) {
            ChangeManager cm = new ChangeManager(this.profile);
            this.profile = cm.ChangeProfile(action, change);
            SetUpVariables();

        }

        private void SetUpVariables() {

            //has to be twice the size at all times
            this.prev_x = new double[this.profile.MaxDelay * 2];
            this.prev_y = new double[this.profile.MaxDelay * 2];

            this.roster.Clear();
            this.main_canvas.Children.Clear();
            

            this.counter = 0;

            this.roster = CreateStack(this.profile.Shape, this.profile.ShapeCount, this.profile.StartSize, this.profile.EndSize, currentPoint);
            this.wireframe = CreateWireFrame(this.profile.ShapeCount);

            this.UpdateCursonPoint(this.currentPoint.X, this.currentPoint.Y);
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

        private HashSet<Shape> CreateStack(GShape type, int amount, int min, int max, Point current) {

            HashSet<Shape> stack = new HashSet<Shape>();

            int range = max - min;

            int step = range / amount;

            for(int i = 0; i < amount; i++) {

                Shape shape = CreateShape(type, min + (step * i), min + (step * i), current);

                stack.Add(shape);

                this.main_canvas.Children.Add(shape);

            }

            return stack;
        }

        public void UpdateCursonPoint(double x, double y) {


            this.sequencer.Update(this.main_canvas.ActualHeight, this.main_canvas.ActualWidth);


            this.currentPoint = new Point(x, y);
            if(x < 0 || y < 0) {

                //center screen
                //x = this.main_canvas.ActualWidth / 2;
                //y = this.main_canvas.ActualHeight / 2;

                //follows sequenser
                x = 0 + this.sequencer.X;
                y = 0 + this.sequencer.Y;
                          
            }

            RendreCanvas(x, y);
        }

        private void RendreCanvas(double x, double y) {

            Point[] points = new Point[this.roster.Count()];

            //checks for change in shapecount, if not, keep old points
            if (this.oldPoints.Length == points.Length) {
                points = this.oldPoints;
            } 


            if (!(this.counter < (this.profile.MaxDelay * 2))) {
                this.counter = 0;
            }

            this.prev_x[this.counter] = x;
            this.prev_y[this.counter] = y;

            String update = null;

            for (int i = 0; i < this.roster.Count; i++) {

                int index = 0;
                int currentOff = (this.profile.MaxDelay / this.profile.ShapeCount) * i;

                if (this.counter < currentOff) {
                    index = (this.profile.MaxDelay * 2) - (currentOff - this.counter);

                } 
                
                else {
                    index = this.counter - currentOff;
                }

                points[i] = new Point(this.prev_x[index], this.prev_y[index]);
            }

            int n = 0;

            foreach (Shape s in this.roster) {

                MoveShape(s, points[n].X, points[n].Y);

                n++;
            }

            update += "Entities : " + this.roster.Count();

            if (this.profile.WireOn) {
                MoveLines(points);
                update += ", Lines : " + this.wireframe.Count();
            } 
            
            else {
                hideLines();
                update += " Lines : 0";
            }

            update += ", Following : x: " + x + ", y: " + (int)y;

            this.oldPoints = points;

            this.txtManager.setStatus(update);

            this.counter++;
        }

        private void hideLines() {
            foreach(Line line in this.wireframe) {
                MoveLine(line, new Point(0,0), new Point(0,0));
            }
        }

        private void MoveLines(Point[] points) {
            int c = 0;

            foreach (Line line in this.wireframe) {

                MoveLine(line, points[c], points[c + 1]);

                c++;
            }
        }

        private void MoveShape(Shape shape, double x, double y) {

            Canvas.SetLeft(shape, x - shape.ActualHeight/2);
            Canvas.SetTop(shape, y - shape.ActualWidth/2);
        }

        private void MoveLine(Line line, Point start, Point end) {
            //preventing wild threds on screen when big change

            double ls = Math.Abs((int)(start.X - end.X)^2);
            double rs = Math.Abs((int)(start.Y - end.Y)^2);


            double dist = Math.Sqrt(ls + rs);

            if(dist > 25) {
                line.X1 = 0;
                line.Y1 = 0;

                line.X2 = 0;
                line.Y2 = 0;
            }

            else {
                line.X1 = start.X;
                line.Y1 = start.Y;

                line.X2 = end.X;
                line.Y2 = end.Y;
            }

        }

        private Shape CreateShape(GShape type, int height, int width, Point current) {

            Shape shape = null;

            switch (type) {
                case GShape.Rectangle:
                shape = new Rectangle();
                break;

                case GShape.Ellipse:
                shape = new Ellipse();
                break;

                default:
                shape = new Rectangle();
                break;
            }

            SolidColorBrush brush = new SolidColorBrush();

            brush.Color = Color.FromArgb(255, 255, 255, 255);

            shape.Opacity = 0.2;

            shape.Height = height;
            shape.Width = width;
            shape.Fill = brush;

            return shape;
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
