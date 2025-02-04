using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        private int _chars;
        public int Chars { get => _chars; }

        public void CalculateResult(bool correct)
        {
            if (correct) _correctWords++; else _wrongWords++;
            SetWPM();
            SetAccuracy();
        }
        public void SetAccuracy()
        {
            _accuracy = Math.Round((double)_correctWords / _wpm * 100, 2);
        }
        public void SetWPM()
        {
            _wpm = _chars / 5;
        }
    }
}
