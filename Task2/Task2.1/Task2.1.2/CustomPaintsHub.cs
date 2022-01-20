using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace YGeometry
{
    public static class CustomPaintsHub
    {
        private static readonly IList<CustomPaint> CustomPaints=new List<CustomPaint>();

        private static CustomPaint _currentPaint;

        private static void CreateNewPaint()
        {
            var paint = CustomPaint.InitCustomPaint();
            CustomPaints.Add(paint);
            _currentPaint = paint;
        }

        private static void SwitchUser(string uname)
        {
            _currentPaint = CustomPaints.FirstOrDefault(p => p.UserName == uname)
                       ??
                       throw new ArgumentException("Paint with such username doesn't exist!");
        }

        public static void Run()
        {
            if(_currentPaint==null)
                CreateNewPaint();
            var stop = false;
            do
            {
                Console.WriteLine(
                    $"Welcome to CustomPaint's Hub! [USER: {_currentPaint.UserName}]\n" +
                    "Choose what to do:\n"+
                "\"cr\" - Create new shape\n" +
                "\"dr\" - Draw all shapes\n"+
                "\"new\" - Create new paint\n"+
                "\"su\" - Switch user\n"
                +"\"cls\" - Clear current paint\n"+
                "\"q\" - Exit\n"
                );
                var input = Console.ReadLine();
                switch (input)
                {
                    case "cr":
                    {
                        Console.WriteLine("Choose shape type (input it's number):\n" +
                                          "1 - Circle\n" +
                                          "2 - Circumference\n" +
                                          "3 - Ring\n" +
                                          "4 - Line\n" +
                                          "5 - Triangle\n" +
                                          "6 - Rectangle\n" +
                                          "7 - Square");
                        try
                        {
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    _currentPaint.AddShape<Circle>();
                                    break;
                                case "2":
                                    _currentPaint.AddShape<Circumference>();
                                    break;
                                case "3":
                                    _currentPaint.AddShape<Ring>();
                                    break;
                                case "4":
                                    _currentPaint.AddShape<Line>();
                                    break;
                                case "5":
                                    _currentPaint.AddShape<Triangle>();
                                    break;
                                case "6":
                                    _currentPaint.AddShape<Rectangle>();
                                    break;
                                case "7":
                                    _currentPaint.AddShape<Square>();
                                    break;
                                default:
                                    Console.WriteLine("Invalid input!");
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        break;
                    }
                    case "dr":
                    {
                        _currentPaint.DrawShapes();
                        break;
                    }
                    case "new":
                    {
                        CreateNewPaint();
                        break;
                    }
                    case "su":
                    {
                        try
                        {
                            Console.Write("Please, enter username: ");
                            SwitchUser(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                        
                    }
                    case "cls":
                    {
                        Console.WriteLine("Current paint cleared!");
                        _currentPaint = new CustomPaint(_currentPaint.UserName);
                        break;
                    }
                    case "q":
                    {
                        Console.WriteLine("Bye!");
                        stop = true;
                        break;
                    }
                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                //Console.ReadKey();
            } while (!stop);
        }
    }
}