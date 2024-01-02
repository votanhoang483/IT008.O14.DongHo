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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Windows.Threading;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Net.Http;
using HtmlAgilityPack;
using System.Runtime.CompilerServices;

namespace DoAn_LT.ChildViews
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            this.DataContext = this;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            Day.Content = DateTime.Now.Day;
            Month.Content = DateTime.Now.Month;
            Year.Content = DateTime.Now.Year;
            GetWeatherData();
            GetNgayAm("https://www.xemlicham.com/");
        }
        public string TimeNow
        {
            get { return DateTime.Now.ToString("dd/MM/yyyy"); }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Hour.Content = DateTime.Now.Hour.ToString();
            if (DateTime.Now.Minute < 10)
            {
                Minute.Content = "0" + DateTime.Now.Minute.ToString();
            }
            else
                Minute.Content = DateTime.Now.Minute.ToString();
            if(DateTime.Now.Second < 10)
            {
                Second.Content = "0" + DateTime.Now.Second.ToString();
            }
            else
               Second.Content = DateTime.Now.Second.ToString();
        }
        public void GetWeatherData()
        {
            string apiKey = "2bb2ffa2c75e1679a84913440bcda55d";
            string cityId = "1566083";
            string requestUrl = $"http://api.openweathermap.org/data/2.5/weather?id={cityId}&appid={apiKey}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(requestUrl);
                JObject obj = JObject.Parse(json);
                int tempInKelvin = (int)obj["main"]["temp"];
                int tempInCelsius = tempInKelvin - 273;
                Weather.Content = tempInCelsius.ToString();
            }
        }

        public async void GetNgayAm(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string htmlContent = response.Content.ReadAsStringAsync().Result;

                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(htmlContent);

                        HtmlNode ngayAmNode = doc.DocumentNode.SelectSingleNode("//div[@class='date-lich-van-su']");

                        if (ngayAmNode != null)
                        {
                            string text = ngayAmNode.InnerText.Trim();
                            ngayam.Content = text;
                        }
                        else
                            ngayam.Content = "không thể lấy ngày âm từ trang web";
                    }
                    else
                    {
                        ngayam.Content = $"Lỗi: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }    
            }
                catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }


            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@class='date-lich-van-su']");

            if(node != null )
            {
                string content = node.InnerHtml.Trim();

                ngayam.Content = content;
            }

        }
    }

}
