using System;
using System.Collections;
using System.Collections.Generic;
using ConsoleBoard.Exceptions;

namespace ConsoleBoard.NewFolder1
{
    /// <summary>
    /// Коллекция фреймов
    /// </summary>
    public class FrameCollection : ICollection<Frame>
    {
        public Frame Parent { get; set; }

        public int Count { get; }
        public bool IsReadOnly { get; }

        private List<Frame> _elementCollection = new List<Frame>();

        public FrameCollection(Frame parent)
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
            //throw new NotImplementedException();

            if (IsElementFitIn(Parent, item))
                _elementCollection.Add(item);
            else
                throw new DrawException(
                       $"Element '{item.GetType()}:Pos{item.Position}:Size{item.Size}' isn`t fit to element '{Parent.GetType()}:Pos{Parent.Position}:Size{Parent.Size}' object");

        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(Frame item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(Frame[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(Frame item)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Помещается ли внутренний элемент полностью в данный родительский?
        /// </summary>
        /// <param name="innerElement">внутренний элемент, </param>
        /// <returns></returns>
        private static bool IsElementFitIn(Frame parentElement, Frame innerElement)
        {
            // определяем прямугольник родительского объекта
            var leftTop = parentElement.Position;
            var rightBottom = parentElement.Position + parentElement.Size;

            // элемент помещается, если его две определяющие точки лежат внутри панели
            if (IsPointInArea(leftTop, rightBottom, innerElement.Position)
                && IsPointInArea(leftTop, rightBottom, innerElement.Position + innerElement.Size))
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