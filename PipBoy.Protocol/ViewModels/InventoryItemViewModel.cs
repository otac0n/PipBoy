// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using Newtonsoft.Json;
    using ReactiveUI;

    public class InventoryItemViewModel : BoxedProperties
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
            : base(box)
        {
        }

        [JsonProperty("canFavorite")]
        public bool CanFavorite => this.canFavorite.Value;

        [JsonProperty("count")]
        public int Count => this.count.Value;

        [JsonProperty("equipState")]
        public int EquipState => this.equipState.Value;

        [JsonProperty("favorite")]
        public int Favorite => this.favorite.Value;

        [JsonProperty("isLegendary")]
        public bool IsLegendary => this.isLegendary.Value;

        [JsonProperty("isPowerArmorItem")]
        public bool IsPowerArmorItem => this.isPowerArmorItem.Value;

        [JsonProperty("taggedForSearch")]
        public bool TaggedForSearch => this.taggedForSearch.Value;

        [JsonProperty("text")]
        public string Text => this.text.Value;
    }
}
