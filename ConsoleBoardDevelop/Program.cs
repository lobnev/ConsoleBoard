using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBoard;
using ConsoleBoard.BaseInterfaceElements;
using ConsoleBoard.NewFolder1;
using iXenter.DTO;

namespace ConsoleBoardDevelop
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleTools.SetConsoleWindowPosition(0, 0);
            Console.SetWindowSize(180, 80);


            Console.ReadKey();
        }

        private void InitialTest()
        {
            var label = new Label();
            label.Position = new CPoint(3, 2);
            label.Size.Y = 2;
            label.Text = "Hello World! 333333333333333333333333333333333333333333333333333333333333444444444444444444444444444444444444";
            label.Font.Background = ConsoleColor.DarkBlue;
            label.Font.TextColor = ConsoleColor.DarkGreen;
            label.Draw();
            //label.Clear();

            var test = new TestDto() { Status = TestStatus.Running };
            var indicator = new Indicator<TestDto>(test, t => t.Status.ToString(), t =>
            {
                if (t.Status == TestStatus.Running)
                    return new Font(ConsoleColor.DarkBlue);
                if (t.Status == TestStatus.Finished)
                    return new Font(ConsoleColor.DarkGreen);
                else return new Font();
            });
            indicator.Position = new CPoint(7, 10);
            indicator.Size = new CPoint(10, 1);
            indicator.Draw();

            test.Status = TestStatus.Finished;
            indicator.Draw();

            Console.ReadKey();
        }
    }
}
