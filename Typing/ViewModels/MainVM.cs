using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Typing.Classes;
using Typing.Models;

namespace Typing.ViewModels
{
    internal class MainVM : INotifyPropertyChanged
    {
        public Result Result { get; set; } // Результат теста
        private CountdownTimer _countdownTimer; // Таймер для отсчета времени
        private ObservableCollection<Text> _words;
        public ObservableCollection<Text> Words { get => _words; set { _words = value; OnPropertyChanged(); } } // Коллекция слов для теста
        private ObservableCollection<Score> _scores;
        public ObservableCollection<Score> Scores { get => _scores; set { _scores = value; OnPropertyChanged(); } }
        private Visibility _isActive;
        public Visibility IsActive { get => _isActive; set { _isActive = value; OnPropertyChanged(); } } // Видимость элемента
        public ICommand RestartCommand { get; } // Команда для перезапуска теста
        private string _inputText;
        public string InputText { get => _inputText; set { _inputText = value; OnPropertyChanged(); CheckInputText(value); } } // Введенный пользователем текст
        private string _currentTime;
        public string CurrentTime { get => _currentTime; set { _currentTime = value; OnPropertyChanged(); } } // Текущее оставшееся время
        private int _index = 0; // Индекс текущего слова в коллекции
        public MainVM()
        {
            try
            {
                // Инициализация таймера с начальным значением 60 секунд
                _countdownTimer = new(60);
                _countdownTimer.TimeChanged += OnTimeChanged;
                _countdownTimer.TimeElapsed += OnTimeElapsed;

                // Загрузка истории результатов
                GetScores();

                // Инициализация данных (сброс состояния)
                Refresh();

                // Инициализация команды перезапуска
                RestartCommand = new RelayCommand(obj => { _countdownTimer.Reset(60); Refresh(); });
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

}
        /// <summary>
        /// Проверка введенного текста
        /// </summary>
        /// <param name="value"></param>
        private void CheckInputText(string value)
        {
            try
            {
                // Проверка, что текст введен, индекс в пределах коллекции и таймер не истек
                if (value.Length != 0 && _index < Words.Count && !_countdownTimer.IsElapsed)
                {
                    // Если введен пробел (пользователь завершил ввод слова)
                    if (value[^1] == ' ')
                    {
                        // Проверка правильности введенного слова
                        Words[_index++].CheckValidWord(value.Trim());

                        // Сброс введенного текста
                        InputText = "";

                        // Пересчет результата
                        Result.CalculateResult(Words[_index - 1]);
                        OnPropertyChanged("Result"); // Уведомление об изменении результата

                        // Если таймер не запущен, запускаем его
                        if (!_countdownTimer.IsRunning)
                        {
                            _countdownTimer.TimerStart();
                        }
                    }

                    // Если достигнут конец коллекции слов, собираем новую коллецию слов
                    if (_index == Words.Count)
                    {
                        _index = 0;
                        Words = new ObservableCollection<Text>(Text.GetWords());
                    }
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

}
        /// <summary>
        /// Сброс состояния (начало нового теста)
        /// </summary>
        private void Refresh()
        {
            try
            {
                Result = new Result();
                _index = 0;
                InputText = "";
                CurrentTime = "1:00";
                IsActive = Visibility.Hidden;
                Words = new ObservableCollection<Text>(Text.GetWords());
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

}
        private void OnTimeChanged(int remainingTime) // Обработчик изменения времени таймера
        {
            CurrentTime = remainingTime.ToString();
        }
        private void OnTimeElapsed() // Обработчик завершения времени таймера
        {
            IsActive = Visibility.Visible;
            SaveScore();
            Scores = new(JSONReader.Scores);
            Words = new ObservableCollection<Text>();
        }
        /// <summary>
        /// Сохранение результата
        /// </summary>
        private void SaveScore()
        {
            Score score = new()
            {
                WPM = Result.WPM,
                Accuracy = Result.Accuracy,
                Date = DateTimeOffset.Now
            };
            JSONReader.Serialize(score);
        }
        /// <summary>
        /// Загрузка истории результатов
        /// </summary>
        private void GetScores()
        {
            JSONReader.Deserialize();
            Scores = new(JSONReader.Scores);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
