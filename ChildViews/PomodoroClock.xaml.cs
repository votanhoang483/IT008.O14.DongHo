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


namespace DoAn_LT.ChildViews
{
    public partial class PomodoroClock : UserControl
    {
        int minute_pomodoro = 25;
        int second_pomodoro = 0;
        public PomodoroClock()
        {
            InitializeComponent();
            aTimer = new System.Windows.Forms.Timer();
            aTimer.Tick += new EventHandler(aTimer_Tick);
            aTimer.Interval = 1000;
            label1.Content = output(second_pomodoro);
            label2.Content = output(minute_pomodoro);
        }
        private System.Windows.Forms.Timer aTimer;
        private void Start_Click(object sender, EventArgs e)
        {

            aTimer.Start();
            //label1.Text  = output(second_pomodoro);
            //label2.Text = output(minute_pomodoro);
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
        }
        private async void aTimer_Tick(object sender, EventArgs e)
        {
            if ((second_pomodoro == 0) && (minute_pomodoro > 0))
            {
                minute_pomodoro--;
                label2.Content = output(minute_pomodoro);
                second_pomodoro = 60;
            }
            second_pomodoro--;

            if ((minute_pomodoro == 0) && (second_pomodoro == 0))
            {
                aTimer.Stop();
                PlayAlertSound();
                await ShowAlertMessageBox();
                label1.Content = output(second_pomodoro);
            }
            else
            {
                label1.Content = output(second_pomodoro);
            }
        }

        private void PlayAlertSound()
        {
            string alertSoundFilePath = "alert_sound.mp3";
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(alertSoundFilePath, UriKind.RelativeOrAbsolute));
            player.Play();
        }

        private async Task ShowAlertMessageBox()
        {
            var tcs = new TaskCompletionSource<bool>();
            MessageBox.Show("Hết thời gian!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            tcs.SetResult(true);
            await tcs.Task;
        }
        public string output(int x)
        {
            if (x >= 10)
                return x.ToString();
            else
                return "0" + x.ToString();
        }
        private void Pomodoro_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            second_pomodoro = 0;
            minute_pomodoro = 25;
            label1.Content = output(second_pomodoro);
            label2.Content = output(minute_pomodoro);
        }
        private void Short_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            second_pomodoro = 0;
            minute_pomodoro = 5;

            label1.Content = output(second_pomodoro);
            label2.Content = output(minute_pomodoro);
        }
        private void Long_Click(object sender, EventArgs e)
        {
            aTimer.Stop();
            second_pomodoro = 0;
            minute_pomodoro = 10;
            label1.Content = output(second_pomodoro);
            label2.Content = output(minute_pomodoro);
        }
        private void Up_Click(object sender, EventArgs e)
        {
            minute_pomodoro += 1;
            label2.Content = output(minute_pomodoro);
        }
        private void Down_Click(object sender, EventArgs e)
        {
            if (minute_pomodoro > 0)
            {
                minute_pomodoro -= 1;
                label2.Content = output(minute_pomodoro);
            }
        }
    }
}