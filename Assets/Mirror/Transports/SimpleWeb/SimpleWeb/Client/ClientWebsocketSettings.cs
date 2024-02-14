using System;

namespace Mirror.Transports.SimpleWeb.SimpleWeb.Client
{
    [Serializable]
    public struct ClientWebsocketSettings
    {
        public WebsocketPortOption ClientPortOption;
        public ushort CustomClientPort;
    }
    public enum WebsocketPortOption
    {
        DefaultSameAsServer,
        MatchWebpageProtocol,
        SpecifyPort
    }
}
