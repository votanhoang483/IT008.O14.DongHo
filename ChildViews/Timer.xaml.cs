using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Notifications.Wpf;


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

        int NumOfTimer = 1;

        private void AddNewTimer_Click(object sender, RoutedEventArgs e)
        {

            Grid grid = new Grid();
            grid.Height = 150;
            grid.Width = 266;
            grid.Background = System.Windows.Media.Brushes.Honeydew;
            Grid childgrid = new Grid();
            childgrid.Background = System.Windows.Media.Brushes.White;
            childgrid.Margin = new Thickness(10, 10, 10, 10);




            Button remove = new Button();
            remove.HorizontalAlignment = HorizontalAlignment.Left;
            remove.VerticalAlignment = VerticalAlignment.Top;
            remove.Height = 30;
            remove.Width = 30;
            remove.Margin = new Thickness(210, 0, 0, 0);
            Image imageRemove = new Image();
            imageRemove.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/remove.png"));
            remove.Content = imageRemove;
            remove.Click += Remove_Click;


            


            Label TimerName = new Label();
            TimerName.HorizontalAlignment = HorizontalAlignment.Left;
            TimerName.VerticalAlignment = VerticalAlignment.Top;
            TimerName.Width = 120;
            TimerName.Height = 35;
            TimerName.Margin = new Thickness(0, 3, 0, 0);
            TimerName.Content = "Timer " + NumOfTimer;
            TimerName.FontSize = 15;
            NumOfTimer++;


            Label hou = new Label();
            hou.Margin = new Thickness(51, 0, 0, 0);
            hou.Content = "00";
            hou.VerticalAlignment = VerticalAlignment.Center;
            hou.HorizontalAlignment = HorizontalAlignment.Left;
            hou.Width = 37;
            hou.FontSize = 25;


            Label min = new Label();
            min.Margin = new Thickness(108, 0, 0, 0);
            min.Content = "00";
            min.VerticalAlignment = VerticalAlignment.Center;
            min.HorizontalAlignment = HorizontalAlignment.Left;
            min.Width = 37;
            min.FontSize = 25;


            Label sec = new Label();
            sec.Margin = new Thickness(158, 0, 0, 0);
            sec.Content = "00";
            sec.VerticalAlignment = VerticalAlignment.Center;
            sec.HorizontalAlignment = HorizontalAlignment.Left;
            sec.Width = 37;
            sec.FontSize = 25;


            Label label = new Label();
            label.Margin = new Thickness(93, 41, 0, 0);
            label.Content = ":";
            label.VerticalAlignment = VerticalAlignment.Top;
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.FontSize = 25;
            label.Width = 15;


            Label label1 = new Label();
            label1.Margin = new Thickness(142, 41, 0, 0);
            label1.Content = ":";
            label1.VerticalAlignment = VerticalAlignment.Top;
            label1.HorizontalAlignment = HorizontalAlignment.Left;
            label1.FontSize = 25;
            label1.Width = 15;

            int h = -1, p = -1, g = -1;

            DispatcherTimer playtimer = new DispatcherTimer();
            playtimer.Interval = TimeSpan.FromSeconds(1);
           

            Button pause = new Button();
            pause.Margin = new Thickness(65, 94, 157, 15);
            Image imagePause = new Image();
            imagePause.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/pause.png"));
            pause.Content = imagePause;
            pause.Click += (s, ev) =>
            {
                playtimer.Stop();
            };


            void notification()
            {


               
                var notificationManager = new NotificationManager();
                var notificationContent = new NotificationContent
                {
                    Title = "Notification",

                    Message = "Time Out",
                    Type = NotificationType.Information,

                };

                notificationManager.Show(notificationContent, expirationTime: TimeSpan.FromSeconds(30)

        );
            }

            

            Button play = new Button();
            play.Margin = new Thickness(111, 94, 111, 15);
            Image imagePlay = new Image();
            imagePlay.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/Play1.png"));
            play.Content = imagePlay;
            play.Click += (s, ev) =>
            {
                h = Convert.ToInt32(hou.Content);
                p = Convert.ToInt32(min.Content);
                g = Convert.ToInt32(sec.Content);
                playtimer.Start();
                if (g >= 0)
                {
                    if (g >= 10)
                    {
                        sec.Content = g.ToString();
                    }
                    else
                    {
                        sec.Content = "0" + g.ToString();
                    }
                }
                if (p >= 0)
                {
                    if (p >= 10)
                    {
                        min.Content = p.ToString();
                    }
                    else
                    {
                        min.Content = "0" + p.ToString();
                    }
                }
                if (h >= 0)
                {
                    if (h >= 10)
                    {
                        hou.Content = h.ToString();
                    }
                    else
                    {
                        hou.Content = "0" + h.ToString();
                    }
                }

            };
            playtimer.Tick += (s, ev) =>
            {
                if(g>0)
                {
                    g--;
                }
                if(g==0&&p>0)
                {
                    g = 59;
                    p--;
                }
                if(h>0&&p==0&&g==0)
                {
                    h--;
                    p = 59;
                    g = 59;
                }
                if(h==0&&g==0&&p==0)
                {

                    playtimer.Stop();
                    notification();
                    
                }
                if (g >= 0)
                {
                    if (g >= 10)
                    {
                        sec.Content = g.ToString();
                    }
                    else
                    {
                        sec.Content = "0" + g.ToString();
                    }
                }
                if (p >= 0)
                {
                    if (p >= 10)
                    {
                        min.Content = p.ToString();
                    }
                    else
                    {
                        min.Content = "0" + p.ToString();
                    }
                }
                if (h >= 0)
                {
                    if (h >= 10)
                    {
                        hou.Content = h.ToString();
                    }
                    else
                    {
                        hou.Content = "0" + h.ToString();
                    }
                }
            };




           

            string firsthou = "00";
            string firstmin = "00";
            string firstsec = "00";



            Button edit = new Button();
            edit.HorizontalAlignment = HorizontalAlignment.Left;
            edit.VerticalAlignment = VerticalAlignment.Top;
            edit.Height = 30;
            edit.Width = 30;
            edit.Margin = new Thickness(180, 0, 0, 0);
            Image imageEdit = new Image();
            imageEdit.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/edit.png"));
            edit.Content = imageEdit;
            edit.Click += (s, ev) =>
            {
                AddTimerWindow newTimer = new AddTimerWindow();
                newTimer.SaveButtonClicked += (ggio, pphut, ggiay, ttitle) =>
                {
                    int gio= Convert.ToInt32(ggio);
                    int phut=Convert.ToInt32(pphut);
                    int giay=Convert.ToInt32(ggiay);
                    if(gio<10)
                    {
                        hou.Content = "0" + gio.ToString();
                        firsthou = "0" + gio.ToString();
                        
                    }
                    if(gio>=10)
                    {
                        hou.Content = gio.ToString();
                        firsthou = gio.ToString();
                    }
                    if (phut < 10)
                    {
                        min.Content = "0" + phut.ToString();
                        firstmin = "0" + phut.ToString();
                    }
                    if (phut >= 10)
                    {
                        min.Content = phut.ToString();
                        firstmin = phut.ToString();
                    }
                    if (giay < 10)
                    {
                        sec.Content = "0" + giay.ToString();
                        firstsec = "0" + giay.ToString();
                    }
                    if (giay >= 10)
                    {
                        sec.Content = giay.ToString();
                        firstsec = giay.ToString();
                    }
                      
                    TimerName.Content = ttitle;

                };
                newTimer.ShowDialog();
            };


            Button reset = new Button();
            reset.Margin = new Thickness(159, 94, 63, 15);
            Image imageReset = new Image();
            imageReset.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/Reset.png"));
            reset.Content = imageReset;
            reset.Click += (s, ev) =>
            {
                playtimer.Stop();
                hou.Content = firsthou;
                min.Content = firstmin;
                sec.Content = firstsec;
            };


            childgrid.Children.Add(hou);
            childgrid.Children.Add(min);
            childgrid.Children.Add(sec);
            childgrid.Children.Add(label);
            childgrid.Children.Add(label1);
            childgrid.Children.Add(edit);
            childgrid.Children.Add(remove);
            childgrid.Children.Add(TimerName);
            childgrid.Children.Add(reset);
            childgrid.Children.Add(play);
            childgrid.Children.Add(pause);
            grid.Children.Add(childgrid);
            ListTimer.Children.Add(grid);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button buttonRemove = (Button)sender;
            Grid childgrid=(Grid)buttonRemove.Parent;
            Grid grid = (Grid)childgrid.Parent;
            ListTimer.Children.Remove(grid);
        }
    }
}
