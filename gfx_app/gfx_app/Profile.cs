using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;

namespace gfx_app {
    
    class Profile {

        private int shapeCount;
        private int startSize;
        private int endSize;
        private int maxDelay;
        private GShape shape;
        private bool wireOn;

        public Profile(int shapeCount, int startSize, int endSize, int maxDelay, GShape shape, bool wireOn) {

            this.shapeCount = shapeCount;
            this.startSize = startSize;
            this.endSize = endSize;
            this.maxDelay = maxDelay;
            this.shape = shape;
            this.wireOn = wireOn;

        }

        public int ShapeCount {
            get { return this.shapeCount; }
            set { this.shapeCount = value; }
        }

        public int StartSize {
            get { return this.startSize; }
            set { this.startSize = value; }
        }

        public int EndSize {
            get { return this.endSize; }
            set { this.endSize = value; }            
        }

        public int MaxDelay {
            get { return this.maxDelay; }
            set { this.maxDelay = value; }
        }

        public GShape Shape {
            get { return this.shape; }
            set { this.shape = value; }
        }

        public bool WireOn {
            get { return this.wireOn; }
            set { this.wireOn = value; }
        }
    }
}
