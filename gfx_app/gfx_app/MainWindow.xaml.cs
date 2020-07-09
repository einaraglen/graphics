using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace gfx_app {

    public partial class MainWindow : Window {

        private readonly GraphicManager graphicManager;
        private readonly TextManager txtManager;

        private int i = 0;

        public MainWindow() {

            InitializeComponent();

            this.txtManager = new TextManager(bottom_status);

            this.graphicManager = new GraphicManager(main_canvas, this.txtManager);

        }

        private void _timer_Tick(object sender, EventArgs e) {
            // called every tick

            Point position = Mouse.GetPosition(main_canvas);
            this.graphicManager.UpdateCursonPoint(position.X, position.Y);

            Console.WriteLine(i);
            this.i++;
        }
    

        private void main_canvas_MouseMove(object sender, MouseEventArgs e) {
            
            Point position = Mouse.GetPosition(main_canvas);
            this.graphicManager.UpdateCursonPoint(position.X, position.Y);
            
        }

        private void Load_Timer(object sender, RoutedEventArgs e) {
            /*var timer = new DispatcherTimer(); // creating a new timer
            timer.Interval = TimeSpan.FromMilliseconds(1); // this timer will trigger every 10 milliseconds
            timer.Start(); // starting the timer
            timer.Tick += _timer_Tick; // with each tick it will trigger this function*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {

            if(System.Text.RegularExpressions.Regex.IsMatch(amountField.Text, "[^0-9]")) {
                amountField.Text = "";
            }

            else {

                try {

                    int change = int.Parse(amountField.Text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);


                    this.graphicManager.ChangeAmount(change);

                } catch (Exception exp) {
                    //Console.WriteLine(exp.Message);
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
                    //Console.WriteLine(exp.Message);
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
                    //Console.WriteLine(exp.Message);
                }

            }
        }
    }

    
}
