using System;
using System.Linq;
using ConsoleBoard.Frame;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Panel<T> : Frame<T>
    {
        public override void Draw()
        {
            foreach (var el in Children)
            {
                // забиваем хер, на то что они могут перекрывать друг друга
                el.Draw();
            }

        }

        public Panel(T obj) : base(obj)
        {
            this.Children.ElementAdded += ChildrenAddedHandler;
        }

        private void ChildrenAddedHandler(object sender, EventArgs e)
        {
            this.Rect.Height = Children.Max(ch => ch.Rect.Height);
        }
    }
}