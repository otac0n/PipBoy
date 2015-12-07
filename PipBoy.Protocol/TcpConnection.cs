// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class TcpConnection : IDisposable
    {
        private readonly Socket socket;

        public TcpConnection()
        {
            this.socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public Task ConnectAsync(EndPoint endPoint)
        {
            var result = new TaskCompletionSource<byte>();

            var connectInfo = new SocketAsyncEventArgs
            {
                RemoteEndPoint = endPoint,
            };

            EventHandler<SocketAsyncEventArgs> complete = (s, a) =>
            {
                if (a.SocketError != SocketError.Success)
                {
                    result.SetException(new SocketException((int)a.SocketError));
                }
                else
                {
                    result.SetResult(0);
                }
            };
            connectInfo.Completed += complete;

            try
            {
                if (!this.socket.ConnectAsync(connectInfo))
                {
                    complete(this.socket, connectInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result.Task;
        }

        public Task<int> ReceiveAsync(byte[] buffer, int offset, int count)
        {
            var result = new TaskCompletionSource<int>();

            var connectInfo = new SocketAsyncEventArgs
            {
                BufferList = new[] { new ArraySegment<byte>(buffer, offset, count) },
            };

            EventHandler<SocketAsyncEventArgs> complete = (s, a) =>
            {
                if (a.SocketError != SocketError.Success)
                {
                    result.SetException(new SocketException((int)a.SocketError));
                }
                else
                {
                    result.SetResult(a.BytesTransferred);
                }
            };
            connectInfo.Completed += complete;

            try
            {
                if (!this.socket.ReceiveAsync(connectInfo))
                {
                    complete(this.socket, connectInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result.Task;
        }

        public Task SendAsync(byte[] buffer, int offset, int count)
        {
            var result = new TaskCompletionSource<int>();

            var connectInfo = new SocketAsyncEventArgs
            {
                BufferList = new[] { new ArraySegment<byte>(buffer, offset, count) },
            };

            EventHandler<SocketAsyncEventArgs> complete = (s, a) =>
            {
                if (a.SocketError != SocketError.Success)
                {
                    result.SetException(new SocketException((int)a.SocketError));
                }
                else
                {
                    result.SetResult(a.BytesTransferred);
                }
            };
            connectInfo.Completed += complete;

            try
            {
                if (!this.socket.SendAsync(connectInfo))
                {
                    complete(this.socket, connectInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetException(ex);
            }

            return result.Task;
        }

        public void Dispose()
        {
            this.socket.Dispose();
        }
    }
}
