// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    internal sealed class LineReaderConnection : IDisposable
    {
        private const int MaxSize = 1024;

        private readonly TcpConnection tcpConnection;

        public LineReaderConnection()
        {
            this.tcpConnection = new TcpConnection();
        }

        public async Task ConnectAsync(EndPoint endPoint)
        {
            await this.tcpConnection.ConnectAsync(endPoint);
        }

        private async Task<bool> TryFillBuffer(byte[] buffer)
        {
            var read = 0;
            while (read < buffer.Length)
            {
                var count = await this.tcpConnection.ReceiveAsync(buffer, read, buffer.Length - read);
                if (count == 0)
                {
                    return false;
                }

                read += count;
            }

            return true;
        }

        public async Task<Tuple<byte, byte[]>> ReceiveAsync()
        {
            var header = new byte[sizeof(int) + 1];
            if (!await this.TryFillBuffer(header))
            {
                return null;
            }

            var payloadSize = BitConverter.ToInt32(header, 0);
            var type = header[sizeof(int)];

            var payload = new byte[payloadSize];
            if (!await this.TryFillBuffer(payload))
            {
                return null;
            }

            return Tuple.Create(type, payload);
        }

        public async Task SendAsync(byte type, byte[] data)
        {
            // Header format:
            // 0x00000000 data length
            // 0x00       type
            // 0x...      data
            var header = BitConverter.GetBytes(data.Length);
            Array.Resize(ref header, sizeof(int) + 1);
            header[sizeof(int)] = type;

            await this.tcpConnection.SendAsync(header, 0, header.Length);
            for (var offset = 0; offset < data.Length; offset += MaxSize)
            {
                await this.tcpConnection.SendAsync(data, offset, Math.Min(data.Length - offset, MaxSize));
            }
        }

        public void Dispose()
        {
            this.tcpConnection.Dispose();
        }
    }
}
