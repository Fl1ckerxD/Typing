using System.Windows.Threading;

namespace Typing.Classes
{
    class CountdownTimer
    {
        private DispatcherTimer _timer = new();
        private int _remainingTime; // Оставшееся время в секундах
        public event Action<int> TimeChanged; // Событие для уведомления об изменении времени
        public event Action TimeElapsed; // Событие для уведомления об истечении времени
        public bool IsRunning { get => _timer.IsEnabled; }
        public CountdownTimer(int totalTimeInSeconds)
        {
            _remainingTime = totalTimeInSeconds;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimerTick;
        }
        public void TimerStart()
        {
            if(_remainingTime > 0)
                _timer.Start();
        }
        public void TimerStop()
        {
            _timer.Stop();
        }
        public void Reset(int totalTimeInSeconds)
        {
            _remainingTime = totalTimeInSeconds;
            _timer.Stop();
            TimeChanged?.Invoke(_remainingTime); // Уведомляем о сбросе времени
        }
        private void TimerTick(object sender, EventArgs e)
        {
            _remainingTime--;
            TimeChanged?.Invoke(_remainingTime); // Уведомляем об изменении времени
            
            if (_remainingTime <= 0) // Если время истекло, останавливаем таймер
            {
                _timer.Stop();
                TimeElapsed?.Invoke();
            }
        }
    }
}
