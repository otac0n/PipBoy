using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    public class GameInfoViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<PlayerInfoViewModel> playerInfo;

        public GameInfoViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.PlayerInfo, out playerInfo, b => new PlayerInfoViewModel(b));
        }

        public PlayerInfoViewModel PlayerInfo => this.playerInfo.Value;
    }
}
