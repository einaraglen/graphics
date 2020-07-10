using System;
using System.Collections.Generic;
using System.Text;

namespace gfx_app {

    class ChangeManager {

        private Profile profile;

        public ChangeManager(Profile profile) {
            this.profile = profile;
        }

        public Profile ChangeProfile(GAction action, int change) {

            switch (action) {
                case GAction.ChangeCount:
                this.profile.ShapeCount = change;

                break;

                case GAction.ChangeFrom:
                this.profile.StartSize = change;
                break;

                case GAction.ChangeTo:
                this.profile.EndSize = change;
                break;

                case GAction.ChangeMax:
                int _max = change;

                if (change % 2 > 0) {
                    _max++;
                }

                this.profile.MaxDelay = _max;
                break;

                case GAction.ChangeShape:
                this.profile.Shape = GetShape(change);
                break;

                case GAction.Wireframe:
                profile.WireOn = (change == 0);
                break;

                default:
                break;
            }

            return this.profile;

        }

        private GShape GetShape(int index) {

            GShape shape = GShape.Rectangle;

            switch(index) {
                case 0:
                shape = GShape.Rectangle;
                break;

                case 1:
                shape = GShape.Ellipse;
                break;

            }

            return shape;
        }


    }
}
