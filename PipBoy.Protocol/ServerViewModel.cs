using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PipBoy.Protocol
{
    internal class ServerViewModel
    {
        private readonly Dictionary<int, Box<object>> graph;
        private HashSet<int> roots;

        public ServerViewModel()
        {
            this.graph = new Dictionary<int, Box<object>>();
            this.roots = new HashSet<int>();
        }

        private Box<object> BoxAt(int id)
        {
            Box<object> box;
            if (!graph.TryGetValue(id, out box))
            {
                roots.Add(id);
                graph[id] = box = new Box<object>();
            }

            return box;
        }

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
                var sb = new StringBuilder();
                while (true)
                {
                    var b = readByte();
                    if (b == 0)
                    {
                        break;
                    }

                    sb.Append((char)b);
                }

                return sb.ToString();
            });

            while (index < data.Length)
            {
                var kind = readByte();
                var id = readInt();

                switch (kind)
                {
                    case 0:
                        {
                            var value = readByte();
                            this.BoxAt(id).Value = (value != 0);
                            break;
                        }

                    case 1:
                    case 2:
                        {
                            var value = readByte();
                            this.BoxAt(id).Value = value;
                            break;
                        }

                    case 3:
                    case 4:
                    case 5:
                        {
                            var value = readInt();
                            this.BoxAt(id).Value = value;
                            break;
                        }

                    case 6:
                        {
                            var value = readString();
                            this.BoxAt(id).Value = value;
                            break;
                        }

                    case 7:
                        {
                            var count = readShort();

                            var items = new int[count];
                            for (var i = 0; i < count; i++)
                            {
                                items[i] = readInt();
                            }

                            this.BoxAt(id).Value = items.Select(r => this.BoxAt(r)).ToList();
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

                            this.BoxAt(id).Value = Enumerable.Range(0, count).ToDictionary(i => keys[i], i => this.BoxAt(values[i]));
                            break;
                        }

                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private class Box<T>
        {
            public Box(T initialValue = default(T))
            {
                this.Value = initialValue;
            }

            public T Value { get; set; }
        }
    }
}
