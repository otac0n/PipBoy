// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class MapMarkerViewModel : BoxedProperties
    {
        private readonly ObservableAsPropertyHelper<int> height;
        private readonly ObservableAsPropertyHelper<bool> visible;
        private readonly ObservableAsPropertyHelper<int> x;
        private readonly ObservableAsPropertyHelper<int> y;

        public MapMarkerViewModel(Box box)
            : base(box)
        {
        }

        public int Height => this.height.Value;

        public bool Visible => this.visible.Value;

        public int X => this.x.Value;

        public int Y => this.y.Value;
    }
}
