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
        public ObservableCollection<Text> Words { get => _words; }
        private string _inputText;
        public string InputText { get => _inputText; set { _inputText = value; OnPropertyChanged(); CheckInputText(value); } }
        private string _currentTime;
        public string CurrentTime { get => _currentTime; set { _currentTime = value; OnPropertyChanged(); } }
        private int _index = 0;
        public ICommand RestartCommand { get; }
        public MainVM()
        {
            _countdownTimer = new(60);
            _countdownTimer.TimeChanged += OnTimeChanged;
            _countdownTimer.TimeElapsed += OnTimeElapsed;

            Refresh();
            CurrentTime = "1:00";
            RestartCommand = new RelayCommand(obj => { Refresh(); _countdownTimer.Reset(60); });
        }
        private void CheckInputText(string value)
        {
            if (value.Length != 0 && _index < Words.Count)
                if (value[^1] == ' ')
                {
                    Words[_index++].CheckValidWord(value.Trim());
                    InputText = "";
                    Result.CalculateResult(Words[_index - 1].isTrue);
                    OnPropertyChanged("Result");
                    if (!_countdownTimer.IsRunning)
                    {
                        _countdownTimer.TimerStart();
                    }
                }
        }
        private void Refresh()
        {
            _words = new ObservableCollection<Text>();
            Result = new Result();
            OnPropertyChanged("Words");
            _index = 0;
            InputText = "";
            GetWords();
        }
        private void GetWords()
        {
            using (StreamReader reader = new StreamReader("Words.txt"))
            {
                Random random = new Random();
                string[] words = reader.ReadToEnd().Split();
                for (int i = 0; i < 20; i++)
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
