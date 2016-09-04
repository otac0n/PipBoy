// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using Newtonsoft.Json;
    using ReactiveUI;

    public class PortalMapMarkerViewModel : MapMarkerViewModel
    {
        private readonly ObservableAsPropertyHelper<bool?> clearedStatus;
        private readonly ObservableAsPropertyHelper<bool?> discovered;
        private readonly ObservableAsPropertyHelper<int> locationFormId;
        private readonly ObservableAsPropertyHelper<int> locationMarkerFormId;
        private readonly ObservableAsPropertyHelper<string> name;
        private readonly ObservableAsPropertyHelper<int> type;
        private readonly ObservableAsPropertyHelper<int?> workshopHappinessPct;
        private readonly ObservableAsPropertyHelper<bool?> workshopOwned;
        private readonly ObservableAsPropertyHelper<int?> workshopPopulation;

        public PortalMapMarkerViewModel(Box box)
            : base(box)
        {
        }

        public bool? ClearedStatus => this.clearedStatus.Value;

        public bool? Discovered => this.discovered.Value;

        public int LocationFormId => this.locationFormId.Value;

        public int LocationMarkerFormId => this.locationMarkerFormId.Value;

        public string Name => this.name.Value;

        [JsonProperty("type")]
        public int Type => this.type.Value;

        public int? WorkshopHappinessPct => this.workshopHappinessPct.Value;

        public bool? WorkshopOwned => this.workshopOwned.Value;

        public int? WorkshopPopulation => this.workshopPopulation.Value;
    }
}
