// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol
{
    public class ServerVersion
    {
        public ServerVersion(string lang, string version)
        {
            this.Lang = lang;
            this.Version = version;
        }

        public string Lang { get; }

        public string Version { get; }
    }
}
