namespace PipBoy.Protocol
{
    using ReactiveUI;

    public class Box : ReactiveObject
    {
        private object value;

        public Box(int id)
        {
            this.Id = id;
        }

        public int Id { get; }

        public object Value
        {
            get { return this.value; }
            set { this.RaiseAndSetIfChanged(ref this.value, value); }
        }
    }
}
