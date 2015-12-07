using System.Collections.Generic;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    public class PlayerInfoViewModel : ReactiveObject
    {
        private ObservableAsPropertyHelper<string> playerName;

        public PlayerInfoViewModel(Box box)
        {
            var properties = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
            properties.ToBoxedProperty(this, x => x.PlayerName, out playerName, b => b.Value as string);
        }

        public string PlayerName => this.playerName.Value;
    }
}
