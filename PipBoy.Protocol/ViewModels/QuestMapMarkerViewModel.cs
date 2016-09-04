// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
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
        }

        public string Name => this.name.Value;

        public bool OnDoor => this.onDoor.Value;

        public bool Shared => this.shared.Value;

        public ObservableBoxedList<int> QuestId => this.questId.Value;
    }
}
