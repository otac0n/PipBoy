namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class PortalMapMarkerViewModel : MapMarkerViewModel
    {
        private readonly ObservableAsPropertyHelper<bool?> clearedStatus;
        private readonly ObservableAsPropertyHelper<bool?> discovered;
        private readonly ObservableAsPropertyHelper<string> name;
        private readonly ObservableAsPropertyHelper<int?> workshopHappinessPct;
        private readonly ObservableAsPropertyHelper<bool?> workshopOwned;
        private readonly ObservableAsPropertyHelper<int?> workshopPopulation;

        public PortalMapMarkerViewModel(Box box)
            : base(box)
        {
        }

        public bool? ClearedStatus => this.clearedStatus.Value;

        public bool? Discovered => this.discovered.Value;

        public string Name => this.name.Value;

        public int? WorkshowHappinessPct => this.workshopHappinessPct.Value;

        public bool? WorkshowOwned => this.workshopOwned.Value;

        public int? WorkshowPopulation => this.workshopPopulation.Value;
    }
}
