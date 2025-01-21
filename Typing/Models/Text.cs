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
        private bool _isTrue;
        public bool isTrue { get { return _isTrue; } set { _isTrue = value;  OnPropertyChanged(); OnPropertyChanged("Color"); } }
        public string Color { get => isTrue ? "Green" : "Red"; }
        public Text(string word)
        {
            _word = word;
            isTrue = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
