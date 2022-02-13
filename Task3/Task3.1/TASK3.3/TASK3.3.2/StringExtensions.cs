using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace TASK3._3._2
{
    public static class StringExtensions
    {
        public static StringLangType GetLangType(this string str)
        {
            var strArr = Regex.Replace(str, @"[^\w\s]", "")
                .ToLower()
                .Split(' ')
                .ToList();

            var rus = strArr.All(s => Regex.IsMatch(s, "[а-я]"));
            var eng = strArr.All(s => Regex.IsMatch(s, "[a-z]"));
            var num = strArr.All(s => Regex.IsMatch(s, "[0-9]"));
            
            if ((rus && eng) || (rus && num) || (eng && num)) return StringLangType.Mixed;
            if (rus) return StringLangType.Russian;
            return eng ? StringLangType.English : StringLangType.Number;
        }
        
    }
}