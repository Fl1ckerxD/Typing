namespace Typing.Models
{
    internal class Result
    {
        private int _wpm;
        public int WPM { get => _wpm; }
        private double _accuracy;
        public double Accuracy { get => _accuracy; }
        private int _correctWords;
        public int CorrectWords { get => _correctWords; }
        private int _wrongWords;
        public int WrongWords { get => _wrongWords; }
        private int _chars = 0;
        public int Chars { get => _chars; }

        /// <summary>
        /// Подсчет результата
        /// </summary>
        /// <param name="word"></param>
        public void CalculateResult(Text word)
        {
            if (word.isTrue) _correctWords++; else _wrongWords++;
            _chars += word.Word.Length;
            SetWPM();
            SetAccuracy();
        }
        /// <summary>
        /// Высчитывает точность
        /// </summary>
        public void SetAccuracy()
        {
            _accuracy = Math.Round((double)_correctWords / (_correctWords + _wrongWords) * 100, 2);
        }
        /// <summary>
        /// Высчитывает СВМ
        /// </summary>
        public void SetWPM()
        {
            _wpm = _correctWords + _wrongWords;
        }
    }
}
