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
using System.Windows.Shapes;

namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for AddNewGMT.xaml
    /// </summary>
    public partial class AddNewGMT : Window
    {
        public event RoutedEventHandler ButtonClicked;
        public AddNewGMT()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ButtonClicked?.Invoke(this, new RoutedEventArgs());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
