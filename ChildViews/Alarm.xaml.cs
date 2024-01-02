using FontAwesome.Sharp;
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
using System.Windows.Threading;
using System.Windows.Media.Animation;
using Notifications.Wpf;


namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarm : UserControl
    {
        public Alarm()
        {
            InitializeComponent();
        }



        private void AddNewAlarm_Click(object sender, RoutedEventArgs e)
        {
            

            Grid grid = new Grid();
            grid.Width = 266;
            grid.Height = 150;
            grid.Background = System.Windows.Media.Brushes.Beige;

            Grid childgrid = new Grid();
            childgrid.Margin = new Thickness(10, 10, 10, 10);
            childgrid.Background = System.Windows.Media.Brushes.White;

            Label hou = new Label();
            hou.Content = "00";
            hou.VerticalAlignment = VerticalAlignment.Top;
            hou.HorizontalAlignment = HorizontalAlignment.Left;
            hou.Margin = new Thickness(10, 10, 0, 0);
            hou.FontSize = 30;


            Label haicham = new Label();
            haicham.Content = ":";
            haicham.VerticalAlignment = VerticalAlignment.Top;
            haicham.HorizontalAlignment = HorizontalAlignment.Left;
            haicham.Margin = new Thickness(52, 10, 0, 0);
            haicham.FontSize = 30;


            Label min = new Label();
            min.Content = "00";
            min.VerticalAlignment = VerticalAlignment.Top;
            min.HorizontalAlignment = HorizontalAlignment.Left;
            min.Margin = new Thickness(69, 10, 0, 0);
            min.FontSize = 30;



            Label onoff = new Label();
            onoff.Content = "ON/OFF";
            onoff.VerticalAlignment = VerticalAlignment.Top;
            onoff.HorizontalAlignment = HorizontalAlignment.Left;
            onoff.Margin = new Thickness(155, 42, 0, 0);
            onoff.FontSize = 10;
            onoff.Height = 22;
            onoff.Width = 40;





            Label notee = new Label();
            notee.Margin = new Thickness(0, 65, 0, 27);
            notee.Content = "Note";
            notee.FontSize = 15;


            Label nhac = new Label();
            nhac.Content = "Ringtone: ";
            nhac.VerticalAlignment = VerticalAlignment.Top;
            nhac.HorizontalAlignment = HorizontalAlignment.Center;
            nhac.Margin = new Thickness(0, 94, 0, 0);
            nhac.FontSize = 15;
            nhac.Width = 246;
            nhac.Height = 36;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);


            bool isOn = false;

            Button mode = new Button();
            mode.Content = "OFF";
            mode.Background = System.Windows.Media.Brushes.Red;
            mode.Margin = new Thickness(195, 42, 15, 65);
            mode.Click += (s, ev) =>
            {
                isOn = !isOn;
                if (isOn)
                {
                    mode.Content = "ON";
                    mode.Background = System.Windows.Media.Brushes.Blue;
                    timer.Start();
                }
                else
                {
                    mode.Content = "OFF";
                    mode.Background = System.Windows.Media.Brushes.Red;
                    timer.Stop();
                }
            };

            void notification(string h)
            {



                var notificationManager = new NotificationManager();
                var notificationContent = new NotificationContent
                {
                    Title = "Notification",
                    Message = "Now is "+h ,
                    Type = NotificationType.Information,

                };

                notificationManager.Show(notificationContent, expirationTime: TimeSpan.FromSeconds(30)

        );
            }

            Button remove = new Button();
            remove.Margin = new Thickness(218, 0, 0, 103);
            Image imageRemove = new Image();
            imageRemove.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/remove.png"));
            remove.Content = imageRemove;
            remove.Click += Remove_Click;



          
            MediaPlayer mediaPlayer = new MediaPlayer();

            

            void RingRing( string filepath)
            {
                try
                {
                    mediaPlayer.Open(new Uri(filepath));
                    mediaPlayer.Play();

                }
                catch(Exception ex) { MessageBox.Show(ex.Message); }
            }


            string file="";

            





            Button edit = new Button();
            edit.Margin = new Thickness(190, 0, 28, 103);
            Image imageEdit = new Image();
            imageEdit.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/edit.png"));
            edit.Content = imageEdit;
            edit.Click += (s, ev) =>
            {
                AddAlarmWindow newAlarm = new AddAlarmWindow();
                newAlarm.SaveButtonClicked += (ggio, pphut, ttitle, ffn, ffp) =>
                {
                    int gio = Convert.ToInt32(ggio);
                    int phut = Convert.ToInt32(pphut);
                    notee.Content = ttitle;
                    nhac.Content = "Ringtone: " + ffn;
                    file = ffp;
                    if (gio >= 10)
                    {
                        hou.Content = gio;
                    }
                    else
                    {
                        hou.Content = "0" + gio;
                    }
                    if (phut >= 10)
                    {
                        min.Content = phut;
                    }
                    else
                    {
                        min.Content = "0" + phut;
                    }
                    mode.Content = "ON";
                    mode.Background = System.Windows.Media.Brushes.Blue;

                    timer.Start();
                };
                newAlarm.ShowDialog();

            };

            timer2.Tick += (s, ev) =>
            {
                int timing = 0;
                timing++;
                RingRing(file);
                if (timing == 30)
                {
                    timer2.Stop();
                }
            };

            timer.Tick += (s, ev) =>
            {
                if (mode.Content == "ON")
                {
                    DateTime d = DateTime.Now;
                    string a = d.ToString();
                    if (d.Hour == Convert.ToInt32(hou.Content) && d.Second < 1)
                    {
                        if (d.Minute == Convert.ToInt32(min.Content))
                        {
                            timer.Stop();
                            if(file!="")
                            {
                                RingRing(file);
                                notification(a);
                            }
                        }
                    }
                }
                if (mode.Content == "OFF")
                    timer.Stop();
            };
            Button turnoff = new Button();
            turnoff.HorizontalAlignment = HorizontalAlignment.Left;
            turnoff.VerticalAlignment = VerticalAlignment.Top;
            turnoff.Margin = new Thickness(162, 0, 0, 103);
            Image imageMute = new Image();
            imageMute.Source = new BitmapImage(new Uri("pack://application:,,,/DoAn_LT;component/Assets/Images/Add TImer/mute.png"));
            turnoff.Content = imageMute;
            turnoff.Click += (s, ev) =>
            {
                mediaPlayer.Stop();
                timer2.Stop();
            };


            

            childgrid.Children.Add(remove);
            childgrid.Children.Add(edit);
            childgrid.Children.Add(mode);
            childgrid.Children.Add(nhac);
            childgrid.Children.Add(notee);
            childgrid.Children.Add(onoff);
            childgrid.Children.Add(turnoff);
            childgrid.Children.Add(min);
            childgrid.Children.Add(haicham);
            childgrid.Children.Add(hou);

            grid.Children.Add(childgrid);

            ListAlarm.Children.Add(grid);





        }



        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button buttonRemove = (Button)sender;
            Grid childgrid = (Grid)buttonRemove.Parent;
            Grid grid = (Grid)childgrid.Parent;
            ListAlarm.Children.Remove(grid);
        }

  
    }
}
