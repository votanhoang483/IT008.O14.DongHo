using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Input;
using System.ComponentModel;
namespace DoAn_LT.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string caption;
        private IconChar icon;
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }
        public string Caption
        {
            get
            {
                return caption;

            }
            set
            {
                caption = value;
                OnPropertyChanged(nameof(Caption));
            }

        }
        public IconChar Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        public ICommand ShowAlarmCommand { get; }
        public ICommand ShowPomodoroCommand { get; }
        public ICommand ShowStopWatchCommand { get; }
        public ICommand ShowTimerCommand { get; }
        public ICommand ShowWorldClockCommand { get; }
        
        public ICommand ShowHomeCommand { get; }
        public MainViewModel()
        {
            ShowAlarmCommand = new ViewModelCommand(ExecuteShowAlarmCommand);
            ShowPomodoroCommand = new ViewModelCommand(ExecuteShowPomodoroCommand);
            ShowStopWatchCommand = new ViewModelCommand(ExecuteShowStopWatchCommand);
            ShowTimerCommand = new ViewModelCommand(ExecuteShowTimerCommand);
            ShowWorldClockCommand = new ViewModelCommand(ExecuteShowWorldClockCommand);
            ShowHomeCommand = new ViewModelCommand(ExecuteShowHomeCommand);
            }

        private void ExecuteShowWorldClockCommand(object obj)
        {
            CurrentChildView = new WorldClockViewModel();
            Caption = "World Clock";
            Icon = IconChar.EarthEurope;
        }

        private void ExecuteShowTimerCommand(object obj)
        {
            CurrentChildView = new TimerViewModel();
            Caption = "Timer";
            Icon = IconChar.Hourglass1;
        }

        private void ExecuteShowStopWatchCommand(object obj)
        {
            CurrentChildView = new StopWatchViewModel();
            Caption = "StopWatch";
            Icon = IconChar.Stopwatch;
        }

        private void ExecuteShowAlarmCommand(object obj)
        {
            CurrentChildView = new AlarmViewModel();
            Caption = "Alarm";
            Icon = IconChar.Bell;
        }
        private void ExecuteShowPomodoroCommand(object obj)
        {
            CurrentChildView = new PomodoroViewModel();
            Caption = "Pomodoro Clock";
            Icon = IconChar.FireFlameCurved;
        }
       private void ExecuteShowHomeCommand(object obj)
        {
            CurrentChildView = new HomeViewMoDel();
            Caption = "Home";
            Icon = IconChar.Home;
        }
    }
}
