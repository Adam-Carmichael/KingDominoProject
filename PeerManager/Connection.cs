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
        public abstract IPeerService Start(MessageDelegate del);

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
