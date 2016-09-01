using System.Collections.Generic;

namespace iXenter.DTO
{
    /// <summary>
    /// Описывает исполняемый тест
    /// </summary>
    public class TestDto
    {
        public int Id { get; set; }

        public TestResult Result { get; set; }
        public TestStatus Status { get; set; }

        /// <summary>
        /// Вывод в конфиг дял Console запуска для удобства юзера
        /// </summary>
        public string SpecName { get; set; }

        public int SessionId { get; set; }
        public int SpecId { get; set; }

        /// <summary>
        /// Сообщения тестера во время выполнения теста
        /// </summary>
        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();

        /// <summary>
        /// История выполнения инструкций
        /// </summary>
        public List<InstructionStatusDto> History { get; set; } = new List<InstructionStatusDto>();
        

    }

    public enum TestStatus
    {
        Idle = 0,
        WaitForLaunch = 1,
        Running = 2,
        Finished = 3,
        Aborted = 4
    }

    public enum TestResult
    {
        Unknown, Success, Failed
    }

    public enum TestEvent
    {
        Started, Message, Finished
    }
}
