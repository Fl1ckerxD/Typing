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
        private string _text;
        public string Texted { get => _text; set { _text = value; OnPropertyChanged(); } }
        private ObservableCollection<Text> _words = new ObservableCollection<Text>();
        public ObservableCollection<Text> Words { get => _words; }
        public MainVM()
        {
            string txt = "программисту этот паттерн позволяет менять отдельные части приложения не затрагивая другие также он может заниматься только одним компонентом вообще не представляя как работают остальные хотя для полного понимания своей работы нужно разбираться во всех аспектах написания приложений";
            foreach (string word in txt.Split())
                _words.Add(new Text(word));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
