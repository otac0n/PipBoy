namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class MapScopeViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<MapMarkerViewModel> custom;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<PortalMapMarkerViewModel>> portals;
        private readonly ObservableAsPropertyHelper<MapMarkerViewModel> powerArmor;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<QuestMapMarkerViewModel>> quests;

        public MapScopeViewModel(Box box, string portalPropertyName)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Custom, out this.custom, b => new MapMarkerViewModel(b));
            properties.ToBoxedListProperty(this, x => x.Portals, out this.portals, b => new PortalMapMarkerViewModel(b), portalPropertyName);
            properties.ToBoxedProperty(this, x => x.PowerArmor, out this.powerArmor, b => new MapMarkerViewModel(b));
            properties.ToBoxedListProperty(this, x => x.Quests, out this.quests, b => new QuestMapMarkerViewModel(b));
        }

        public MapMarkerViewModel PowerArmor => this.powerArmor.Value;

        public ObservableBoxedList<PortalMapMarkerViewModel> Portals => this.portals.Value;

        public MapMarkerViewModel Custom => this.custom.Value;

        public ObservableBoxedList<QuestMapMarkerViewModel> Quests => this.quests.Value;
    }
}
