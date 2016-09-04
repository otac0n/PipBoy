// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class MapViewModel : BoxedProperties
    {
        private readonly ObservableAsPropertyHelper<string> currCell;
        private readonly ObservableAsPropertyHelper<string> currWorldspace;
        private readonly ObservableAsPropertyHelper<LocalMapScopeViewModel> local;
        private readonly ObservableAsPropertyHelper<WorldMapScopeViewModel> world;

        public MapViewModel(Box box)
            : base(box)
        {
        }

        public string CurrCell => this.currCell.Value;

        public string CurrWorldspace => this.currWorldspace.Value;

        public LocalMapScopeViewModel Local => this.local.Value;

        public WorldMapScopeViewModel World => this.world.Value;
    }
}
