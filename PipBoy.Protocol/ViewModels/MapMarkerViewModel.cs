namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class MapMarkerViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<int> height;
        private readonly ObservableAsPropertyHelper<bool> visible;
        private readonly ObservableAsPropertyHelper<int> x;
        private readonly ObservableAsPropertyHelper<int> y;

        public MapMarkerViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Height, out this.height);
            properties.ToBoxedProperty(this, x => x.Visible, out this.visible);
            properties.ToBoxedProperty(this, x => x.X, out this.x);
            properties.ToBoxedProperty(this, x => x.Y, out this.y);
        }

        public int Height => this.height.Value;

        public bool Visible => this.visible.Value;

        public int X => this.x.Value;

        public int Y => this.y.Value;
    }
}
