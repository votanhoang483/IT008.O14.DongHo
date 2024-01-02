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
using System.Timers;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using Notifications.Wpf;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Threading;


namespace DoAn_LT.ChildViews
{
    /// <summarya>
    /// Interaction logic for PomodoroClock.xaml
    /// </summary>

    public partial class PomodoroClock : UserControl
    {
        string filename;
        string fullfilepath;
        int flag = 1;
        string strcon = @"Data Source=LAPTOP-CL3NH660;Initial Catalog=clock;Integrated Security=True";
        SqlConnection sqlcon = null;
        int minute_pomodoro = 25;
        int second_pomodoro = 0;
        int second_short = 0;
        int second_long = 0;
        int minute_long = 5;
        int minute_short = 10;
        static MediaPlayer mediaPlayer = new MediaPlayer();

        private void Music_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd1 = new SqlCommand();
            SqlCommand sqlcmd2 = new SqlCommand();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fullfilepath = openFileDialog.FileName;
                filename = System.IO.Path.GetFileNameWithoutExtension(fullfilepath);
                mediaPlayer.Open(new Uri(fullfilepath));
                sqlcmd1.CommandType = CommandType.Text;
                sqlcmd1.CommandText = "update pomodoro_clock set name=N'" + filename + "'";
                sqlcmd1.Connection = sqlcon;
                sqlcmd1.ExecuteNonQuery();
                sqlcmd2.CommandType = CommandType.Text;
                sqlcmd2.CommandText = "update pomodoro_clock set link=N'" + fullfilepath + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();


            }
            StopSong.Content = filename;
        }



        public PomodoroClock()
        {
            InitializeComponent();

            if (sqlcon == null)
            {
                sqlcon = new SqlConnection(strcon);
            }
            if (sqlcon.State == ConnectionState.Closed)
            {
                sqlcon.Open();
            }
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 1000;
            SqlCommand sqlcmd1 = new SqlCommand();

            try
            {
                sqlcmd1.CommandType = CommandType.Text;
                sqlcmd1.CommandText = "select * from pomodoro_clock";
                sqlcmd1.Connection = sqlcon;
                SqlDataReader reader = sqlcmd1.ExecuteReader();

                if (reader.Read())
                {
                    int a = reader.GetInt32(0);
                    int b = reader.GetInt32(1);
                    int c = reader.GetInt32(2);
                    int d = reader.GetInt32(3);
                    int e = reader.GetInt32(4);
                    int f = reader.GetInt32(5);
                    minute_pomodoro = a;
                    minute_long = b;
                    minute_short = c;
                    second_pomodoro = d;
                    second_long = f;
                    second_short = e;
                    string g = reader.GetString(6);
                    string h = reader.GetString(7);
                    filename = g;
                    fullfilepath = h;


                }
                reader.Close();
                StopSong.Content = filename;


            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }


            label1.Text = output(second_pomodoro);
            label2.Text = output(minute_pomodoro);

        }

        private System.Windows.Forms.Timer aTimer;

        private void Start_Click(object sender, EventArgs e)
        {

            aTimer.Start();

        }

        private void Stop_Click(object sender, EventArgs e)
        {
            aTimer.Stop();

        }

        private void aTimer_Tick(object sender, EventArgs e)
        {

            if (flag == 1 && minute_pomodoro > 0 && second_pomodoro == 0)
            {
                minute_pomodoro--;
                label2.Text = output(minute_pomodoro);
                second_pomodoro = 60;
            }
            if (flag == 2 && minute_short > 0 && second_short == 0)
            {
                minute_short--;
                label2.Text = output(minute_short);
                second_short = 60;
            }
            if (flag == 3 && minute_long > 0 && second_long == 0)
            {
                minute_long--;
                label2.Text = output(minute_long);
                second_long = 60;
            }


            if (second_pomodoro > 0 && flag == 1)

            {
                second_pomodoro--;
                label1.Text = output(second_pomodoro);
            }
            if (second_short > 0 && flag == 2)
            {
                second_short--;
                label1.Text = output(second_short);
            }
            if (second_long > 0 && flag == 3)
            {
                second_long--;
                label1.Text = output(second_long);
            }
            if (minute_pomodoro == 0 && second_pomodoro == 0 && flag == 1)
            {

                notification();
                mediaPlayer.Play();

                aTimer.Stop();
            }
            if (minute_short == 0 && second_short == 0 && flag == 2)
            {
                notification();

                aTimer.Stop();
            }
            if (minute_long == 0 && second_long == 0 && flag == 3)
            {
                notification();

                aTimer.Stop();
            }

        }
        private void label2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public string output(int x)
        {
            if (x >= 10)
                return x.ToString();
            else
                return "0" + x.ToString();
        }
        private void Pomodoro_Click(object sender, RoutedEventArgs e)
        {
            aTimer.Stop();
            flag = 1;
            label1.Text = output(second_pomodoro);
            label2.Text = output(minute_pomodoro);
        }
        private void Short_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            flag = 2;
            label1.Text = output(second_short);
            label2.Text = output(minute_short);
        }
        private void Long_Click(object sender, EventArgs e)
        {

            aTimer.Stop();
            flag = 3;
            label1.Text = output(second_long);
            label2.Text = output(minute_long);
        }
        string noti;

        private void hour_change(object sender, EventArgs e)
        {
            SqlCommand sqlcmd2 = new SqlCommand();
            sqlcmd2.CommandType = CommandType.Text;
            if (flag == 1 && label2.Text != "")
            {
                minute_pomodoro = int.Parse(label2.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set pomodoro='" + minute_pomodoro + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
            if (flag == 2 && label2.Text != "")
            {
                minute_short = int.Parse(label2.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set shortbreak='" + minute_short + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
            if (flag == 3 && label2.Text != "")
            {
                minute_long = int.Parse(label2.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set longbreak='" + minute_long + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
        }
        private void second_change(object sender, EventArgs e)
        {
            SqlCommand sqlcmd2 = new SqlCommand();
            sqlcmd2.CommandType = CommandType.Text;
            if (flag == 1 && label1.Text != "")
            {
                second_pomodoro = int.Parse(label1.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set second_po='" + second_pomodoro + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
            if (flag == 2 && label1.Text != "")
            {
                second_short = int.Parse(label1.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set second_short='" + second_short + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
            if (flag == 3 && label1.Text != "")
            {
                second_long = int.Parse(label1.Text);
                sqlcmd2.CommandText = "update pomodoro_clock set second_long='" + second_long + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
            }
        }
        private void Up_Click(object sender, EventArgs e)
        {

            SqlCommand sqlcmd2 = new SqlCommand();
            sqlcmd2.CommandType = CommandType.Text;


            if (flag == 1)
            {

                minute_pomodoro += 1;
                sqlcmd2.CommandText = "update pomodoro_clock set pomodoro='" + minute_pomodoro + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
                label2.Text = output(minute_pomodoro);
            }
            if (flag == 2)
            {
                minute_short += 1;

                sqlcmd2.CommandText = "update pomodoro_clock set shortbreak='" + minute_short + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();


                label2.Text = output(minute_short);

            }
            if (flag == 3)
            {
                minute_long += 1;
                sqlcmd2.CommandText = "update pomodoro_clock set longbreak='" + minute_long + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
                label2.Text = output(minute_long);
            }
        }
        private void Down_Click(object sender, EventArgs e)
        {
            SqlCommand sqlcmd2 = new SqlCommand();

            if (flag == 1 && minute_pomodoro > 0)
            {

                minute_pomodoro -= 1;
                sqlcmd2.CommandText = "update pomodoro_clock set pomodoro='" + minute_pomodoro + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
                label2.Text = output(minute_pomodoro);
            }
            if (flag == 2 && minute_short > 0)
            {
                minute_short -= 1;
                sqlcmd2.CommandText = "update pomodoro_clock set shortbreak='" + minute_short + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
                label2.Text = output(minute_short);

            }
            if (flag == 3 && minute_long > 0)
            {
                minute_long -= 1;
                sqlcmd2.CommandText = "update pomodoro_clock set longbreak='" + minute_long + "'";
                sqlcmd2.Connection = sqlcon;
                sqlcmd2.ExecuteNonQuery();
                label2.Text = output(minute_long);
            }
        }
        public class MyNotificationContent : NotificationContent
        {
            public event Action Closed;

            public void Close()
            {
                Closed?.Invoke();
            }
        }
        private void notification()
        {


            if (flag == 1)
            {
                noti = "Time Out Pomodoro";
            }
            if (flag == 2) { noti = "Time Out Short Break"; }
            if (flag == 3) { noti = "Time Out Long Break"; }

            var myNotificationContent = new MyNotificationContent
            {
                Title = "Notification",
                Message = noti,
                Type = NotificationType.Information,

            };

            var notificationManager = new NotificationManager();

            // Subscribe to the notification dismissed event

            myNotificationContent.Closed += () =>
            {
                try
                {
                    mediaPlayer.Stop();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            notificationManager.Show(myNotificationContent, expirationTime: TimeSpan.FromSeconds(30));


        }

        private void Stop_Song_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }
    }

}
