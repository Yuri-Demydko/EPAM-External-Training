using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task3_1_2
{
    public class TextStatCounter
    {
        public class WordStatsDto
        {
            private Dictionary<string, int> _stats;
            private string _mostCommon;
            private string _lessCommon;

            public WordStatsDto(Dictionary<string, int> stats, string mostCommon, string lessCommon)
            {
                _stats = stats;
                _mostCommon = mostCommon;
                _lessCommon = lessCommon;
            }

            public Dictionary<string, int> Stats => _stats;

            public string MostCommon => _mostCommon;

            public string LessCommon => _lessCommon;
        }
        private string text;

        public TextStatCounter(FileInfo file)
        {
           Prepare(file.OpenText().ReadLine());
        }
        public TextStatCounter(string text)
        {
            Prepare(text);
        }
        private void Prepare(string text)
        {
            this.text = Regex.Replace(text.ToLower(),@"[^\w\s]", " ");
        }
        
        public WordStatsDto CalculateStats()
        {
            var arr=text.Replace("\n"," ")
                .Split(" ");
            var dict= arr.Distinct()
                .ToDictionary(k=>k,v => arr.Count(i => i == v))
                .OrderByDescending(i=>i.Value)
                .ToDictionary(k=>k.Key,v=>v.Value);
            if (dict.ContainsKey(""))
                dict.Remove("");
            var mostCommonWords = string.Join(", ",dict.Keys.Where(i => dict[i] == dict.Values.Max()));
            var lessCommonWords = string.Join(", ",dict.Keys.Where(i => dict[i] == dict.Values.Min()));
            return new WordStatsDto(dict, mostCommonWords, lessCommonWords);
        }
        
        
    }
}