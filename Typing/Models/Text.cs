using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;

namespace Typing.Models
{
    internal class Text : INotifyPropertyChanged
    {
        private string _word;
        public string Word { get => _word; }
        private string _color;
        public string Color { get => _color; }
        private bool _isTrue = false;
        public bool isTrue { get { return _isTrue; } set { _isTrue = value; OnPropertyChanged(); _color = isTrue ? "Green" : "Red"; OnPropertyChanged("Color"); } }
        private const string fileName = "data/Words.txt";
        public Text(string word)
        {
            _word = word;
            _color = "White";
        }
        /// <summary>
        /// Узнать совподают ли слова
        /// </summary>
        /// <param name="wordA"></param>
        public void CheckValidWord(string wordA)
        {
            isTrue = wordA == _word;
        }
        /// <summary>
        /// Загрузка слов из файла
        /// </summary>
        public static ICollection<Text> GetWords()
        {
            try
            {
                List<Text> Words = new();
                if (!File.Exists(fileName))
                    using (StreamWriter sw = new(fileName))
                        sw.Write("начальные тестовые слова да может его их и не было вообще");

                using (StreamReader reader = new StreamReader(fileName))
                {
                    Random random = new Random();
                    string[] words = reader.ReadToEnd().Split();
                    for (int i = 0; i < 40; i++)
                    {
                        int r = random.Next(0, words.Length - 1);
                        Words.Add(new Text(words[r]));
                    }
                }
                return Words;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не получлось собрать коллекцию слов из файла data/Words.txt", ex);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
