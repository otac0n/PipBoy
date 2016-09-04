// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class WorldMapScopeViewModel : MapScopeViewModel
    {
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<PortalMapMarkerViewModel>> locations;

        public WorldMapScopeViewModel(Box box)
            : base(box)
        {
        }

        public ObservableBoxedList<PortalMapMarkerViewModel> Locations => this.locations.Value;
    }
}
