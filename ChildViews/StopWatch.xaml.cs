using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace DoAn_LT.ChildViews
{
    public partial class StopWatch : UserControl
    {
        private int nextLapID = 1;
        private List<LapInfo> batchLaps;
        private LapInfo lapInfo;  // Declare lapInfo at the class level
        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private ObservableCollection<LapInfo> lapTimes;
        private DateTime lastLapTime;
        private TimeSpan? previousLapTime;
        private string connectionString = @"Server=tcp:server-super-vip.database.windows.net,1433;Initial Catalog=doancuoiki-dongho;Persist Security Info=False;User ID=serversupervip;Password=Vip12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public StopWatch()
        {
            InitializeComponent();
            InitializeStopWatch();
            LoadLapHistoryFromDatabase();
            batchLaps = new List<LapInfo>();
            previousLapTime = null;  // Khởi tạo previousLapTime
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
            try
            {
                stopwatch.Start();
                timer.Start();
                lastLapTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting stopwatch: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                stopwatch.Stop();
                timer.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping stopwatch: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnLap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (stopwatch.IsRunning)
                {
                    TimeSpan lapTime = stopwatch.Elapsed;

                    TimeSpan lapDiff;
                    if (previousLapTime != null)
                    {
                        lapDiff = lapTime - previousLapTime.Value;
                    }
                    else
                    {
                        lapDiff = lapTime;
                    }

                    lapInfo = new LapInfo();

                    // Set LapNumber equal to LapID
                    lapInfo.LapNumber = nextLapID;

                    // Set LapID based on the next available LapID
                    lapInfo.LapID = nextLapID++;

                    lapInfo.LapTime = lapTime.ToString(@"hh\:mm\:ss\.ff");
                    lapInfo.LapDiff = (lapTimes.Count == 0) ? lapTime.ToString(@"ss\.ff") : lapDiff.ToString(@"ss\.ff");
                    lapInfo.StartTime = DateTime.Now;
                    lapInfo.EndTime = DateTime.Now;

                    batchLaps.Add(lapInfo);

                    lapTimes.Insert(0, lapInfo);

                    previousLapTime = lapTime;

                    lastLapTime = DateTime.Now;

                    // Save the lap to the database immediately after adding it to the collection
                    await SaveLapToDatabase(new List<LapInfo> { lapInfo });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording lap: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task SaveLapToDatabase(List<LapInfo> lapsInfo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO LapHistory (StartTime, EndTime, LapNumber, LapTime, LapDiff) " +
                                   "VALUES (@StartTime, @EndTime, @LapNumber, @LapTime, @LapDiff)";

                    // Iterate through the list of laps
                    foreach (var lap in lapsInfo)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@StartTime", lap.StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", lap.EndTime);
                            cmd.Parameters.AddWithValue("@LapNumber", lap.LapNumber);
                            cmd.Parameters.AddWithValue("@LapTime", lap.LapTime);
                            cmd.Parameters.AddWithValue("@LapDiff", lap.LapDiff);

                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving laps to database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Log the error for debugging purposes
                Debug.WriteLine($"Error saving laps to database: {ex}");
            }
        }

        private async void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Reset();
                    batchLaps.Clear();

                    // Clear lap history from the database
                    await ClearLapHistoryFromDatabaseAsync();

                    // Clear lap history from the ObservableCollection
                    lapTimes.Clear();
                }

                lastLapTime = DateTime.Now;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error resetting stopwatch: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstLapTimes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changed event if needed
        }

        private void LoadLapHistoryFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM LapHistory ORDER BY LapNumber DESC"; // Thay đổi tại đây
                    SqlCommand cmd = new SqlCommand(query, connection);

                    // Tạo danh sách tạm thời để lưu trữ dữ liệu từ cơ sở dữ liệu
                    List<LapInfo> tempLapList = new List<LapInfo>();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LapInfo lapInfo = new LapInfo
                            {
                                LapID = Convert.ToInt32(reader["LapID"]),
                                LapNumber = Convert.ToInt32(reader["LapNumber"]),
                                LapTime = Convert.ToString(reader["LapTime"]),
                                LapDiff = Convert.ToString(reader["LapDiff"])
                            };

                            tempLapList.Add(lapInfo);
                        }
                    }

                    // Sau khi đọc dữ liệu từ cơ sở dữ liệu, sắp xếp lại danh sách theo LapNumber giảm dần
                    var orderedLapList = tempLapList.OrderByDescending(lap => lap.LapNumber).ToList();

                    // Chắc chắn rằng lapTimes đang hoạt động trên luồng giao diện người dùng
                    Dispatcher.Invoke(() =>
                    {
                        // Thêm dữ liệu vào lapTimes
                        foreach (var lapInfo in orderedLapList)
                        {
                            // Kiểm tra xem LapID đã tồn tại trong lapTimes chưa
                            if (!lapTimes.Any(lap => lap.LapID == lapInfo.LapID))
                            {
                                lapTimes.Add(lapInfo);
                            }
                        }

                        lstLapTimes.ItemsSource = lapTimes;
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading lap history from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task ClearLapHistoryFromDatabaseAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Delete all records from the LapHistory table
                        string deleteQuery = "DELETE FROM LapHistory";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                        deleteCmd.ExecuteNonQuery();

                        // Reset the identity seed to 0
                        string resetIdentityQuery = "DBCC CHECKIDENT ('LapHistory', RESEED, 0)";
                        SqlCommand resetIdentityCmd = new SqlCommand(resetIdentityQuery, connection);
                        resetIdentityCmd.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing lap history from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Log the error for debugging purposes
                Debug.WriteLine($"Error clearing lap history from database: {ex}");
            }
        }

        private class LapInfo
        {
            public int LapID { get; set; }
            public int LapNumber { get; set; }
            public string LapTime { get; set; }
            public string LapDiff { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
        }
    }
}
