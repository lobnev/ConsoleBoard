using System;

namespace iXenter.DTO
{
    /// <summary>
    /// Сообщение, которые тестер может отправить
    /// </summary>
    public class MessageDto
    {
        public MessageDto()
        {
        }
        public MessageDto(string text, MessageLevel level, int testProcessId)
        {
            Text = text;
            Level = level;
            TestProcessId = testProcessId;
        }

        public int Id { get; set; }

        public string Text { get; set; }
        public string JsonObject { get; set; } = "";
        public MessageLevel Level { get; set; }
        public int TestProcessId { get; set; }

        /// <summary>
        /// время отправки сообщения
        /// </summary>
        public DateTime CreateTime { get; set; }
        
    }

    public enum MessageLevel
    {
        Trace = 0,
        Info = 2,
        Warn = 3,
        Error = 4,
        FatalError = 5
    }
}