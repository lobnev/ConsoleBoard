using System;
using System.Collections.Generic;
using ConsoleBoard.Exceptions;
using ConsoleBoard.NewFolder1;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Table<T> : Frame
    {
        public List<T> Objects { get; set; }
        


        public override void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}