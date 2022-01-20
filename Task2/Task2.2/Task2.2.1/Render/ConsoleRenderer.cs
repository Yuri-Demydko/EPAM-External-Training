using System;
using ConsoleGameDev.Render.Interfaces;

namespace ConsoleGameDev.Render
{
    public class ConsoleRenderer:IRenderer
    {
        
        public void DrawCell(int x, int y,char symbol, IField field)
        {
            try
            {
                var (left, top)=Console.GetCursorPosition();
                Console.SetCursorPosition(x,field.Size-y-1);
                Console.Write(symbol);
                Console.SetCursorPosition(left,top);
            }
            catch (Exception)
            {
                return;
            }
        }

        public void DrawText(string text, IField field)
        {
            var (left, top)=Console.GetCursorPosition();
            Console.SetCursorPosition(0,field.Size+1);
            Console.Write(text);
            Console.SetCursorPosition(left,top);
        }

        public void ClearField(IField field)
        {
            for (var i = 0; i < field.Size; i++)
            {
                Console.WriteLine(new string(field.EmptyCellSymbol, field.Size));
            }
        }
        public void MoveCell(int xOld, int yOld, int xNew, int yNew, char symbol,IField field)
        {
            DrawCell(xOld,yOld,field.EmptyCellSymbol, field);
            DrawCell(xNew,yNew,symbol, field);
        }
    }
}