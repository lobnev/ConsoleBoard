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

            var test = new TestDto()
            {
                SpecName = "test"
            };
            var testPanel = new Panel<TestDto>(test);
            testPanel.RelativeRect = new CRectangle(10, 30, 60, 3);

            var testName = new Indicator<TestDto>(test, t => t.SpecName);
            testName.RelativeRect = new CRectangle(0, 1, 30, 1);
            var testStatus = new Indicator<TestDto>(test, t => t.Status.ToString());
            testStatus.RelativeRect = new CRectangle(45, 1, 10, 1);
            var testResult = new Indicator<TestDto>(test, t => t.Result.ToString());
            testResult.RelativeRect = new CRectangle(70, 1, 10, 1);

            testPanel.Childrens.Add(testName);
            testPanel.Childrens.Add(testStatus);
            testPanel.Childrens.Add(testResult);

            testPanel.Draw();

            Console.ReadKey();
        }

        private void InitialTest()
        {
            var label = new Label();
            label.RelativeRect.Position = new CPoint(3, 2);
            label.RelativeRect.Width = 2;
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
            indicator.RelativeRect.Position = new CPoint(7, 10);
            indicator.RelativeRect.Width = 10;
            indicator.RelativeRect.Height = 1;
            indicator.Draw();

            test.Status = TestStatus.Finished;
            indicator.Draw();

            Console.ReadKey();
        }
    }
}
