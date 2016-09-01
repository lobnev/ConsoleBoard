using ConsoleBoard.NewFolder1;

namespace ConsoleBoard.BaseInterfaceElements
{
    public class Panel<T> : Frame<T>
    {
        //public T Object { get; set; }

        //public List<Frame> Childrens { get; set; }
        

        public override void Draw()
        {
            foreach (var el in Childrens)
            {
                // забиваем хер, на то что они могут перекрывать друг друга
                el.Draw();
            }

        }

        public Panel(T obj) : base(obj)
        {
        }
    }
}