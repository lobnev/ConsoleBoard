using System;
using ConsoleBoard.Helpers;

namespace ConsoleBoard.BaseInterfaceElements
{
    /// <summary>
    /// TODO: Описывает текущий фрейм принадлежащий консоли (надо ли?)
    /// </summary>
    public class ConsoleFrame : Frame.Frame
    {
        private static Frame.Frame _consoleFrame {get; set; }
        
        public override void Draw()
        {
            throw new System.NotImplementedException();
        }

        public static Frame.Frame Current()
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