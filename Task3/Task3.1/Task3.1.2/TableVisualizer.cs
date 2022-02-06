using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task3_1_2
{
    public static class TableVisualizer
    {
        public static string Compile(IDictionary sourceDictionary,string headerKey, string headerValue,bool upperLine)
        {
            try
            {
                var source = new Dictionary<object, object> {{headerKey, headerValue}};
                foreach (var key in sourceDictionary.Keys)
                {
                    source.Add(key, sourceDictionary[key]);
                }

                var tableLenKey = source.Keys.Max(i => i.ToString()!.Length);
                var tableLenVal = source.Values.Max(i => i.ToString()!.Length);

                var result = new List<List<char>>();
                var lineSplitter = new string('-', 2 * (tableLenKey + tableLenVal) + 3);
                if(upperLine)
                 result.Add(new List<char>(lineSplitter.ToCharArray()));
                foreach (var key in source.Keys)
                {

                    var keySpaceCount = Math.Max(tableLenKey - (int) Math.Ceiling(key.ToString()!.Length / 2.0), 0);
                    var valSpaceCount = Math.Max(tableLenVal - key.ToString()!.Length / 2, 1);
                    var rightSpaceCount = keySpaceCount - Math.Min((key.ToString()!.Length) / 2, 0);
                    var str = ($"{new string(' ', keySpaceCount)}{key}{new string(' ', rightSpaceCount)}" +
                               $"\0{new string(' ', valSpaceCount)}{source[key]}" +
                               "    ");
                    result.Add(new List<char>(str.ToCharArray()));
                    result.Add(new List<char>(lineSplitter.ToCharArray()));
                }

                for (var i = 1; i < result.Count; i++)
                {
                    var line = result[i];
                    var prevline = result[i - 1];
                    var diff = line.Count - prevline.Count;
                    switch (diff)
                    {
                        case > 0:
                        {
                            var splitIndex = prevline.IndexOf('\0');
                            prevline.InsertRange(splitIndex + 1, new string(' ', diff));
                            break;
                        }
                        case < 0:
                        {
                            var splitIndex = line.IndexOf('\0');
                            line.InsertRange(splitIndex + 1, new string(' ', -diff));
                            break;
                        }
                    }
                }

                for (var i = result.Count - 2; i >= 0; i--)
                {
                    var line = result[i];
                    var nextline = result[i + 1];
                    var diff = line.Count - nextline.Count;
                    switch (diff)
                    {
                        case > 0:
                        {
                            var splitIndex = nextline.LastIndexOf('\0');
                            nextline.InsertRange(splitIndex + 1, new string(' ', diff));
                            break;
                        }
                        case < 0:
                        {
                            var splitIndex = line.LastIndexOf('\0');
                            line.InsertRange(splitIndex + 1, new string(' ', -diff));
                            break;
                        }
                    }
                }

                var sb = new StringBuilder();
                for (var index = 0; index < result.Count; index++)
                {
                    var line = result[index];
                    if (index == 0 && upperLine || index != 0)
                        sb.Append('|');
                    else
                        sb.Append(' ');
                    sb.Append(new string(line.ToArray()));
                    if(index==0&&upperLine||index!=0)
                     sb.Append('|');
                    else
                        sb.Append(' ');
                    sb.Append('\n');
                }

                return sb.ToString().Replace('\0', ' ');
            }
            catch (Exception)
            {
                throw new Exception("Compiling error");
            }
        }
    }
}