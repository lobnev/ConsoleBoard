using System;
using System.Runtime.InteropServices;

namespace ConsoleBoard
{
    public class ConsoleTools
    {
        const int SWP_NOSIZE = 0x0001;
        
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private static IntPtr MyConsole = GetConsoleWindow();

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        /// <summary>
        /// Устанавливает позицию окна консоли по координатам правого левого угла окна.
        /// </summary>
        /// <param name="left">По ширине</param>
        /// <param name="top">По высоте</param>
        /// /// <param name="width">По ширине</param>
        /// <param name="height">По высоте</param>
        public static void SetConsoleWindowPosition(int left, int top)
        {
            SetWindowPos(MyConsole, 0, left, top, 0, 0, SWP_NOSIZE);
        }
    }
}