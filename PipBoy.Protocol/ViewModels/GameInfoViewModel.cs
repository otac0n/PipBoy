// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System.Collections.Generic;
    using ReactiveUI;

    public class GameInfoViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<InventoryViewModel> inventory;
        private readonly ObservableAsPropertyHelper<MapViewModel> map;
        private readonly ObservableAsPropertyHelper<PlayerInfoViewModel> playerInfo;

        public GameInfoViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Inventory, out this.inventory, b => new InventoryViewModel(b));
            properties.ToBoxedProperty(this, x => x.Map, out this.map, b => new MapViewModel(b));
            properties.ToBoxedProperty(this, x => x.PlayerInfo, out this.playerInfo, b => new PlayerInfoViewModel(b));
        }

        public InventoryViewModel Inventory => this.inventory.Value;

        public MapViewModel Map => this.map.Value;

        public PlayerInfoViewModel PlayerInfo => this.playerInfo.Value;
    }
}
