using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace gfx_app {
    class TextManager {

        private readonly Label bottom_status;
        public TextManager(Label bottom_status) {

            this.bottom_status = bottom_status;

        }

        public void setStatus(String str) {
            this.bottom_status.Content = str;
        }

    }
}
