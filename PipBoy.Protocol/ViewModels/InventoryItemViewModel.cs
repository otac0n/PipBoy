// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class InventoryItemViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<bool> canFavorite;
        private readonly ObservableAsPropertyHelper<int> count;
        private readonly ObservableAsPropertyHelper<int> equipState;
        private readonly ObservableAsPropertyHelper<int> favorite;
        private readonly ObservableAsPropertyHelper<bool> isLegendary;
        private readonly ObservableAsPropertyHelper<bool> isPowerArmorItem;
        private readonly ObservableAsPropertyHelper<bool> taggedForSearch;
        private readonly ObservableAsPropertyHelper<string> text;

        public InventoryItemViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.CanFavorite, out this.canFavorite, "canFavorite");
            properties.ToBoxedProperty(this, x => x.Count, out this.count, "count");
            properties.ToBoxedProperty(this, x => x.EquipState, out this.equipState, "equipState");
            properties.ToBoxedProperty(this, x => x.Favorite, out this.favorite, "favorite");
            properties.ToBoxedProperty(this, x => x.IsLegendary, out this.isLegendary, "isLegendary");
            properties.ToBoxedProperty(this, x => x.IsPowerArmorItem, out this.isPowerArmorItem, "isPowerArmorItem");
            properties.ToBoxedProperty(this, x => x.TaggedForSearch, out this.taggedForSearch, "taggedForSearch");
            properties.ToBoxedProperty(this, x => x.Text, out this.text, "text");
        }

        public bool CanFavorite => this.canFavorite.Value;

        public int Count => this.count.Value;

        public int EquipState => this.equipState.Value;

        public int Favorite => this.favorite.Value;

        public bool IsLegendary => this.isLegendary.Value;

        public bool IsPowerArmorItem => this.isPowerArmorItem.Value;

        public bool TaggedForSearch => this.taggedForSearch.Value;

        public string Text => this.text.Value;
    }
}
