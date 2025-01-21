using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Typing.Classes;
using Typing.Models;

namespace Typing.ViewModels
{
    internal class MainVM : INotifyPropertyChanged
    {
        private string _text;
        public string Texted { get => _text; set { _text = value; OnPropertyChanged(); CheckInputText(value); } }
        private ObservableCollection<Text> _words = new ObservableCollection<Text>();
        public ObservableCollection<Text> Words { get => _words; }
        private int _k = 0;
        private DispatcherTimer _timer = null;
        private string _time;
        public string Timer { get => _time; set { _time = value; OnPropertyChanged(); } }
        private bool _running = false;
        private int x = 60;
        public MainVM()
        {
            string txt = "программисту этот паттерн позволяет менять отдельные части приложения не затрагивая другие также он может заниматься только одним компонентом вообще не представляя как работают остальные хотя для полного понимания своей работы нужно разбираться во всех аспектах написания приложений";
            foreach (string word in txt.Split())
                _words.Add(new Text(word));
        }
        private void CheckInputText(string value)
        {
            if (value.Length != 0)
                if (value[^1] == ' ')
                {
                    Words[_k++].CheckValidWord(value.Trim());
                    Texted = "";
                    if (!_running)
                    {
                        _running = true;
                        TimerStart();
                    }
                }
        }
        private void TimerStart()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(TimerTick);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            x--;
            Timer = x.ToString();
            if (x == 0)
            {
                _timer.Stop();
                _running = false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
