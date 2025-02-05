using System.IO;
using System.Text.Json;
using Typing.Models;

namespace Typing.Classes
{
    internal static class JSONReader
    {
        private static List<Score> Scores = new();
        private const string fileName = "data/Scores.json";
        public static void Serialize(Score score)
        {
            Scores.Add(score);
            string jsonString = JsonSerializer.Serialize(Scores);
            using (StreamWriter sw = new(fileName))
                sw.Write(jsonString);
        }
        public static void Deserialize()
        {
            if (!File.Exists(fileName))
                return;
            string jsonString = File.ReadAllText(fileName);
            Scores = new(JsonSerializer.Deserialize<Score[]>(jsonString));
        }
    }
}

