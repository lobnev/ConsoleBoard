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

    public class Panel<T> : Frame
    {
        public T Object { get; set; }

        //public List<Frame> Elements { get; set; }



        protected override void Initialize()
        {
            // все элементы теперь часть панели
            // переделываем их их координаты относительно панели

            foreach(var уд in Elements)


            base.Initialize();
        }

        public override void Draw()
        {
            // если не лежит - уменьшать его размеры пока полностью не влезет в панель
            // TODO: временно (и надолго =)) выбрасываем исключение
            foreach (var el in Elements)
            {
                // проверяем лежит ли элемент внутри панели.
                if (IsInPanelArea(el) == false)
                    throw new DrawException(
                        $"Element '{el.GetType()}' is`n fit panel for '{this.Object.GetType()}' object");

                // забиваем хер, на то что они могут перекрывать друг друга
                el.Draw();
            }

        }

        /// <summary>
        /// Помещается ли элемент полностью в данную панель?
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private bool IsInPanelArea(Frame element)
        {
            var leftTop = this.Position;
            var rightBottom = this.Position + this.Size;

            // элемент помещается, если его две определяющие точки лежат внутри панели
            if (IsInArea(leftTop, rightBottom, element.Position)
                && IsInArea(leftTop, rightBottom, element.Position + element.Size))
                return true;

            return false;
        }

        /// <summary>
        /// проверяет лежит ли точка в прямоугольнике заданном двумя точками (very old legacy. Haha, i was so young. So cute =))
        /// </summary>
        /// <param name="leftTopAngle">Верхний левый угол прямоугольника</param>
        /// <param name="rightBottomAngle">Нижний правый угол прямоугольника</param>
        /// <param name="point"></param>
        private static bool IsInArea(CPoint leftTopAngle, CPoint rightBottomAngle, CPoint point)  //
        {
            float x_max = Math.Max(leftTopAngle.X, rightBottomAngle.X);
            float x_min = Math.Min(leftTopAngle.X, rightBottomAngle.X);
            float y_max = Math.Max(leftTopAngle.Y, rightBottomAngle.Y);
            float y_min = Math.Min(leftTopAngle.Y, rightBottomAngle.Y);

            if ((point.X > x_min) 
                && (point.X < x_max) 
                && (point.Y > y_min) 
                && (point.Y < y_max)) return true;
            else return false;
        }
    }
}