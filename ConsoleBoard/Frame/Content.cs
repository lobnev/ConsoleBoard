using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleBoard.Exceptions;
using ConsoleBoard.Helpers;

namespace ConsoleBoard.Frame
{
    ///// <summary>
    ///// Коллекция Фреймов
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    //public class ChildrenFrameCollection<T> : ChildrenFrameCollection
    //{
    //    public new Frame<T> Parent { get; set; }
        
    //    private new List<Frame<T>> _elementCollection = new List<Frame<T>>();

    //    public ChildrenFrameCollection(Frame<T> parent) : base(parent)
    //    {
    //    }
    //}

    /// <summary>
    /// Коллекция фреймов
    /// </summary>
    public class Content : ICollection<Frame>
    {
        public Frame Parent { get; set; }

        public int Count => _elementCollection.Count;
        public bool IsReadOnly { get; } = false;

        protected List<Frame> _elementCollection = new List<Frame>();

        public event EventHandler ElementAdded = delegate { };

        public Content(Frame parent)
        {
            Parent = parent;
        }

        public IEnumerator<Frame> GetEnumerator()
        {
            return _elementCollection.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Frame item)
        {
            item.Parent = Parent;
            _elementCollection.Add(item);

            ElementAdded(this, EventArgs.Empty);
            //throw new NotImplementedException();
            // TODO: если элемент не влезает в родителя, то временно (и надолго =)) выбрасываем исключение
            if (true)//IsElementFitIn(Parent, item))
            {
                
            }
            else
                throw new DrawException(
                       $"Element '{item.GetType()}:{item.Rect}' isn`t fit to element '{Parent.GetType()}:{item.Rect}' object");

        }
        public void Clear()
        {
            _elementCollection.Clear();
        }
        public bool Contains(Frame item)
        {
            if (_elementCollection.Contains(item))
                return true;
            return false;
        }
        public void CopyTo(Frame[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(Frame item)
        {
            item.Parent = null;
            return _elementCollection.Remove(item);
        }


        /// <summary>
        /// TODO: Помещается ли внутренний элемент полностью в данный родительский?
        /// </summary>
        /// <param name="innerElement">внутренний элемент, </param>
        /// <returns></returns>
        private static bool IsElementFitIn(Frame parentElement, Frame innerElement)
        {
            
            //if(parentElement == null)

            // определяем прямугольник родительского объекта
            var leftTop = parentElement.Rect.Position;
            var rightBottom = parentElement.Rect.RightBottom;

            // элемент помещается, если его две определяющие точки лежат внутри панели
            if (IsPointInArea(leftTop, rightBottom, innerElement.Rect.Position)
                && IsPointInArea(leftTop, rightBottom, innerElement.Rect.RightBottom))
                return true;

            return false;
        }

        /// <summary>
        /// проверяет лежит ли точка в прямоугольнике заданном двумя точками (very old legacy. Haha, i was so young. So cute =))
        /// </summary>
        /// <param name="leftTopAngle">Верхний левый угол прямоугольника</param>
        /// <param name="rightBottomAngle">Нижний правый угол прямоугольника</param>
        /// <param name="point"></param>
        private static bool IsPointInArea(CPoint leftTopAngle, CPoint rightBottomAngle, CPoint point)  //
        {
            float x_max = Math.Max(leftTopAngle.X, rightBottomAngle.X);
            float x_min = Math.Min(leftTopAngle.X, rightBottomAngle.X);
            float y_max = Math.Max(leftTopAngle.Y, rightBottomAngle.Y);
            float y_min = Math.Min(leftTopAngle.Y, rightBottomAngle.Y);

            if ((point.X > x_min)
                && (point.X < x_max)
                && (point.Y > y_min)
                && (point.Y < y_max))
                return true;
            else return false;
        }
    }
}