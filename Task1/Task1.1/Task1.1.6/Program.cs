using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;

namespace Task1._1._6
{
    class Program
    {
        private static Dictionary<string, bool> _styles = new Dictionary<string, bool>()
        {
            {"Bold", false},
            {"Italic", false},
            {"Underlined", false}
        };
        
        private static void GetStyles()
        {
            var res = _styles.Keys.Where(style => _styles[style])
                .Aggregate("", (current, style) => current + style + "\n");
            res = string.IsNullOrWhiteSpace(res) ? "None" : res;
            Console.WriteLine(res);
        }

        private static void SetStyle(string arg)
        {
            try
            {
                switch (arg[0])
                {
                    case '+':
                    {
                        var key = _styles.Keys.ElementAt(Convert.ToInt32(arg.Remove(0, 1))-1);
                        _styles[key] = true;
                        break;
                    }
                    case '-':
                    {
                        var key = _styles.Keys.ElementAt(Convert.ToInt32(arg.Remove(0, 1))-1);
                        _styles[key] = false;
                        break;
                    }
                    default:
                        throw new Exception();

                }
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong input!");
            }
        }
        static void Main(string[] args)
        {
            var cont=true;
            while (cont)
            {
                Console.WriteLine("Choose what to do:\n\'1\' - GetStyles font styles\n\'2\' - Set font style\nAny other input - Exit");
                var mode = Console.ReadLine();
                switch (mode)
                {
                    case "1":
                    {
                        GetStyles();
                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("Choose style:\n\'1\' - Bold\n\'2\' - Italic\n\'3\' - Underlined" +
                                          "\nWrite \'+\'[number] to add style, \'-\'[number] to remove style");
                        var input = Console.ReadLine();
                        SetStyle(input);
                        break;
                    }
                    default:
                    {
                        cont = false;
                        break;
                    }
                }
            }
        }

        
    }
}
