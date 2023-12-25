using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        public Timer()
        {
            InitializeComponent();
        }

        string hour, min, sec;
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var AddWindow = new AddTimerWindow();
            AddWindow.ButtonClicked += AddWindow_ButtonClicked;
            AddWindow.ShowDialog();
        }

        private void AddWindow_ButtonClicked(object sender, RoutedEventArgs e)
        {
           var grid=new Grid();
            grid.Background = System.Windows.Media.Brushes.LightGreen;
            ListTimer.Children.Add(grid);
        }
    }
}
