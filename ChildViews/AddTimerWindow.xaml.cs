using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AddTimerWindow.xaml
    /// </summary>
    public partial class AddTimerWindow : Window
    {
        public delegate void SaveButtonClickedEventHandler(string gio, string phut, string giay, string title);
        public event SaveButtonClickedEventHandler SaveButtonClicked;
        public AddTimerWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ggio=boxhou.Text;
            string pphut=boxmin.Text;
            string ggiay = boxsec.Text;
            string ttitle = Title.Text;
            SaveButtonClicked?.Invoke(ggio,pphut, ggiay, ttitle);
            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int hou = Convert.ToInt32(boxhou.Text);
            hou++;
            if(hou<10)
            {
                boxhou.Text = 0 + hou.ToString();
            }
            else
            {
                boxhou.Text=hou.ToString();
            }    
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int min = Convert.ToInt32(boxmin.Text);
            if (min == 59)
            {
                min = -1;
            }
            if (min < 59)
            {
                min++;
            }
            if (min < 10)
            {
                boxmin.Text = 0 + min.ToString();
            }
            else
            {
                boxmin.Text=min.ToString();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int sec = Convert.ToInt32(boxsec.Text);
            if (sec == 59)
            {
                sec = -1;
            }
            if (sec < 59)
            {
                sec++;
            }
            if (sec < 10)
            {
                boxsec.Text = 0 + sec.ToString();
            }
            else
            {
                boxsec.Text = sec.ToString();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            int hou = Convert.ToInt32(boxhou.Text);
            if (hou == 0)
            {
                return;
            }
            if(hou>0)
            {
                hou--;
                if (hou < 10)
                {
                    boxhou.Text = 0 + hou.ToString();
                }
                else
                {
                    boxhou.Text = hou.ToString();
                }
            }
           
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            int min = Convert.ToInt32(boxmin.Text);
            if (min == 0)
            {
                min = 60;
            }
            if (min > 0)
            {
                min--;
            }
            if (min < 10)
            {
                boxmin.Text = 0 + min.ToString();
            }
            else
            {
                boxmin.Text = min.ToString();
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            int sec = Convert.ToInt32(boxsec.Text);
            if (sec == 0)
            {
                sec = 60;
            }
            if (sec >0)
            {
                sec--;
            }
            if (sec < 10)
            {
                boxsec.Text = 0 + sec.ToString();
            }
            else
            {
                boxsec.Text = sec.ToString();
            }
        }

        private void boxhou_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!int.TryParse(e.Text, out int number))
            {
                e.Handled = true;
            }
            else
            {
                TextBox box=(TextBox)sender;
                string txt=box.Text+e.Text;
                if(int.TryParse(txt, out int onumber)&&onumber>=0)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void boxmin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number))
            {
                e.Handled = true;
            }
            else
            {
                TextBox box = (TextBox)sender;
                string txt = box.Text + e.Text;
                if (int.TryParse(txt, out int onumber) && onumber >=0 && onumber<=59)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void boxsec_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number))
            {
                e.Handled = true;
            }
            else
            {
                TextBox box = (TextBox)sender;
                string txt = box.Text + e.Text;
                if (int.TryParse(txt, out int onumber) && onumber >= 0 && onumber <= 59)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }
    }
}
