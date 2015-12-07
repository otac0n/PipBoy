namespace PipBoy.Protocol
{
    using System;
    using System.Net;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using ReactiveUI;

    public class CommandInterpreter : ReactiveObject, IDisposable
    {
        private readonly IDisposable disposable;
        private readonly LineReaderConnection lineReaderConnection;
        private ServerVersion version;

        public CommandInterpreter(EndPoint endPoint)
        {
            this.ServerViewModel = new ServerViewModel();

            this.lineReaderConnection = new LineReaderConnection();

            var lineReader = Observable
                .Create<Tuple<byte, byte[]>>(async (observer, cancel) =>
                {
                    await this.lineReaderConnection.ConnectAsync(endPoint);
                    while (true)
                    {
                        var line = await this.lineReaderConnection.ReceiveAsync();
                        if (line == null)
                        {
                            break;
                        }

                        observer.OnNext(line);
                    }
                })
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(
                    line => this.ReadLine((CommandType)line.Item1, line.Item2),
                    ex => this.Dispose(),
                    () => this.Dispose());

            var ping = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Subscribe(async _ => await this.lineReaderConnection.SendAsync((byte)CommandType.Ping, new byte[0]));

            this.disposable = new CompositeDisposable(
                this.lineReaderConnection,
                lineReader,
                ping);
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
            this.disposable.Dispose();
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
                            Encoding.GetEncoding(28591).GetString(data));
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
