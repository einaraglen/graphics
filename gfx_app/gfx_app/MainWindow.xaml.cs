using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gfx_app {

    public partial class MainWindow : Window {

        private readonly GraphicManager graphicManager;
        private readonly TextManager txtManager;

        private Point pos; 

        public MainWindow() {

            InitializeComponent();


            this.txtManager = new TextManager(bottom_status);
            Profile profile = new Profile(15, 10, 90, 70, GShape.Rectangle, true);

            this.graphicManager = new GraphicManager(main_canvas, this.txtManager, profile);

            SetStart(profile);

            //Renderer
            CompositionTarget.Rendering += DoUpdates;
        }

        private void SetStart(Profile profile) {
            amountField.Text = profile.ShapeCount.ToString();
            from.Text = profile.StartSize.ToString();
            to.Text = profile.EndSize.ToString();
            max.Text = profile.MaxDelay.ToString();

            types.SelectedIndex = 1;

            if(profile.WireOn) {
                wireframe.SelectedIndex = 0;
            }

            else {
                wireframe.SelectedIndex = 1;
            }
        }

        private void DoUpdates(object sender, EventArgs e) {

            this.pos = Mouse.GetPosition(main_canvas);
            this.graphicManager.UpdateCursonPoint(this.pos.X, this.pos.Y);

        }

        private int GetIntFrom(TextBox sender) {

            int change = 1;

            if (System.Text.RegularExpressions.Regex.IsMatch(sender.Text, "[^0-9]")) {
                sender.Text = "";
            } else {

                try {

                    change = int.Parse(sender.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                    if (change == 0) {
                        change = 1;
                    }


                } catch (Exception exp) {
                    String err = exp.Message;
                    sender.Text = "";
                }

            }

            return change;

        }

        private void amount_TextChanged(object sender, TextChangedEventArgs e) {

            int change = GetIntFrom(amountField);

            this.graphicManager.DoChange(GAction.ChangeCount, change);

        }

        private void from_TextChanged(object sender, TextChangedEventArgs e) {

            int change = GetIntFrom(from);

            this.graphicManager.DoChange(GAction.ChangeFrom, change);

        }

        private void to_TextChanged(object sender, TextChangedEventArgs e) {

            int change = GetIntFrom(to);

            this.graphicManager.DoChange(GAction.ChangeTo, change);
        }

        private void max_TextChanged(object sender, TextChangedEventArgs e) {

            int change = GetIntFrom(max);

            this.graphicManager.DoChange(GAction.ChangeMax, change);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            this.graphicManager.DoChange(GAction.ChangeShape, types.SelectedIndex);
        }

        private void wireframe_SelectionChanged(object sender, SelectionChangedEventArgs e) {

            this.graphicManager.DoChange(GAction.Wireframe, wireframe.SelectedIndex);
        }
    }

    
}
