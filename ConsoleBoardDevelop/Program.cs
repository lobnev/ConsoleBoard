using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleBoard;
using ConsoleBoard.BaseInterfaceElements;
using ConsoleBoard.Helpers;
using iXenter.DTO;

namespace ConsoleBoardDevelop
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleTools.SetConsoleWindowPosition(0, 0);
            Console.SetWindowSize(180, 80);
            
            // TODO: задача размеров через проценты
            // TODO: возможность обновлять элемент отдельно. Когда они все обновляются консоль как попрыгайчик выглядит
            //var consoleFrame = ConsoleFrame.Current();

            TableTest();

            

            Console.ReadKey();
        }
        private static void TableTest()
        {
            var tests = new List<TestDto>();

            var sampleTest = new TestDto();

            // создаем модель строки
            var rowModel = new Panel<TestDto>(sampleTest);
            rowModel.Rect = new CRectangle(0, 0, 60, 3);

            var testName = new Indicator<TestDto>(sampleTest, t => t.SpecName);
            testName.Rect = new CRectangle(0, 1, 25, 1);
            var testMessage = new Indicator<TestDto>(sampleTest, t => t.Messages.LastOrDefault().Text);
            testMessage.Rect = new CRectangle(30, 1, 60, 3);
            var testStatus = new Indicator<TestDto>(sampleTest, t => t.Status.ToString());
            testStatus.Rect = new CRectangle(115, 1, 10, 1);
            var testResult = new Indicator<TestDto>(sampleTest, t => t.Result.ToString());
            testResult.Rect = new CRectangle(130, 1, 10, 1);

            rowModel.Content.Add(testName);
            rowModel.Content.Add(testMessage);
            rowModel.Content.Add(testStatus);
            rowModel.Content.Add(testResult);

            // TODO: проблема в том, что если сунуть пустой массив, то при его изменении, внутренний объхект Objects таблицы не изменяется. Надо разобраться с этим.

            // создаем таблицу
            var table = new Table<TestDto>(tests, rowModel, new List<string>() {"Spec name", "Message", "Staus", "Result"});
            table.Rect.Position = new CPoint(0, 15);

            tests = Tools.GetTestTestList();

            table.Draw();
            Console.ReadKey();

            table.SetObjectLists(tests);
            

            for (int i = 0; i < 20; i++)
            {
                tests.ForEach(t => t.Messages.Add(new MessageDto() {Text = i.ToString()}));

                tests[3].Messages.Add(new MessageDto() {Text = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111"});

                table.Draw();
                Thread.Sleep(100);
            }
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

            testPanel.Content.Add(testName);
            testPanel.Content.Add(testStatus);
            testPanel.Content.Add(testResult);

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
