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
using NodaTime;

namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for WorldClock.xaml
    /// </summary>
    public partial class WorldClock : UserControl
    {
        private DispatcherTimer _timer;
        List<string> myItems = new List<string>

        {

        };

        public WorldClock()
        {
            InitializeComponent();


        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {

            var itemToRemove = ListResult.SelectedItem;


            if (itemToRemove != null)
            {

                ListResult.Items.Remove(itemToRemove);
            }
        }
        private void ListClock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ListClock.Text.Length > 0)
            {

                ListClock.ItemsSource = myItems.Where(item => item.ToLower().Contains(ListClock.Text.ToLower()));
            }
            else
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ListClock.Text != null)
            {
                if (ListClock.Text == "Samoa"
                    || ListClock.Text == "Pago Pago")
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Niue"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 20;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Hawaii"
                    || ListClock.Text == "Tahiti (Pháp)")
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Honolulu"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Alaska")
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/Anchorage"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Washington D.C., Virginia, Maryland"
                    || ListClock.Text == "Mexico City (Mexico)"
                    || ListClock.Text == "British Columbia (Canada)")
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Etc/GMT+8"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Arizona, Colorado, Utah"
                   || ListClock.Text == "Canada-Alberta"
                   || ListClock.Text == "Chihuahua (Mexico)")
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/Los_Angeles"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Texas, Illiois, Missouri"
                   || ListClock.Text == "Canada-Saskatchewan"
                   )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/Denver"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-New York, Florida, Georgia"
                  || ListClock.Text == "Canada-Toronto"
                  || ListClock.Text == "Peru"
                  )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/Chicago"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hoa Kì-Washington D.C., Virginia, Maryland"
                  || ListClock.Text == "Puerto Rico"
                  || ListClock.Text == "Chile"
                  )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/New_York"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Argentina"
                 || ListClock.Text == "Brazil"
                 || ListClock.Text == "Canada-Halifax"

                 )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["America/Argentina/Buenos_Aires"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Bồ Đào Nha"
                 || ListClock.Text == "Cape Verde"


                 )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Atlantic/Azores"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Anh (UK)"
                || ListClock.Text == "Gambia"
                || ListClock.Text == "Senegal"
                || ListClock.Text == "Ireland"
                || ListClock.Text == "Iceland"
                || ListClock.Text == "Ghana"


                )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Etc/GMT"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Pháp"
                || ListClock.Text == "Đức"
                || ListClock.Text == "ý"
                || ListClock.Text == "Tây Ban Nha"
                || ListClock.Text == "Hà Lan"
                || ListClock.Text == "Bỉ"
                || ListClock.Text == "Thụy Sĩ"
                || ListClock.Text == "Algeria"
                || ListClock.Text == "Tunisia"
                || ListClock.Text == "Nigeria"


                )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Europe/London"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Hy Lạp"
                || ListClock.Text == "Bulgaria"
                || ListClock.Text == "Romania"
                || ListClock.Text == "Ukraine"
                || ListClock.Text == "Jordan"
                || ListClock.Text == "Israel"
                || ListClock.Text == "Nam Phi"
                || ListClock.Text == "Syria"
                || ListClock.Text == "Ai Cập"



                )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Europe/Paris"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Nga"
                || ListClock.Text == "Thổ Nhĩ Kì"
                || ListClock.Text == "Saudi Arabia"
                || ListClock.Text == "Iraq"
                || ListClock.Text == "Kuwait"
                || ListClock.Text == "Qatar"
                || ListClock.Text == "Yemen"




                )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Europe/Moscow"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Iran"

                )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Tehran"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Azerbaijan"
               || ListClock.Text == "Armenia"
               || ListClock.Text == "Georgia"
               || ListClock.Text == "Oman"
               || ListClock.Text == "UAE"


               )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Dubai"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Afghanistan"

               )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Kabul"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }

                if (ListClock.Text == "Kazakhstan"
              || ListClock.Text == "Uzbekistan"
              || ListClock.Text == "Pakistan"
              || ListClock.Text == "Turkmenistan"



              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Almaty"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Ấn Độ"
              || ListClock.Text == "Sri Lanka"

              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Colombo"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Nepal"

              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Kathmandu"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Kyrgyzstan"
              || ListClock.Text == "Bangladesh"
              || ListClock.Text == "Bhutan"


              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Almaty"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Cocos Islands"
              || ListClock.Text == "Myanmar"



              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Rangoon"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Việt Nam"
             || ListClock.Text == "Indonesia (Western Time)"
             || ListClock.Text == "Thái Lan"
             || ListClock.Text == "Lào"
             || ListClock.Text == "Cambodia"


             )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Bangkok"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Trung Quốc"
        || ListClock.Text == "Singapore"
        || ListClock.Text == "Malaysia"
        || ListClock.Text == "Philippines"
        || ListClock.Text == "Úc (Western Standard Time)"


)
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Asia/Ho_Chi_Minh"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Úc (Central Western Standard Time)"

              )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Eucla"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Nhật Bản"
             || ListClock.Text == "Hàn Quốc"



             )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Perth"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Úc (Central Standard Time)"



             )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Adelaide"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Úc (Eastern Standard Time)"
            || ListClock.Text == "Papua New Guinea"



            )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Darwin"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Úc-Lord Howe Island"



            )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Brisbane"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Solomon Islands"
        || ListClock.Text == "Vanuatu"
        || ListClock.Text == "Micronesia (Chuuk)"
        || ListClock.Text == "New Caledonia"


)
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Australia/Lord_Howe"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Úc-Norfolk Island"



            )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Norfolk"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Fiji"
       || ListClock.Text == "Tuvalu"
       || ListClock.Text == "New Zealand"
       || ListClock.Text == "Marshall Islands"
       || ListClock.Text == "Nauru"


)
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Auckland"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Chatham Islands (New Zealand)"



           )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Chatham"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Tonga"
      || ListClock.Text == "Kiribati (Phoenix Islands)"



)
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Tongatapu"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
                if (ListClock.Text == "Line Islands (Kiribati)"



           )
                {
                    DateTimeZone UTCminus11 = DateTimeZoneProviders.Tzdb["Pacific/Kiritimati"];
                    ZonedDateTime currentUTCminus11 = SystemClock.Instance.GetCurrentInstant().InZone(UTCminus11);
                    TextBlock UTC_minus11 = new TextBlock();
                    UTC_minus11.FontSize = 30;
                    UTC_minus11.Text = "Time in " + ListClock.Text + ": " + currentUTCminus11.ToString();
                    ListResult.Items.Add(UTC_minus11);
                }
            }
        }

    }
}
