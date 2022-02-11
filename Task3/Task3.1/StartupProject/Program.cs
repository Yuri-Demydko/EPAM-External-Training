using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Task3_1_1;
using Task3_1_2;

namespace StartupProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Task3_2_Run();
        }

        private static void Task3_1_Run()
        {
            var baseList = new[] {"Bob", "Charlie", "Vini", "Vidi", "Vita"};
            var cl = new CircleList<string>(baseList);
            Console.WriteLine("Enter skip value.");
            var skip = Convert.ToInt32(Console.ReadLine());
            while (true)
            {
                try
                {
                    Console.WriteLine($"Removing: {cl.GetNext(skip)}, {cl.Count - 1} remain:");
                    cl.RemoveCurrent();
                    cl.GetAll().ForEach(i => Console.Write($"{i} "));
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("End!");
                    break;
                }
            }
        }

        private static void Task3_2_Run()
        {
            TextStatCounter.WordStatsDto lastResult = null;
            Console.WriteLine("\tWelcome to YWordStatCounter™! Please, input command.");
            var actions = new Dictionary<string, string>()
            {
                {"Input text", "it"},
                {"Input file", "if"},
                {"Last statistics", "ls"},
                {"Clear console", "cc"},
                {"Exit", "ex"}
            };
            Console.WriteLine(TableVisualizer.Compile(actions,"Action","Cmd",true));
            
            var input = "";
            while (input!="ex")
            {
                Console.Write("input: ");
                input = Console.ReadLine().ToLower();
                switch (input)
                {
                    case "it":
                    {
                        Console.WriteLine("---Input text (multiline). Press Ctrl+Enter to stop---");
                        string line;
                        var sb = new StringBuilder();
                        while(!String.IsNullOrWhiteSpace(line=Console.ReadLine()))
                        {
                            sb.Append(line+"\n");
                        }

                        try
                        {
                            lastResult = new TextStatCounter(sb.ToString()).CalculateStats();
                            Console.WriteLine("Statistics calculated! You can check it by \"ls\" command");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Empty text!");
                        }
                        break;
                    }
                    case "if":
                    {
                        Console.WriteLine("---Enter global path to text file---");
                        var path = Console.ReadLine();
                        try
                        {
                            var finfo = new FileInfo(path);
                            lastResult = new TextStatCounter(finfo.OpenText().ReadToEnd()).CalculateStats();
                            Console.WriteLine("Statistics calculated! You can check it by \"ls\" command");
                        }
                        catch (FileNotFoundException)
                        {
                            Console.WriteLine("Wrong file path!");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Empty text!");
                        }
                        break;
                    }
                    case "ls":
                    {
                        if (lastResult == null)
                        {
                            Console.WriteLine("There aren't any text stats! Input some text/file to calculate them.");
                            break;
                        }

                        Console.WriteLine("\tLast analyzed text stats:");
                        Console.WriteLine($"Most common words: {lastResult.MostCommon}");
                        Console.WriteLine($"Most uncommon words: {lastResult.LessCommon}");
                        Console.WriteLine("\tWords table:");
                        Console.WriteLine(TableVisualizer.Compile(lastResult.Stats,"Word","Count",true));
                        break;
                    }
                    case "cc":
                    {
                        Console.Clear();
                        Console.WriteLine(TableVisualizer.Compile(actions,"Action","Cmd",true));
                        break;
                    }
                    case "ex":
                    {
                        Console.WriteLine("Bye!");
                        break;
                    }
                    default:
                        Console.WriteLine("Invalid command!");
                        break;
                }
            }
        }
    }
}