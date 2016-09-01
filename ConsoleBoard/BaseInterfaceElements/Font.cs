﻿using System;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Font
    {
        public Font()
        {
        }
        public Font(ConsoleColor background, ConsoleColor textColor)
        {
            TextColor = textColor;
            Background = background;
        }
        public Font(ConsoleColor background)
        {
            Background = background;
        }
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public ConsoleColor Background { get; set; } = ConsoleColor.White;
    }
}