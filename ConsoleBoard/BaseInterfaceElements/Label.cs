using System;
using System.Linq;
using ConsoleBoard.NewFolder1;
using Pie;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Label : Frame
    {
        public string Text { get; set; }
        public Font Font { get; set; } = new Font();

        public Label() : base()
        {
            
        }
        public Label(string text) : base()
        {
            Text = text;
        }

        protected override void Initialize()
        {
            Size = new CPoint(30, 1);
        }
        

        public override void Draw()
        {
            // полный текст фрагмента
            string textToDraw = Text;

            // допустимыое количество символов (включая перенос на строчки)
            int maxTextLength = Size.X * Size.Y;

            // сравниваем количество символов, и отсекаем те - которые не влезут в допустимый прямоугольник
            if (textToDraw.Length > maxTextLength)
                textToDraw = textToDraw.Substring(0, maxTextLength);
            
            // разбиваем фрагмент на подстроки - такой длины чтобы каждый уместился по ширине в "ячейку" (MaxLength)
            var textInStrokes = LINQExtension.SeparateArrayToArrays(textToDraw.ToCharArray().ToList(), Size.X);
            
            MoveCursor(0, 0);
            foreach (var textStroke in textInStrokes)
            {
                Console.BackgroundColor = Font.Background;
                Console.ForegroundColor = Font.TextColor;
                Console.Write(string.Join("", textStroke));
                MoveCursor(Cursor.Y + 1, 0);

                // TODO: в идеале - сбрасывать настройки цвета должна какой-то внешний объект
                Console.ResetColor();
            }

            //DrawEvent(this, EventArgs.Empty);
        }
    }
}