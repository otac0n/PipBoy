// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class InventoryViewModel : ReactiveObject
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
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedListProperty(this, x => x.Aid, out this.aid, b => new InventoryItemViewModel(b), "48");
            properties.ToBoxedListProperty(this, x => x.Ammo, out this.ammo, b => new InventoryItemViewModel(b), "44");
            properties.ToBoxedListProperty(this, x => x.Apparel, out this.apparel, b => new InventoryItemViewModel(b), "29");
            properties.ToBoxedListProperty(this, x => x.Holotapes, out this.holotapes, b => new InventoryItemViewModel(b), "50");
            properties.ToBoxedListProperty(this, x => x.Keys, out this.keys, b => new InventoryItemViewModel(b), "47");
            properties.ToBoxedListProperty(this, x => x.Misc, out this.misc, b => new InventoryItemViewModel(b), "35");
            properties.ToBoxedListProperty(this, x => x.Notes, out this.notes, b => new InventoryItemViewModel(b), "30");
            properties.ToBoxedListProperty(this, x => x.Weapons, out this.weapons, b => new InventoryItemViewModel(b), "43");
        }

        public ObservableBoxedList<InventoryItemViewModel> Aid => this.aid.Value;

        public ObservableBoxedList<InventoryItemViewModel> Ammo => this.ammo.Value;

        public ObservableBoxedList<InventoryItemViewModel> Apparel => this.apparel.Value;

        public ObservableBoxedList<InventoryItemViewModel> Holotapes => this.holotapes.Value;

        public ObservableBoxedList<InventoryItemViewModel> Keys => this.keys.Value;

        public ObservableBoxedList<InventoryItemViewModel> Misc => this.misc.Value;

        public ObservableBoxedList<InventoryItemViewModel> Notes => this.notes.Value;

        public ObservableBoxedList<InventoryItemViewModel> Weapons => this.weapons.Value;
    }
}
