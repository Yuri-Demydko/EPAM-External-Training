using System;
using YGeometry;
using YStrings;

namespace StartupProject
{
    class Program
    {
        static void Main(string[] args)
        {
            new CustomPaintsHub(new ConsoleShapeCreationHelper()).Run();
        }
    }
}