using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using ConsoleBoard.Exceptions;
using ConsoleBoard.NewFolder1;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Table<T> : Frame
    {
        public List<T> Objects { get; set; }

        /// <summary>
        /// Заголовок таблицы
        /// </summary>
        public Panel<T> Header { get; set; }

        /// <summary>
        /// Строки таблицы
        /// </summary>
        public List<Panel<T>> Rows { get; set; }

        /// <summary>
        /// Модель строки
        /// </summary>
        private Panel<T> RowModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="rowHeight">Ширина строки. Если ноль, строка занимает всю консоль</param>
        /// <param name="rowWidth">Ширина строки. Если ноль, строка занимает всю консоль</param>
        public Table(List<T> objects, Panel<T> rowModel, List<string> header, int rowDistance = 1)
        {
            Objects = objects;
            this.RowModel = rowModel;
            this.Rect = new CRectangle(0,0, RowModel.Rect.Width, rowModel.Rect.Height * (objects.Count + 1));

            
            Rows = ConstructRowsFromModel(this, RowModel, Objects, rowDistance);
            Header = ConstructHeader(this, RowModel, header);
        }

        /// <summary>
        /// Конструирует заголовочную строку
        /// </summary>
        /// <param name="rowModel"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static Panel<T> ConstructHeader(Frame parent, Panel<T> rowModel, List<string> header)
        {
            if (rowModel.Children.Count != header.Count)
                throw new TableConstructionException(
                    $"Row model children count ({rowModel.Children.Count}) is not equla to header count ({header.Count})");

            var headerPanel = new Panel<T>(default(T));
            headerPanel.Rect = new CRectangle(0, 0, rowModel.Rect.Width, rowModel.Rect.Height);
            headerPanel.Parent = parent;

            for (int i = 0; i < rowModel.Children.Count; i++)
            {
                var headerCell = new Label(header[i]);
                headerCell.Rect = rowModel.Children.ElementAt(i).Rect;
                headerPanel.Children.Add(headerCell);
            }

            return headerPanel;
        }

        /// <summary>
        /// Конструирует список панелей - строки
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="model"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        private static List<Panel<T>> ConstructRowsFromModel(Frame parent, Panel<T> model, List<T> objects, int rowDistance)
        {
            int currentTop = model.Rect.Height;
            var result = new List<Panel<T>>();
            foreach (var obj in objects)
            {
                //var previosRow = Rows.LastOrDefault();

                var objPanel = new Panel<T>(obj);
                objPanel.Rect = new CRectangle(0, currentTop, model.Rect.Width, model.Rect.Height);
                currentTop += model.Rect.Height + rowDistance;

                objPanel.Parent = parent;

                // смотрим все дочерние объекты модели и присваиваем им объект для отображения
                foreach (var child in model.Children)
                {
                    // создаем копию объекта
                    var childType = child.GetType();
                    var childCopy = FormatterServices.GetUninitializedObject(childType);

                    var properties = childType.GetProperties();
                    foreach (var property in properties)
                    {
                        var childPropertyValue = property.GetValue(child);

                        if (property.Name == nameof(Frame<T>.Object))
                            property.SetValue(childCopy, obj);
                        else property.SetValue(childCopy, childPropertyValue);
                    }

                    objPanel.Children.Add((Frame) childCopy);
                }

                //if (previosRow != null)
                //    objPanel.Rect.Position = new CPoint(0, previosRow.Rect.Position.Y + rowHeight);
                //else objPanel.Rect.Position = new CPoint(0, 0);

                result.Add(objPanel);

            }
            return result;
        }

        public override void Draw()
        {
            //throw new System.NotImplementedException();
            Header.Draw();
            //Rows.ForEach(r => r.Draw());

            foreach (var row in Rows)
            {
                row.Draw();
            }
        }
    }
}