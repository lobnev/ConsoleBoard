using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleBoard.BaseInterfaceElements;

namespace ConsoleBoard.NewFolder1
{
    /// <summary>
    /// Объект обозначающий элемент интерфейса - Frame для краткости
    /// </summary>
    public abstract class Frame : IDrawable
    {
        //public virtual CPoint Position { get; set; }
        //public virtual CPoint Size { get; set; }
        private CRectangle _relativeRelativeRect;
        private CRectangle _absoluteRectangle;
        public virtual CRectangle RelativeRect
        {
            get
            {


                return _relativeRelativeRect;
            }
            set { _relativeRelativeRect = value; }
        }

        protected CRectangle AbsoluteRect => new CRectangle(RelativeRect.Position + Parent.RelativeRect.Position, RelativeRect.Width, RelativeRect.Height);

        private Frame _parent;
        
        public virtual Frame Parent { get; set;
            //get
            //{
            //    if (_parent == null)
            //        _parent = ConsoleFrame.Current();
            //    return _parent;
            //}
            //set
            //{
            //    if (value == null)
            //        _parent = ConsoleFrame.Current();
            //    else this._parent = value;
            //}
        }
        public virtual FrameCollection Childrens { get; set; }



        public Frame()
        {
            //Parent = ConsoleFrame.Current();
            Childrens = new FrameCollection(this);

            Initialize();
        }
        public Frame(CRectangle relativeRect, Frame parent = null)
        {
            RelativeRect = relativeRect;
            Parent = parent;

            if (Parent == null)
                Parent = ConsoleFrame.Current();

            Childrens = new FrameCollection(this);

            Initialize();
        }
        

        /// <summary>
        /// Координаты курсора относительно правого верхнего угла элемента
        /// </summary>
        protected virtual CPoint Cursor { get; set; }

        private CPoint GlobalCursor => new CPoint(Console.CursorLeft, Console.CursorTop);
        
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
            string emptyString = new string(' ', RelativeRect.Width);

            //Console.SetCursorPosition(Position.X, Position.Y);
            for (int i = 0; i < RelativeRect.Width; i++)
            {
                Console.SetCursorPosition(RelativeRect.Position.X, RelativeRect.Position.Y + i);
                Console.Write(emptyString);
            }
        }

        /// <summary>
        /// Двигает курсор в заданные координаты элемента
        /// Двигайте курсор только этой функцией (ок, договорились)
        /// </summary>
        /// <param name="top">Задает смещение по длине</param>
        /// <param name="left">Задает смещение по высоте</param>
        protected void MoveCursor(CPoint point)
        {
            int top = point.X;
            int left = point.Y;

            


            // проверка на выход из зоны элементы. Бросаем исключение? Нет, лучше обнулять текущие относительные координаты
            if (top > RelativeRect.Width)
                top = 0;
            if (left > RelativeRect.Height)
                left = 0;

            Cursor = new CPoint(left, top);

            if (Parent != null)
                Cursor += Parent.RelativeRect.Position;
            

            // результирующий курсор - сдвиг относительно верхнего левого угла элемента
            var resultGlobalCursor = Cursor + RelativeRect.Position;
            Console.SetCursorPosition(resultGlobalCursor.X, resultGlobalCursor.Y);
        }

        //private static void DrawEventHandler(object sender, EventArgs e)
        //{
        //    Console.ResetColor();
        //}

        
    }

    public abstract class Frame<T> : Frame
    {
        public T Object { get; set; }

        //public new Frame<T> Parent { get; set; }
        //public new FrameCollection<T> Childrens { get; set; }


        public Frame(T obj) : base()
        {
            Object = obj;
        }

        //public Frame() : base()
        //{
        //    Childrens = new FrameCollection<T>(null);
        //    Initialize();
        //}
        //public Frame(CRectangle RelativeRect, Frame<T> parent = null) : base(RelativeRect, parent)
        //{
        //    //RelativeRect = RelativeRect;
        //    //Parent = parent;
        //    //Childrens = new FrameCollection(Parent);
        //}

        public abstract override void Draw();
    }
}