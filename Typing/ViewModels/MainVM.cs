using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
        public Result Result { get; set; }
        private CountdownTimer _countdownTimer;
        private ObservableCollection<Text> _words;
        public ObservableCollection<Text> Words { get => _words; set { _words = value; OnPropertyChanged(); } }
        private string _inputText;
        public string InputText { get => _inputText; set { _inputText = value; OnPropertyChanged(); CheckInputText(value); } }
        private string _currentTime;
        public string CurrentTime { get => _currentTime; set { _currentTime = value; OnPropertyChanged(); } }
        private int _index = 0;
        private Visibility _isActive;
        public Visibility IsActive { get => _isActive; set { _isActive = value; OnPropertyChanged(); } }
        public ICommand RestartCommand { get; }
        public MainVM()
        {
            _countdownTimer = new(60);
            _countdownTimer.TimeChanged += OnTimeChanged;
            _countdownTimer.TimeElapsed += OnTimeElapsed;

            Refresh();
            RestartCommand = new RelayCommand(obj => { _countdownTimer.Reset(60); Refresh(); });
        }
        private void CheckInputText(string value)
        {
            
            if (value.Length != 0 && _index < Words.Count && !_countdownTimer.IsElapsed)
            {
                if (value[^1] == ' ')
                {
                    Words[_index++].CheckValidWord(value.Trim());
                    InputText = "";
                    Result.CalculateResult(Words[_index - 1]);
                    OnPropertyChanged("Result");
                    if (!_countdownTimer.IsRunning)
                    {
                        _countdownTimer.TimerStart();
                    }
                }
                if (_index == Words.Count)
                {
                    _index = 0;
                    GetWords();
                }
            }
        }
        private void Refresh()
        {
            Result = new Result();
            _index = 0;
            InputText = "";
            CurrentTime = "1:00";
            IsActive = Visibility.Hidden;
            GetWords();
        }
        private void GetWords()
        {
            Words = new ObservableCollection<Text>();
            using (StreamReader reader = new StreamReader("Words.txt"))
            {
                Random random = new Random();
                string[] words = reader.ReadToEnd().Split();
                for (int i = 0; i < 40; i++)
                {
                    int r = random.Next(0, words.Length - 1);
                    _words.Add(new Text(words[r]));
                }
            }
        }
        private void OnTimeChanged(int remainingTime)
        {
            CurrentTime = remainingTime.ToString();
        }
        private void OnTimeElapsed()
        {
            MessageBox.Show("Время истекло");
            IsActive = Visibility.Visible;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
