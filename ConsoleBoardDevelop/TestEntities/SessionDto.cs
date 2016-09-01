using System.Collections.Generic;
using System.Linq;

namespace iXenter.DTO
{
    /// <summary>
    /// Описывает текущую запущенную на выполнение тестировочную сессию
    /// </summary>
    public class SessionDto
    {
        public int Id { get; set; }
        public List<TestDto> TestProcesses { get; set; }
        public SessionResult Result { get; set; }
        public SessionStatus Status { get; set; }
    }

    public enum SessionStatus
    {
        Idle, Launching, Running, Finished
    }

    public enum SessionResult
    {
        Unknown, Success, Failed, Aborted
    }
}
