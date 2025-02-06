using System.IO;
using System.Text.Json;
using Typing.Models;

namespace Typing.Classes
{
    internal static class JSONReader
    {
        public static List<Score> Scores = new();
        private const string fileName = "data/Scores.json";
        /// <summary>
        /// Запись scores в json файл
        /// </summary>
        /// <param name="score"></param>
        /// <exception cref="Exception"></exception>
        public static void Serialize(Score score)
        {
            try
            {
                Scores.Add(score);
                string jsonString = JsonSerializer.Serialize(Scores);
                using (StreamWriter sw = new(fileName))
                    sw.Write(jsonString);
                Scores = new(SortCollection(Scores));
            }
            catch (Exception ex)
            {
                throw new Exception($"Неполучилось записать данные в файл {fileName}", ex);
            }
        }
        /// <summary>
        /// Читает json файл и получиет объекты scores
        /// </summary>
        /// <exception cref="Exception"></exception>
        public static void Deserialize()
        {
            try
            {
                if (!File.Exists(fileName))
                    return;
                string jsonString = File.ReadAllText(fileName);
                Scores = new(SortCollection(JsonSerializer.Deserialize<Score[]>(jsonString)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Неполучилось получить данные из файла {fileName}", ex);
            }
        }
        /// <summary>
        /// Сортировка коллекции по дате
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        private static Score[] SortCollection(ICollection<Score> scores)
        {
            if (scores is not null)
                return scores.OrderByDescending(x => x.Date).ToArray();
            return null;
        }
    }
}

