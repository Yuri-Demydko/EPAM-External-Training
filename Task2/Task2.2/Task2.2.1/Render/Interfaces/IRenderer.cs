#nullable enable
namespace ConsoleGameDev.Render.Interfaces
{
    public interface IRenderer
    {
        public void DrawCell(int x, int y,char symbol,IField field);
        public void ClearField(IField field);
        public void MoveCell(int xOld, int yOld, int xNew, int yNew, char symbol,IField field);
        public void DrawText(string text, IField field);
    }
}