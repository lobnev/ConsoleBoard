using System;
using System.Linq;
using ConsoleBoard.Helpers;
using Pie;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Label : Frame.Frame
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
            Rect = new CRectangle(0,0,30,1);
        }
        

        public override void Draw()
        {
            // полный текст фрагмента
            string textToDraw = Text;

            if (textToDraw == null)
                textToDraw = "";

            // допустимыое количество символов (включая перенос на строчки)
            int maxTextLength = Rect.Width * Rect.Height;

            // сравниваем количество символов, и отсекаем те - которые не влезут в допустимый прямоугольник
            if (textToDraw.Length > maxTextLength)
                textToDraw = textToDraw.Substring(0, maxTextLength);
            
            // разбиваем фрагмент на подстроки - такой длины чтобы каждый уместился по ширине в "ячейку" (MaxLength)
            var textInStrokes = LINQExtension.SeparateArrayToArrays(textToDraw.ToCharArray().ToList(), Rect.Width);
            
            MoveCursor(new CPoint(0,0));
            foreach (var textStroke in textInStrokes)
            {
                Console.BackgroundColor = Font.Background;
                Console.ForegroundColor = Font.TextColor;
                Console.Write(string.Join("", textStroke));
                MoveCursor(new CPoint(0, Cursor.Y + 1));

                // TODO: в идеале - сбрасывать настройки цвета должна какой-то внешний объект
                Console.ResetColor();
            }

            //DrawEvent(this, EventArgs.Empty);
        }
    }
}