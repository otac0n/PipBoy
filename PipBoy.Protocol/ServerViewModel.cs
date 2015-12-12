// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class ServerViewModel
    {
        public static readonly Encoding StringEncoding = Encoding.GetEncoding(28591);

        private readonly Dictionary<int, Box> graph;

        public ServerViewModel()
        {
            this.graph = new Dictionary<int, Box>();
            this.Root = this.GetBox(0);
        }

        public Box Root { get; }

        public void Update(byte[] data)
        {
            var index = 0;
            var readByte = new Func<byte>(() => data[index++]);
            var readShort = new Func<int>(() =>
            {
                var value = BitConverter.ToInt16(data, index);
                index += sizeof(short);
                return value;
            });
            var readInt = new Func<int>(() =>
            {
                var value = BitConverter.ToInt32(data, index);
                index += sizeof(int);
                return value;
            });
            var readString = new Func<string>(() =>
            {
                var start = index;
                var end = Array.IndexOf(data, (byte)0, start);

                index = end + 1;
                return StringEncoding.GetString(data, start, end - start);
            });

            while (index < data.Length)
            {
                var kind = readByte();
                var box = this.GetBox(id: readInt());

                switch (kind)
                {
                    case 0:
                        box.Value = readByte() != 0;
                        break;

                    case 1:
                    case 2:
                        box.Value = readByte();
                        break;

                    case 3:
                    case 4:
                    case 5:
                        box.Value = readInt();
                        break;

                    case 6:
                        box.Value = readString();
                        break;

                    case 7:
                        {
                            var count = readShort();

                            var items = new int[count];
                            for (var i = 0; i < count; i++)
                            {
                                items[i] = readInt();
                            }

                            box.Value = items.Select(r => this.GetBox(r)).ToList();
                            break;
                        }

                    case 8:
                        {
                            var count = readShort();

                            var values = new int[count];
                            var keys = new string[count];
                            for (var i = 0; i < count; i++)
                            {
                                values[i] = readInt();
                                keys[i] = readString();
                            }

                            var tail = readShort(); // TODO: I don't know what this is for.

                            box.Value = Enumerable.Range(0, count).ToDictionary(i => keys[i], i => this.GetBox(values[i]));
                            break;
                        }

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private Box GetBox(int id)
        {
            Box box;
            if (!this.graph.TryGetValue(id, out box))
            {
                this.graph[id] = box = new Box(id);
            }

            return box;
        }
    }
}
