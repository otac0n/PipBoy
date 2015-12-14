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

        private LocalMapScopeViewModel Local => this.local.Value;

        private WorldMapScopeViewModel World => this.world.Value;
    }
}
