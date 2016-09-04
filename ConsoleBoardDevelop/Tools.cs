using System.Collections.Generic;
using iXenter.DTO;

namespace ConsoleBoardDevelop
{
    public class Tools
    {
        public static List<TestDto> GetTestTestList()
        {
            var test1 = new TestDto()
            {
                SpecName = "test1",
                Result = TestResult.Success,
                Messages = new List<MessageDto>()
                {
                    new MessageDto()
                    {
                        Text =
                            "Big and scary erro message! Arghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh",
                        Level = MessageLevel.Error
                    }
                }
            };
            var test2 = new TestDto()
            {
                SpecName = "test2",
                Status = TestStatus.Running,
                Messages = new List<MessageDto>()
                {
                    new MessageDto() {Text = "Simple info test message!", Level = MessageLevel.Info}
                }
            };
            var test3 = new TestDto()
            {
                SpecName = "test3",
                Result = TestResult.Failed,
                Status = TestStatus.Finished,
                Messages = new List<MessageDto>()
                {
                    new MessageDto()
                    {
                        Text =
                            "Big and scary erro message! Arghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh",
                        Level = MessageLevel.Error
                    }
                }
            };
            var test4 = new TestDto()
            {
                SpecName = "test5",
                Result = TestResult.Failed,
                Status = TestStatus.Finished,
                Messages = new List<MessageDto>()
                {
                    new MessageDto()
                    {
                        Text =
                            "Big and scary erro message! Arghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh",
                        Level = MessageLevel.Error
                    }
                }
            };
            var test5 = new TestDto()
            {
                SpecName = "test6",
                Result = TestResult.Failed,
                Status = TestStatus.Finished,
                Messages = new List<MessageDto>()
                {
                    new MessageDto()
                    {
                        Text =
                            "Big and scary erro message! Arghhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh",
                        Level = MessageLevel.Error
                    }
                }
            };

            var testList = new List<TestDto>() { test1, test2, test3, test4, test5 };
            return testList;
        }
    }
}