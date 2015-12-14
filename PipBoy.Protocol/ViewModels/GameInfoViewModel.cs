// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using ReactiveUI;

    public class GameInfoViewModel : BoxedProperties
    {
        protected readonly ObservableAsPropertyHelper<InventoryViewModel> inventory;
        protected readonly ObservableAsPropertyHelper<MapViewModel> map;
        protected readonly ObservableAsPropertyHelper<PlayerInfoViewModel> playerInfo;

        public GameInfoViewModel(Box box)
            : base(box)
        {
        }

        public InventoryViewModel Inventory => this.inventory.Value;

        public MapViewModel Map => this.map.Value;

        public PlayerInfoViewModel PlayerInfo => this.playerInfo.Value;
    }
}
