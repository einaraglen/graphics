using System;
using System.Collections.Generic;
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

            this.graphicManager = new GraphicManager(main_canvas, this.txtManager);


        }

        private void main_canvas_MouseMove(object sender, MouseEventArgs e) {
            
            this.pos = Mouse.GetPosition(main_canvas);
            this.graphicManager.UpdateCursonPoint(this.pos.X, this.pos.Y);
            
        }

        private void amount_TextChanged(object sender, TextChangedEventArgs e) {

            if (System.Text.RegularExpressions.Regex.IsMatch(amountField.Text, "[^0-9]")) {
                amountField.Text = "";
            } else {

                try {

                    int change = int.Parse(amountField.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
                    if(change != 0) {
                        this.graphicManager.ChangeAmount(change);
                    }


                } catch (Exception exp) {
                    Console.WriteLine(exp.Message);
                }

            }

        }

        private void from_TextChanged(object sender, TextChangedEventArgs e) {

            if (System.Text.RegularExpressions.Regex.IsMatch(from.Text, "[^0-9]")) {
                from.Text = "";
            } else {

                try {

                    int change = int.Parse(from.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);


                    this.graphicManager.ChangeFrom(change);

                } catch (Exception exp) {
                    Console.WriteLine(exp.Message);
                }

            }

        }

        private void to_TextChanged(object sender, TextChangedEventArgs e) {

            if (System.Text.RegularExpressions.Regex.IsMatch(to.Text, "[^0-9]")) {
                to.Text = "";
            } else {

                try {

                    int change = int.Parse(to.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);


                    this.graphicManager.ChangeTo(change);

                } catch (Exception exp) {
                    Console.WriteLine(exp.Message);
                }

            }
        }

        private void max_TextChanged(object sender, TextChangedEventArgs e) {

            if (System.Text.RegularExpressions.Regex.IsMatch(max.Text, "[^0-9]")) {
                max.Text = "";
            } else {

                try {

                    int change = int.Parse(max.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);


                    this.graphicManager.ChangeMax(change);

                } catch (Exception exp) {
                    Console.WriteLine(exp.Message);
                }

            }
        }

    }

    
}
