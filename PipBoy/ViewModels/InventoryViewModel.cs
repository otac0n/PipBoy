using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
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
            properties.ToBoxedProperty(this, x => x.Aid, out aid, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "48");
            properties.ToBoxedProperty(this, x => x.Ammo, out ammo, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "44");
            properties.ToBoxedProperty(this, x => x.Apparel, out apparel, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "29");
            properties.ToBoxedProperty(this, x => x.Holotapes, out holotapes, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "50");
            properties.ToBoxedProperty(this, x => x.Keys, out keys, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "47");
            properties.ToBoxedProperty(this, x => x.Misc, out misc, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "35");
            properties.ToBoxedProperty(this, x => x.Notes, out notes, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "30");
            properties.ToBoxedProperty(this, x => x.Weapons, out weapons, b => new ObservableBoxedList<InventoryItemViewModel>(b, b2 => new InventoryItemViewModel(b2)), "43");
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
