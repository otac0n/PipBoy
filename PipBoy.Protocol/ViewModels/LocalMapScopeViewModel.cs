// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class LocalMapScopeViewModel : MapScopeViewModel
    {
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<PortalMapMarkerViewModel>> doors;

        public LocalMapScopeViewModel(Box box)
            : base(box)
        {
        }

        public ObservableBoxedList<PortalMapMarkerViewModel> Doors => this.doors.Value;
    }
}
