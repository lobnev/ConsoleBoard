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

            var tests = Tools.GetTestTestList();

            // создаем модель строки
            var rowModel = new Panel<TestDto>(tests[0]);
            rowModel.Rect = new CRectangle(0, 0, 60, 3);

            var testName = new Indicator<TestDto>(tests[0], t => t.SpecName);
            testName.Rect = new CRectangle(0, 1, 25, 1);
            var testMessage = new Indicator<TestDto>(tests[0], t => t.Messages.LastOrDefault().Text);
            testMessage.Rect = new CRectangle(30, 1, 60, 3);
            var testStatus = new Indicator<TestDto>(tests[0], t => t.Status.ToString());
            testStatus.Rect = new CRectangle(115, 1, 10, 1);
            var testResult = new Indicator<TestDto>(tests[0], t => t.Result.ToString());
            testResult.Rect = new CRectangle(130, 1, 10, 1);

            rowModel.Children.Add(testName);
            rowModel.Children.Add(testMessage);
            rowModel.Children.Add(testStatus);
            rowModel.Children.Add(testResult);
            

            // создаем таблицу
            var table = new Table<TestDto>(tests, rowModel, new List<string>() {"Spec name", "Message", "Staus", "Result"});

            table.Draw();

            Console.ReadKey();
        }
        private static void GenericElementsTest()
        {
            var test = new TestDto()
            {
                SpecName = "test"
            };
            var testPanel = new Panel<TestDto>(test);
            testPanel.Rect = new CRectangle(10, 30, 60, 3);

            var testName = new Indicator<TestDto>(test, t => t.SpecName);
            testName.Rect = new CRectangle(0, 1, 30, 1);
            var testStatus = new Indicator<TestDto>(test, t => t.Status.ToString());
            testStatus.Rect = new CRectangle(45, 1, 10, 1);
            var testResult = new Indicator<TestDto>(test, t => t.Result.ToString());
            testResult.Rect = new CRectangle(70, 1, 10, 1);

            testPanel.Children.Add(testName);
            testPanel.Children.Add(testStatus);
            testPanel.Children.Add(testResult);

            testPanel.Draw();
        }

        private void InitialTest()
        {
            var label = new Label();
            label.Rect.Position = new CPoint(3, 2);
            label.Rect.Width = 2;
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
            indicator.Rect.Position = new CPoint(7, 10);
            indicator.Rect.Width = 10;
            indicator.Rect.Height = 1;
            indicator.Draw();

            test.Status = TestStatus.Finished;
            indicator.Draw();

            Console.ReadKey();
        }
    }
}
