using System;
using System.Net;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    public class ClientViewModel : ReactiveObject, IDisposable
    {
        private readonly CommandInterpreter connection;
        private readonly ObservableAsPropertyHelper<ServerVersion> serverVersion;

        public ClientViewModel(EndPoint endPoint)
        {
            this.connection = new CommandInterpreter(endPoint);

            this.connection
                .WhenAny(x => x.ServerVersion, x => x.Value)
                .ToProperty(this, x => x.ServerVersion, out serverVersion);

            this.GameInfo = new GameInfoViewModel(this.connection.ServerViewModel.Root);
        }

        public ServerVersion ServerVersion => serverVersion.Value;

        public GameInfoViewModel GameInfo { get; }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
