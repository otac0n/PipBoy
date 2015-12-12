namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class MapViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<string> currCell;
        private readonly ObservableAsPropertyHelper<string> currWorldspace;
        private readonly ObservableAsPropertyHelper<MapScopeViewModel> local;
        private readonly ObservableAsPropertyHelper<MapScopeViewModel> world;

        public MapViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.CurrCell, out this.currCell);
            properties.ToBoxedProperty(this, x => x.CurrWorldspace, out this.currWorldspace);
            properties.ToBoxedProperty(this, x => x.Local, out this.local, b => new MapScopeViewModel(b, "Doors"));
            properties.ToBoxedProperty(this, x => x.World, out this.world, b => new MapScopeViewModel(b, "Locations"));
        }

        public string CurrCell => this.currCell.Value;

        public string CurrWorldspace => this.currWorldspace.Value;

        private MapScopeViewModel Local => this.local.Value;

        private MapScopeViewModel World => this.world.Value;
    }
}
