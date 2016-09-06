using System;
using ConsoleBoard.Helpers;

namespace ConsoleBoard.BaseInterfaceElements
{
    /// <summary>
    /// Элемент, который выводит текст с параметрами зависящими от состояния объекта
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Indicator<T> : Label
    {
        /// <summary>
        /// Объект, который описывает индикатор
        /// </summary>
        public T Object { get; set; }

        /// <summary>
        /// Функция возвращающая Шрифт с которым выводится текст
        /// </summary>
        public new Func<T, Font> Font { get; set; } = (t) => new Font();

        /// <summary>
        /// Функция возвращающая текст для вывода
        /// </summary>
        public new Func<T, string> Text { get; set; } = t => "";

        //public new()
        //public CPoint Position { get; set; }
        //public new CPoint Size { get; set; }

        //-----------------------------CONSTRUCTORS-----------------------------------
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Объект, который описывает индикатор</param>
        public Indicator(T obj)
        {
            Object = obj;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Объект, который описывает индикатор</param>
        /// <param name="textResolveFunc">Функция возвращающая текст для вывода</param>
        public Indicator(T obj, Func<T,string> textResolveFunc)
        {
            Object = obj;
            Text = textResolveFunc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">Объект, который описывает индикатор</param>
        /// <param name="textResolveFunc">Функция возвращающая текст для вывода</param>
        /// <param name="fontResolveFunc">Функция возвращающая Шрифт с которым выводится текст</param>
        public Indicator(T obj, Func<T, string> textResolveFunc, Func<T, Font> fontResolveFunc)
        {
            Object = obj;
            Text = textResolveFunc;
            Font = fontResolveFunc;
        }


        public override void Draw()
        {
            base.Text = this.Text(Object);
            base.Font = this.Font(Object);
            //Clear();
            base.Draw();
        }
    }
}