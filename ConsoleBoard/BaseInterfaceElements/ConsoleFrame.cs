using System;
using ConsoleBoard.NewFolder1;

namespace ConsoleBoard.BaseInterfaceElements
{
    /// <summary>
    /// TODO: Описывает текущий фрейм принадлежащий консоли (надо ли?)
    /// </summary>
    public class ConsoleFrame : Frame
    {
        private static Frame _consoleFrame {get; set; }
        
        public override void Draw()
        {
            throw new System.NotImplementedException();
        }

        public static Frame Current()
        {
            if (_consoleFrame == null)
                _consoleFrame = new ConsoleFrame()
                {
                    Rect = new CRectangle(0, 0, Console.WindowWidth, Console.WindowHeight),
                    Parent = _consoleFrame
                };

            return _consoleFrame;
        }
    }
}