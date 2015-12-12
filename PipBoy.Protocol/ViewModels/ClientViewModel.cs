// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System;
    using System.Net;
    using ReactiveUI;

    public class ClientViewModel : ReactiveObject, IDisposable
    {
        private readonly CommandInterpreter connection;
        private readonly ObservableAsPropertyHelper<ServerVersion> serverVersion;

        public ClientViewModel(EndPoint endPoint)
        {
            this.connection = new CommandInterpreter(endPoint);

            this.connection
                .WhenAny(x => x.ServerVersion, x => x.Value)
                .ToProperty(this, x => x.ServerVersion, out this.serverVersion);

            this.GameInfo = new GameInfoViewModel(this.connection.ServerViewModel.Root);
        }

        public ServerVersion ServerVersion => this.serverVersion.Value;

        public GameInfoViewModel GameInfo { get; }

        public void Dispose()
        {
            this.connection.Dispose();
        }
    }
}
