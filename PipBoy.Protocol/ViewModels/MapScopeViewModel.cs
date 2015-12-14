namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public abstract class MapScopeViewModel : BoxedProperties
    {
        private readonly ObservableAsPropertyHelper<MapMarkerViewModel> custom;
        private readonly ObservableAsPropertyHelper<MapMarkerViewModel> powerArmor;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<QuestMapMarkerViewModel>> quests;

        public MapScopeViewModel(Box box)
            : base(box)
        {
        }

        public MapMarkerViewModel PowerArmor => this.powerArmor.Value;

        public MapMarkerViewModel Custom => this.custom.Value;

        public ObservableBoxedList<QuestMapMarkerViewModel> Quests => this.quests.Value;
    }
}
