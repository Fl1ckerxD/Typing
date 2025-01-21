using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Typing.Classes;
using Typing.Models;

namespace Typing.ViewModels
{
    internal class MainVM
    {
        public ObservableCollection<Text> Words;
        public MainVM()
        {
            string txt = "программисту этот паттерн позволяет менять отдельные части приложения не затрагивая другие также он может заниматься только одним компонентом вообще не представляя как работают остальные хотя для полного понимания своей работы нужно разбираться во всех аспектах написания приложений";
            Words = new ObservableCollection<Text>();
            foreach(string word in txt.Split())
                Words.Add(new Text(word));
        }
    }
}
