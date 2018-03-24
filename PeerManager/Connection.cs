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
        public int PeerID;

        // start establishes then returns the peer service implementation
        // TODO possibly move start implementations to constructors
        public abstract IPeerService Start(MessageDelegate del);

        // delegate method for messages assisting the connection process
        protected abstract void SystemMessageHandler(PeerSysMessage message);

        public abstract void Stop();

        // factory
        // TODO expand factory to decide which connection type to return based on presence on the network
        public static Connection CreateConnection(bool host)
        {
            if (host)
                return new HostConnection();
            else
                return new PeerConnection();
        }
    }
}
