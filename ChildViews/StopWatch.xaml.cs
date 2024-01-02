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
        private ObservableCollection<LapInfo> savedLaps;
        private int initialLapID = 1;
        private int nextLapID = 1;
        private List<LapInfo> batchLaps;
        private LapInfo lapInfo; 
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
            previousLapTime = null;
            savedLaps = new ObservableCollection<LapInfo>();
            lstSavedLaps.ItemsSource = savedLaps;
            LoadSavedLapsFromDatabase();
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
                    lapInfo.IsSaved = false;
                    lapInfo.LapNumber = nextLapID;
                    lapInfo.LapID = nextLapID++;
                    lapInfo.LapTime = lapTime.ToString(@"hh\:mm\:ss\.ff");
                    lapInfo.LapDiff = (lapTimes.Count == 0) ? lapTime.ToString(@"ss\.ff") : lapDiff.ToString(@"ss\.ff");
                    lapInfo.StartTime = DateTime.Now;
                    lapInfo.EndTime = DateTime.Now;
                    batchLaps.Add(lapInfo);
                    lapTimes.Insert(0, lapInfo);
                    previousLapTime = lapTime;
                    lastLapTime = DateTime.Now;

                    // Increment initialLapID for the next LapNumber
                    initialLapID++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recording lap: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadSavedLapsFromDatabase()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM LapHistory";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                LapInfo lap = new LapInfo();
                                lap.LapID = (int)reader["LapID"];
                                lap.LapNumber = (int)reader["LapNumber"];
                                lap.LapTime = reader["LapTime"].ToString();
                                lap.LapDiff = reader["LapDiff"].ToString();
                                lap.Note = reader["Note"].ToString();
                                savedLaps.Add(lap);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task SaveLapToDatabase(List<LapInfo> lapsInfo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO LapHistory (StartTime, EndTime, LapNumber, LapTime, LapDiff, Note) " +
                                   "VALUES (@StartTime, @EndTime, @LapNumber, @LapTime, @LapDiff, @Note)";
                    foreach (var lap in lapsInfo)
                    {
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@StartTime", lap.StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", lap.EndTime);
                            cmd.Parameters.AddWithValue("@LapNumber", lap.LapNumber);
                            cmd.Parameters.AddWithValue("@LapTime", lap.LapTime);
                            cmd.Parameters.AddWithValue("@LapDiff", lap.LapDiff);
                            cmd.Parameters.AddWithValue("@Note", lap.Note ?? "");

                            await cmd.ExecuteNonQueryAsync();
                            lap.IsSaved = true;
                            savedLaps.Insert(0, lap);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving laps to database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    if (lapTimes.Count > 0)
                        await ClearLapHistoryFromDatabaseAsync();
                    var lapsToRemove = lapTimes.Where(lap => !lap.IsSaved).ToList();
                    foreach (var lapToRemove in lapsToRemove)
                    {
                        lapTimes.Remove(lapToRemove);
                    }

                    // Reset initial LapID
                    initialLapID = 1;
                    // Reset nextLapID to initialLapID
                    nextLapID = initialLapID;
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
                    string query = "SELECT * FROM LapHistory ORDER BY LapNumber DESC";
                    SqlCommand cmd = new SqlCommand(query, connection);
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
                                LapDiff = Convert.ToString(reader["LapDiff"]),
                                StartTime = Convert.ToDateTime(reader["StartTime"]),
                                EndTime = Convert.ToDateTime(reader["EndTime"]),
                                Note = Convert.ToString(reader["Note"]),
                                IsSaved = true
                            };
                            tempLapList.Add(lapInfo);
                        }
                    }
                    var orderedLapList = tempLapList.OrderByDescending(lap => lap.LapNumber).ToList();
                    Dispatcher.Invoke(() =>
                    {
                        lapTimes.Clear();
                        foreach (var lapInfo in orderedLapList)
                        {
                            if (!lapInfo.IsSaved)
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
                        string deleteQuery = "DELETE FROM LapHistory";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection);
                        deleteCmd.ExecuteNonQuery();
                        string resetIdentityQuery = "DBCC CHECKIDENT ('LapHistory', RESEED, 0)";
                        SqlCommand resetIdentityCmd = new SqlCommand(resetIdentityQuery, connection);
                        resetIdentityCmd.ExecuteNonQuery();
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing lap history from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            public bool IsSelected { get; set; }
            public string Note { get; set; }
            public bool IsSaved { get; set; }

        }
        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedLaps = lapTimes.Where(lap => lap.IsSelected).ToList();

                foreach (var lap in selectedLaps)
                {
                    lap.Note = lapTimes.First(l => l.LapID == lap.LapID).Note;
                }

                await SaveLapToDatabase(selectedLaps);

                foreach (var lap in selectedLaps)
                {
                    lapTimes.Remove(lap);
                }

                lstLapTimes.Items.Refresh();
                lstSavedLaps.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving laps to database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedLapsToDelete = savedLaps.Where(lap => lap.IsSelected).ToList();
                await DeleteSelectedLapsFromDatabaseAsync(selectedLapsToDelete);
                foreach (var lapToDelete in selectedLapsToDelete)
                {
                    savedLaps.Remove(lapToDelete);
                }
                lstSavedLaps.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting laps: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task DeleteSelectedLapsFromDatabaseAsync(List<LapInfo> lapsToDelete)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string deleteQuery = "DELETE FROM LapHistory WHERE LapID = @LapID";

                    foreach (var lap in lapsToDelete)
                    {
                        using (SqlCommand cmd = new SqlCommand(deleteQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@LapID", lap.LapID);
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting laps from database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Debug.WriteLine($"Error deleting laps from database: {ex}");
            }
        }
    }
}