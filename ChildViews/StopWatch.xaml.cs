using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DoAn_LT.ChildViews
{
    public partial class StopWatch : UserControl
    {
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private ObservableCollection<LapInfo> lapTimes;
        private DateTime lastLapTime;

        public StopWatch()
        {
            InitializeComponent();
            InitializeStopWatch();
        }

        private void InitializeStopWatch()
        {
            stopwatch = new Stopwatch();
            lapTimes = new ObservableCollection<LapInfo>();
            lstLapTimes.ItemsSource = lapTimes;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            TimeSpan elapsedTime = stopwatch.Elapsed;
            txtDisplay.Text = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}.{elapsedTime.Milliseconds / 10:D2}";
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
                timer.Start();
                lastLapTime = DateTime.Now;
            }
            else
            {
                stopwatch.Stop();
                timer.Stop();
            }
        }


        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Stop();
            timer.Stop();
        }

        private void btnLap_Click(object sender, RoutedEventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                TimeSpan lapElapsedTime = stopwatch.Elapsed;
                TimeSpan lapDiff = DateTime.Now - lastLapTime;
                lapTimes.Insert(0, new LapInfo { LapNumber = lapTimes.Count + 1, LapTime = lapElapsedTime.ToString(@"hh\:mm\:ss\.ff"), LapDiff = lapDiff.ToString(@"ss\.ff") });
                lastLapTime = DateTime.Now;
            }
        }


        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Reset();
                lapTimes.Clear();
            }

            lastLapTime = DateTime.Now;
            UpdateDisplay();
        }

        private void lstLapTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class LapInfo
    {
        public int LapNumber { get; set; }
        public string LapTime { get; set; }
        public string LapDiff { get; set; }
    }
}
