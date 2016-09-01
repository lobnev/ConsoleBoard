using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ConsoleBoard.NewFolder1
{
    /// <summary>
    /// Объект обозначающий элемент интерфейса - Frame для краткости
    /// </summary>
    public abstract class Frame : IDrawable
    {
        public virtual CPoint Position { get; set; }
        public virtual CPoint Size { get; set; }

        public List<Frame> Elements { get; set; }

        /// <summary>
        /// Координаты курсора относительно правого верхнего угла элемента
        /// </summary>
        protected virtual CPoint Cursor { get; set; }

        private CPoint GlobalCursor => new CPoint(Console.CursorLeft, Console.CursorTop);

        //public event EventHandler DrawEvent = DrawEventHandler;

        protected Frame()
        {
            Initialize();
        }

        /// <summary>
        /// Инициализация базовыми значениями может проивзодится здесь. Метод вызывается из базового конструктора класса
        /// </summary>
        protected virtual void Initialize()
        {
            
        }
        
        public abstract void Draw();

        /// <summary>
        /// Очищает область элемента
        /// </summary>
        public void Clear()
        {
            string emptyString = new string(' ', Size.X);

            //Console.SetCursorPosition(Position.X, Position.Y);
            for (int i = 0; i < Size.Y; i++)
            {
                Console.SetCursorPosition(Position.X, Position.Y + i);
                Console.Write(emptyString);
            }
        }

        /// <summary>
        /// Двигает курсор в заданные координаты элемента
        /// Двигайте курсор только этой функцией (ок, договорились)
        /// </summary>
        /// <param name="top">Задает смещение по длине</param>
        /// <param name="left">Задает смещение по высоте</param>
        protected void MoveCursor(int top, int left)
        {
            // проверка на выход из зоны элементы. Бросаем исключение? Нет, лучше обнулять текущие относительные координаты
            if (top > Size.Y)
                top = 0;
            if (left > Size.X)
                left = 0;
            
            
            Cursor = new CPoint(left, top);

            // результирующий курсор - сдвиг относительно верхнего левого угла элемента
            var resultGlobalCursor = Cursor + Position;
            Console.SetCursorPosition(resultGlobalCursor.X, resultGlobalCursor.Y);
        }

        //private static void DrawEventHandler(object sender, EventArgs e)
        //{
        //    Console.ResetColor();
        //}

        
    }
}