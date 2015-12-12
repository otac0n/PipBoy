namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
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
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.ClearedStatus, out this.clearedStatus);
            properties.ToBoxedProperty(this, x => x.Discovered, out this.discovered);
            properties.ToBoxedProperty(this, x => x.Name, out this.name);
            properties.ToBoxedProperty(this, x => x.WorkshowHappinessPct, out this.workshopHappinessPct);
            properties.ToBoxedProperty(this, x => x.WorkshowOwned, out this.workshopOwned);
            properties.ToBoxedProperty(this, x => x.WorkshowPopulation, out this.workshopPopulation);
        }

        public bool? ClearedStatus => this.clearedStatus.Value;

        public bool? Discovered => this.discovered.Value;

        public string Name => this.name.Value;

        public int? WorkshowHappinessPct => this.workshopHappinessPct.Value;

        public bool? WorkshowOwned => this.workshopOwned.Value;

        public int? WorkshowPopulation => this.workshopPopulation.Value;
    }
}
