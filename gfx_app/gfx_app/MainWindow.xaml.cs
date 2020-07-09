using System;
using System.Collections.Generic;
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

        private int i = 0;

        public MainWindow() {

            InitializeComponent();

            this.graphicManager = new GraphicManager(main_canvas);

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
    }

    
}
