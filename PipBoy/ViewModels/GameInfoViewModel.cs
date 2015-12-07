using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    public class GameInfoViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<InventoryViewModel> inventory;
        private readonly ObservableAsPropertyHelper<PlayerInfoViewModel> playerInfo;

        public GameInfoViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.Inventory, out inventory, b => new InventoryViewModel(b));
            properties.ToBoxedProperty(this, x => x.PlayerInfo, out playerInfo, b => new PlayerInfoViewModel(b));
        }

        public InventoryViewModel Inventory => this.inventory.Value;

        public PlayerInfoViewModel PlayerInfo => this.playerInfo.Value;
    }
}
