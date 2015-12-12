namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class QuestMapMarkerViewModel : MapMarkerViewModel
    {
        private readonly ObservableAsPropertyHelper<string> name;
        private readonly ObservableAsPropertyHelper<bool> onDoor;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<int>> questId;
        private readonly ObservableAsPropertyHelper<bool> shared;

        public QuestMapMarkerViewModel(Box box)
            : base(box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Name, out this.name);
            properties.ToBoxedProperty(this, x => x.Shared, out this.shared);
            properties.ToBoxedProperty(this, x => x.OnDoor, out this.onDoor);
            properties.ToBoxedListProperty(this, x => x.QuestId, out this.questId);
        }

        public string Name => this.name.Value;

        public bool OnDoor => this.onDoor.Value;

        public bool Shared => this.shared.Value;

        public ObservableBoxedList<int> QuestId => this.questId.Value;
    }
}
