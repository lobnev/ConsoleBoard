using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pie;

namespace ConsoleBoard
{
    public class CinematicConsole
    {
        public CinematicConsole()
        {

        }
    }

    public class ConsoleBoard
    {
        public IList<IFrame> Frames { get; set; }

        private int _spaceBetweenFrames = 5;

        public ConsoleBoard(IList<IFrame> frames)
        {
            Frames = frames;
        }

        public void Draw(IEnumerable<IEnumerable> paramForFrames)
        {
            // TODO: Better! Do Better lazy motherfucker!
            if (Frames.Count != paramForFrames.Count())
                throw new Exception();

            Console.Clear();
            for (int i = 0; i < Frames.Count; i++)
            {


                Frames[i].Draw(paramForFrames.ElementAt(i));

                Console.SetCursorPosition(0, Console.CursorTop + _spaceBetweenFrames);
            }
        }
    }


    /// <summary>
    /// Модель описывающая отрисовку 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Frame<T> : IFrame
    {
        public IList<Stroke<T>> Strokes { get; set; } = new List<Stroke<T>>();
        public string Name { get; set; }
        /// <summary>
        /// Разделитель строк
        /// </summary>
        public Stroke<object> Divider { get; set; } = new Stroke<object>()
        {
            Fragments = new List<Fragment<object>>()
            {
                // TODO: не вот эта фигня - а над длинну текущего окна консоли (по аналогии нелпохо бы разделитель фрагментов
                new Fragment<object>((o) => "-----------------------------------------------------------------------------------------------------------------------")
            }
        };

        //public int DistanceBetweenStrokes { get; set; } = 1;

        public void Draw(IEnumerable objects)
        {
            //Console.Clear();

            int curTopOffset = Console.CursorTop;

            // уезжаем вниз на высоту строки
            Console.SetCursorPosition(0, curTopOffset);
            Console.WriteLine(Name);
            Console.Write(Divider.Fragments[0].Text(null));
            curTopOffset += 3 * Divider.Height;

            //var strokesWithDeviders = 

            // рисуем фрейм для всех объектов
            foreach (var obj in objects)
            {

                foreach (var stroke in Strokes)
                {
                    //кусрор на начало следующей строки
                    Console.SetCursorPosition(0, curTopOffset);

                    // рисуем фрагменты в строке
                    int curLeftOffset = 0;
                    foreach (var fragment in stroke.Fragments)
                    {
                        // ставим курсор со смещением относительно начала строки и верхней границы текущей строки
                        Console.SetCursorPosition(curLeftOffset + fragment.LeftOffset, curTopOffset + fragment.TopOffset);

                        // полный текст фрагмента
                        string fragmentText = fragment.Text((T)obj);

                        // допустимыое количество символов (включая перенос на строчки)
                        int maxFragmentLength = fragment.MaxLength * fragment.MaxHeight;

                        // сравниваем количество символов, и отсекаем те - которые не влезут в допустимый прямоугольник
                        if (fragmentText.Length > maxFragmentLength)
                            fragmentText = fragmentText.Substring(0, maxFragmentLength);

                        // разбиваем фрагмент на подстроки - такой длины чтобы каждый уместился по ширине в "ячейку"
                        //int strokeCount = fragmentText.Length/fragment.MaxLength;

                        //var fragmentStrokes = new List<string>();
                        //for (int i = 0; i < fragmentText.Length; i += fragment.MaxLength)
                        //{
                        //    int strokeLength = 
                        //    string fragmentStroke = fragmentText.Substring(i, fragment.MaxLength);
                        //    fragmentStrokes.Add(fragmentStroke);
                        //} 

                        // разбиваем фрагмент на подстроки - такой длины чтобы каждый уместился по ширине в "ячейку" (MaxLength)
                        var fragmentStrokes = LINQExtension.SeparateArrayToArrays(fragmentText.ToCharArray().ToList(), fragment.MaxLength);

                        // смещаемся вверх от центра ячейки на половину количества строчек - для центровки сообщения
                        int fragmentTopOffset = curTopOffset + fragment.TopOffset - fragmentStrokes.Count / 2;

                        // на случай, если подошли в упор к верху в консоли
                        if (fragmentTopOffset < 0)
                            fragmentTopOffset = 0;

                        Console.SetCursorPosition(curLeftOffset + fragment.LeftOffset, fragmentTopOffset);
                        foreach (var textStroke in fragmentStrokes)
                        {
                            Console.SetCursorPosition(curLeftOffset + fragment.LeftOffset, fragmentTopOffset);
                            Console.Write(string.Join("", textStroke));
                            fragmentTopOffset++;
                        }

                        // пишем текст с задаными цветовыми настройками.
                        //Console.Write();

                        // TODO: для длинных строк, должно учитываться ограничение и перенос строк
                        curLeftOffset += fragment.MaxLength + stroke.MinimalSpaceBetweenFragments;
                    }

                    // уезжаем вниз на высоту строки
                    curTopOffset += stroke.Height;// + Divider.Height;

                    Console.SetCursorPosition(0, curTopOffset);
                    Console.Write(Divider.Fragments[0].Text(null));
                    curTopOffset += 3 * Divider.Height;

                }

            }
        }
    }

    public interface IFrame
    {
        string Name { get; set; }
        void Draw(IEnumerable objects);
    }

    /// <summary>
    /// Набор объектов для вывода и которые будут перерисовываться 
    /// </summary>
    public class Stroke<T>
    {
        //public T Object { get; set; }
        public IList<Fragment<T>> Fragments { get; set; } = new List<Fragment<T>>();

        public int MinimalSpaceBetweenFragments { get; set; } = 3;
        public int Height => Fragments.Max(f => f.MaxHeight + f.TopOffset);
    }

    /// <summary>
    /// Фрагмент фрейма который будет отрисовываться. Содержит в себе данные для вывода
    /// В сущности прямоугольник ограничивающий длинну выводимого текста
    /// </summary>
    public class Fragment<T>
    {
        public int LeftOffset { get; set; } = 0;
        public int TopOffset { get; set; } = 0;

        public int MaxHeight { get; set; } = 1;
        public int MaxLength { get; set; } = 15;

        public Func<T, Font> Font { get; set; } = (t) => new Font();
        private Func<T, string> _text { get; set; }

        public Fragment(Func<T, string> text)
        {
            _text = text;
        }

        public Fragment(Func<T, string> text, Func<T, Font> font)
        {
            _text = text;
            Font = font;
        }

        //public Fragment(Func<T, string> text, Dictionary<object> )
        //{
        //    _text = text;
        //    Font = font;
        //}

        public string Text(T obj)
        {
            Font(obj).SetFont();
            return _text(obj);
        }
    }

    public class Font
    {
        public ConsoleColor TextColor { get; set; } = ConsoleColor.White;
        public ConsoleColor Background { get; set; } = ConsoleColor.Black;

        public Font()
        {
        }
        public Font(ConsoleColor background, ConsoleColor textColor = ConsoleColor.White)
        {
            Background = background;
            TextColor = textColor;
        }

        /// <summary>
        /// Устанавливает текущий фонт в качестве рабочего
        /// </summary>
        public void SetFont()
        {
            Console.ForegroundColor = TextColor;
            Console.BackgroundColor = Background;
        }
    }
}