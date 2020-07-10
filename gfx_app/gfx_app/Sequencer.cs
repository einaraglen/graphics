using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace gfx_app {
    class Sequencer {

        private int counter;
        private double x;
        private double y;


        private double height;
        private double width;

        public Sequencer(double height, double width) {
            this.counter = 0;
            this.x = 0;

            this.height = height;
            this.width = width;

        }

        public double X {
            get { return this.x; }
        }

        public double Y {
            get { return this.y; }
        }

        public void Update(double height, double width) {

            if((this.counter % 2) == 0) {
                //do things here
                DoThis(height, width);
            }

            this.counter++;
        }

        private void DoThis(double height, double width) {

            if(this.x > width) {
                this.x = 0;
            }

            this.y = ((Math.Cos(this.x/10) * 100) + height / 2);

            this.x += 1;
        }

    }
}
