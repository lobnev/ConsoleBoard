﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ConsoleBoard.BaseInterfaceElements;

namespace ConsoleBoard.NewFolder1
{
    /// <summary>
    /// Объект вмещающий объект интерфейса, который имеет прикрепленную к нему модель данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Frame<T> : Frame
    {
        public T Object { get; set; }

        public Frame(T obj) : base()
        {
            Object = obj;
        }

        //protected abstract void Draw<T>(T obj);
    }

    /// <summary>
    /// Объект обозначающий элемент интерфейса - Frame для краткости
    /// </summary>
    public abstract class Frame : IDrawable
    {
        private Frame _parent;
        /// <summary>
        /// Прямоугольник вмещающий элемент. Отсчет позици относительно вмещающего фрейма
        /// </summary>
        public virtual CRectangle Rect { get; set; } = new CRectangle();

        /// <summary>
        /// Прямоугольник вмещающий элемент. Отсчет позиции в глобальных консольных координатах
        /// </summary>
        protected CPoint AbsolutePosition {
            get
            {
                var result = new CPoint(Rect.Position.X, Rect.Position.Y);

                if (Parent != null)
                    result += Parent.AbsolutePosition;

                return result;
            }
        }


        /// <summary>
        /// Родителський фрейм
        /// </summary>
        public virtual Frame Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /// <summary>
        /// Коллекция дочерних фреймов
        /// </summary>
        public virtual ChildrenFrameCollection Children { get; set; }

        /// <summary>
        /// Координаты курсора относительно правого верхнего угла элемента
        /// </summary>
        protected virtual CPoint Cursor { get; set; }

        /// <summary>
        /// Глобальные координаты курсора
        /// </summary>
        private CPoint GlobalCursor => new CPoint(Console.CursorLeft, Console.CursorTop);

        public Frame()
        {
            Children = new ChildrenFrameCollection(this);

            Initialize();
        }
        
        /// <summary>
        /// Инициализация базовыми значениями может проивзодится здесь. Метод вызывается из базового конструктора класса
        /// </summary>
        protected virtual void Initialize()
        {
            
        }
        
        /// <summary>
        /// Отрисовка элемента
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Очищает область элемента
        /// </summary>
        public void Clear()
        {
            string emptyString = new string(' ', Rect.Width);

            //Console.SetCursorPosition(Position.X, Position.Y);
            for (int i = 0; i < Rect.Width; i++)
            {
                var absCursor = AbsolutePosition;
                Console.SetCursorPosition(absCursor.X, absCursor.Y + i);
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
            if (top > Rect.Width)
                top = 0;
            if (left > Rect.Height)
                left = 0;

            // обновляем текущее положение курсора (зачем?)
            //Cursor = new CPoint(left, top);

            Cursor = point;

            //// если есть родитель, сдвигаем курсор также на абсолютное положение родительского фрейма
            //if (Parent != null)
            //    cursorPostiion += this.AbsolutePosition;
            

            // результирующий курсор - сдвиг относительно верхнего левого угла элемента
            var resultGlobalCursor = Cursor + this.AbsolutePosition;
            Console.SetCursorPosition(resultGlobalCursor.X, resultGlobalCursor.Y);
        }

        //private static void DrawEventHandler(object sender, EventArgs e)
        //{
        //    Console.ResetColor();
        //}

        
    }
}