using System;

namespace iXenter.DTO
{
    public class InstructionStatusDto
    {
        /// <summary>
        /// Номер интсрукции, которая выполнилась
        /// </summary>
        public int InstructionId { get; set; }

        /// <summary>
        /// Номер теста, который исполнил инструкцию
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// Имя инструкции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Объект-результат возвращенный инструкцией при выполнении
        /// </summary>
        public dynamic ReturnedResult { get; set; }

        /// <summary>
        /// Время начала
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Время окончания
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Длительность выполнения инструкции
        /// </summary>
        public TimeSpan Duration => this.EndTime - this.StartTime;

        /// <summary>
        /// Инструкция завершилась с ошибкой?
        /// </summary>
        public bool HasError { get; set; }
    }
}