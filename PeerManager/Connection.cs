using System;

namespace PeerManager
{
    /*
     * this abstract class establishes a peer service connection for the application.
     * implementation is decided based on whether this connection is the first on a network
     * or needs to connect to an existing session
     */
    public abstract class Connection
    {
        // the player number, 1-4.
        // 1 is the "host" or first player to run the application
        // 0 is "System"
        protected int PeerId;
        protected IPeerService PeerService;

        // start establishes communication with other peers
        public abstract void Start();

        // pass-through: from messenger to peer service
        public void Send(SerializedMessage message)
        {
            PeerService.SendMessage(message);
        }

        // delegate method for messages assisting the connection process
        protected abstract void SystemMessageHandler(PeerSysMessage message);

        public abstract void Stop();

        // factory
        // TODO expand factory to decide which connection type to return based on presence on the network
        public static Connection CreateConnection(bool host, MessageDelegate del)
        {
            if (host)
                return new HostConnection(del);
            else
                return new ClientConnection(del);
        }
    }
}
