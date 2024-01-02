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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for AddAlarmWindow.xaml
    /// </summary>
    public partial class AddAlarmWindow : Window
    {
        public delegate void SaveButtonClickedEventHandler(string gio, string phut, string title, string fn, string fp);
        public event SaveButtonClickedEventHandler SaveButtonClicked;
        public AddAlarmWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }




        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void boxhou_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int number))
            {
                e.Handled = true;
            }
            else
            {
                TextBox box = (TextBox)sender;
                string txt = box.Text + e.Text;
                if (int.TryParse(txt, out int onumber) && onumber >= 0 && onumber <= 23)
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int hou = Convert.ToInt32(boxhou.Text);
            hou++;
            if (hou < 10)
            {
                boxhou.Text = 0 + hou.ToString();
            }
            else
            {
                boxhou.Text = hou.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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
                boxmin.Text = min.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int hou = Convert.ToInt32(boxhou.Text);
            if (hou == 0)
            {
                return;
            }
            if (hou > 0)
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
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


        string fullfilepath;
        string filename;

        private void nhacc_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {

                    fullfilepath = openFileDialog.FileName;
                    filename = System.IO.Path.GetFileNameWithoutExtension(fullfilepath);
                    nhacc.Text = filename;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void bt_save_Click(object sender, RoutedEventArgs e)
        {
            string ggio = boxhou.Text;
            string pphut = boxmin.Text;
            string ttitle = note.Text;
            nhacc.Text = filename;
            string ffn = nhacc.Text;
            string ffp = fullfilepath;
            SaveButtonClicked?.Invoke(ggio, pphut, ttitle, ffn, ffp);
            Close();
        }
    }
}
