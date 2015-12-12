// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol
{
    using System;
    using System.Net;
    using System.Reactive.Linq;
    using Newtonsoft.Json;
    using ReactiveUI;

    internal sealed class CommandInterpreter : ReactiveObject, IDisposable
    {
        private readonly IDisposable lineReader;
        private ServerVersion version;

        public CommandInterpreter(EndPoint endPoint)
        {
            this.ServerViewModel = new ServerViewModel();

            this.lineReader = Observable
                .Create<Tuple<byte, byte[]>>(async (observer, cancel) =>
                {
                    using (var conn = new LineReaderConnection())
                    {
                        cancel.Register(() => conn.Dispose());
                        await conn.ConnectAsync(endPoint);

                        var ping = Observable
                            .Interval(TimeSpan.FromSeconds(1))
                            .Subscribe(async _ => await conn.SendAsync((byte)CommandType.Ping, new byte[0]));
                        using (ping)
                        {
                            while (!cancel.IsCancellationRequested)
                            {
                                var line = await conn.ReceiveAsync();
                                if (line == null)
                                {
                                    break;
                                }

                                observer.OnNext(line);
                            }
                        }
                    }
                })
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(
                    line => this.ReadLine((CommandType)line.Item1, line.Item2),
                    ex => this.Dispose(),
                    () => this.Dispose());
        }

        private enum CommandType : byte
        {
            Ping = 0,
            Version = 1,
            GameState = 3,
        }

        public ServerVersion ServerVersion
        {
            get { return this.version; }
            set { this.RaiseAndSetIfChanged(ref this.version, value); }
        }

        public ServerViewModel ServerViewModel { get; }

        public void Dispose()
        {
            this.lineReader.Dispose();
            this.ServerViewModel.Root.Value = null;
        }

        private void ReadLine(CommandType type, byte[] data)
        {
            switch (type)
            {
                case CommandType.Ping:
                    break;

                case CommandType.Version:
                    {
                        this.ServerVersion = JsonConvert.DeserializeObject<ServerVersion>(
                            ServerViewModel.StringEncoding.GetString(data));
                        break;
                    }

                case CommandType.GameState:
                    {
                        this.ServerViewModel.Update(data);
                        break;
                    }

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
