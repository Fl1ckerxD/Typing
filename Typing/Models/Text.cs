using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Typing.Models
{
    internal class Text : INotifyPropertyChanged
    {
        private string _word;
        public string Word { get => _word; }
        private bool _isTrue = false;
        public bool isTrue { get { return _isTrue; } set { _isTrue = value;  OnPropertyChanged(); _color = isTrue ? "Green" : "Red"; OnPropertyChanged("Color");} }
        private string _color;
        public string Color { get => _color; }
        public Text(string word)
        {
            _word = word;
            _color = "White";
        }

        public void CheckValidWord(string wordA)
        {
            isTrue = wordA == _word;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
