﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using ConsoleBoard.Exceptions;
using ConsoleBoard.Frame;
using ConsoleBoard.Helpers;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Table<T> : Frame.Frame
    {
        public List<T> Objects { get; set; }

        /// <summary>
        /// Заголовок таблицы
        /// </summary>
        //public Panel<T> Header { get; set; }

        /// <summary>
        /// Строки таблицы
        /// </summary>
        //public List<Panel<T>> Rows { get; set; }

        /// <summary>
        /// Модель строки
        /// </summary>
        private Panel<T> RowModel { get; set; }

        public int RowDistance { get; set; }

        private List<string> HeaderStrings { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objects"></param>
        /// <param name="rowHeight">Ширина строки. Если ноль, строка занимает всю консоль</param>
        /// <param name="rowWidth">Ширина строки. Если ноль, строка занимает всю консоль</param>
        public Table(List<T> objects, Panel<T> rowModel, List<string> header, int rowDistance = 1)
        {
            //Objects = objects;
            this.HeaderStrings = header;
            this.RowDistance = rowDistance;
            this.RowModel = rowModel;
            //this.Rect = new CRectangle(0,0, RowModel.Rect.Width, rowModel.Rect.Height * (objects.Count + 1));

            SetObjectLists(Objects);
            //var rows = ConstructRowsFromModel(this, RowModel, Objects, RowDistance);
            //var Header = ConstructHeader(this, RowModel, HeaderStrings);

            //Content.Add(Header);
            //foreach (var row in rows)
            //{
            //    Content.Add(row);
            //}


        }

        /// <summary>
        /// Конструирует заголовочную строку
        /// </summary>
        /// <param name="rowModel"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static Panel<T> ConstructHeader(Frame.Frame parent, Panel<T> rowModel, List<string> header)
        {
            if (rowModel.Content.Count != header.Count)
                throw new TableConstructionException(
                    $"Row model children count ({rowModel.Content.Count}) is not equla to header count ({header.Count})");

            var headerPanel = new Panel<T>(default(T));
            headerPanel.Rect = new CRectangle(0, 0, rowModel.Rect.Width, rowModel.Rect.Height);
            headerPanel.Parent = parent;

            for (int i = 0; i < rowModel.Content.Count; i++)
            {
                var headerCell = new Label(header[i]);
                headerCell.Rect = rowModel.Content.ElementAt(i).Rect;
                headerPanel.Content.Add(headerCell);
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
        private static List<Panel<T>> ConstructRowsFromModel(Frame.Frame parent, Panel<T> model, List<T> objects, int rowDistance)
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
                foreach (var child in model.Content)
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

                    objPanel.Content.Add((Frame.Frame) childCopy);
                }

                //if (previosRow != null)
                //    objPanel.Rect.Position = new CPoint(0, previosRow.Rect.Position.Y + rowHeight);
                //else objPanel.Rect.Position = new CPoint(0, 0);

                result.Add(objPanel);

            }
            return result;
        }

        public void SetObjectLists(List<T> objectsToDraw)
        {
            Objects.Clear();
            Objects = objectsToDraw;
            this.Rect = new CRectangle(Rect.Position.X, Rect.Position.Y, RowModel.Rect.Width, RowModel.Rect.Height * (objectsToDraw.Count + 1));

            Content.Clear();

            var rows = ConstructRowsFromModel(this, RowModel, Objects, RowDistance);
            var Header = ConstructHeader(this, RowModel, HeaderStrings);

            Content.Add(Header);
            foreach (var row in rows)
            {
                Content.Add(row);
            }
        }

        //public override void Draw()
        //{
        //    //throw new System.NotImplementedException();
        //    //Header.Draw();
        //    //Rows.ForEach(r => r.Draw());

        //    //var amount = this.Objects.Count;

        //    //foreach (var row in Rows)
        //    //{
        //    //    row.Draw();
        //    //}

        //    base.Draw();
        //}

    }
}