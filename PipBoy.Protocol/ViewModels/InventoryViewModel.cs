// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using Newtonsoft.Json;
    using ReactiveUI;

    public class InventoryViewModel : BoxedProperties
    {
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> aid;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> ammo;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> apparel;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> holotapes;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> keys;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> misc;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> notes;
        private readonly ObservableAsPropertyHelper<ObservableBoxedList<InventoryItemViewModel>> weapons;

        public InventoryViewModel(Box box)
            : base(box)
        {
        }

        [JsonProperty("48")]
        public ObservableBoxedList<InventoryItemViewModel> Aid => this.aid.Value;

        [JsonProperty("44")]
        public ObservableBoxedList<InventoryItemViewModel> Ammo => this.ammo.Value;

        [JsonProperty("29")]
        public ObservableBoxedList<InventoryItemViewModel> Apparel => this.apparel.Value;

        [JsonProperty("50")]
        public ObservableBoxedList<InventoryItemViewModel> Holotapes => this.holotapes.Value;

        [JsonProperty("47")]
        public ObservableBoxedList<InventoryItemViewModel> Keys => this.keys.Value;

        [JsonProperty("35")]
        public ObservableBoxedList<InventoryItemViewModel> Misc => this.misc.Value;

        [JsonProperty("30")]
        public ObservableBoxedList<InventoryItemViewModel> Notes => this.notes.Value;

        [JsonProperty("43")]
        public ObservableBoxedList<InventoryItemViewModel> Weapons => this.weapons.Value;
    }
}
