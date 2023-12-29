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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Grid grid= new Grid();
            grid.Height = 150;
            grid.Width = 266;
            grid.Background = System.Windows.Media.Brushes.Honeydew;
            Grid childgrid = new Grid();
            childgrid.Background = System.Windows.Media.Brushes.White;
            childgrid.Margin=new Thickness(10,10,10,10);
            Button edit= new Button();  
            edit.HorizontalAlignment = HorizontalAlignment.Left;
            edit.VerticalAlignment = VerticalAlignment.Top;
            edit.Height = 30;
            edit.Width = 30;
            edit.Margin = new Thickness(180, 0, 0, 0);
            Image imageEdit=new Image();
            imageEdit.Source = new BitmapImage(new Uri("C:\\Users\\PC\\OneDrive\\Máy tính\\IT008.O14.DongHo\\Assets\\Images\\Timer\\Edit.png"));
            edit.Content= imageEdit;
            Button remove = new Button();
            remove.HorizontalAlignment = HorizontalAlignment.Left;
            remove.VerticalAlignment = VerticalAlignment.Top;
            remove.Height = 30;
            remove.Width = 30;
            remove.Margin = new Thickness(210, 0, 0, 0);
            remove.Click += Remove_Click;
            Image imageRemove = new Image();
            imageRemove.Source = new BitmapImage(new Uri("C:\\Users\\PC\\OneDrive\\Máy tính\\IT008.O14.DongHo\\Assets\\Images\\Timer\\Cancel.png"));
            remove.Content = imageRemove;
            Label TimerName = new Label();
            TimerName.HorizontalAlignment = HorizontalAlignment.Left;
            TimerName.VerticalAlignment = VerticalAlignment.Top;
            TimerName.Width = 82;
            TimerName.Height = 35;
            TimerName.Margin = new Thickness(0, 3, 0, 0);
            TimerName.Content = "Timer " + NumOfTimer ;
            TimerName.FontSize = 15;
            NumOfTimer++;
            Label hou = new Label();
            hou.Margin=new Thickness(51,0,0,0);
            hou.Content = "00";
            hou.VerticalAlignment = VerticalAlignment.Center;
            hou.HorizontalAlignment= HorizontalAlignment.Left;
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
            Button play = new Button();
            play.Margin = new Thickness(86, 94, 136, 15);
            Image imagePlay = new Image();
            imagePlay.Source = new BitmapImage(new Uri("C:\\Users\\PC\\OneDrive\\Máy tính\\IT008.O14.DongHo\\Assets\\Images\\Timer\\Play1.png"));
            play.Content = imagePlay;
            Button reset = new Button();
            reset.Margin = new Thickness(130, 94, 92, 15);
            play.Margin = new Thickness(86, 94, 136, 15);
            Image imageReset = new Image();
            imageReset.Source = new BitmapImage(new Uri("C:\\Users\\PC\\OneDrive\\Máy tính\\IT008.O14.DongHo\\Assets\\Images\\Timer\\Reset.png"));
            reset.Content = imageReset;
            reset.Click += Reset_Click;
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
            grid.Children.Add(childgrid);
            ListTimer.Children.Add(grid);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Grid gridToRemove = (Grid)deleteButton.Parent;

            // Xóa Grid khỏi WrapPanel
            ListTimer.Children.Remove(gridToRemove);
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Button deleteButton = (Button)sender;
            Grid gridToRemove = (Grid)deleteButton.Parent;

            // Xóa Grid khỏi WrapPanel
            ListTimer.Children.Remove(gridToRemove);
        }
    }
}
