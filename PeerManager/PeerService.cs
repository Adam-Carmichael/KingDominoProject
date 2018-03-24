using System;
using System.ServiceModel;

namespace PeerManager
{
    /*
     * ServiceBehavior defines the implementation of the ServiceContract
     * Context mode ensures only one instance of the service is created per process
     */
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PeerService : IPeerService
    {
        private readonly MessageDelegate _msgDelegate;              // where to send messeges received for the game
        private readonly PeerSysDelegate _sysDelegate;              // where to send messeges received while setting up the connection
        private readonly ServiceHost _host = null;                  // this process
        private readonly ChannelFactory<IPeerService> _channelFactory = null;       // creates channels that match the same interface as this
        private IPeerService _channel;                              // where to send remote procedure calls

        public PeerService() { }                                    // default constructor req'd for ServiceContract


        /*
         * Construtor accepts two different delegates, one for game messages, one for connection setup
         */
        public PeerService(MessageDelegate del, PeerSysDelegate sysDel)
        {
            _msgDelegate = del;
            _sysDelegate = sysDel;

            // init the windows service host
            _host = new ServiceHost(this);
            // init factory telling it to use a binding defined in app.config
            _channelFactory = new ChannelFactory<IPeerService>("KDPeerEndpoint");
        }

        public void StartSvc(string password)
        {
            if (password == null)
                password = "";
            
            // start the service
            _host.Credentials.Peer.MeshPassword = password;
            _host.Open();

            // open communication channel
            _channelFactory.Credentials.Peer.MeshPassword = password;
            _channelFactory.Open();
            _channel = _channelFactory.CreateChannel();
        }

        public void StopSvc()
        {
            if (_host != null)
            {
                if (_host.State != CommunicationState.Closed)
                {
                    _channelFactory.Close();
                    _host.Close();
                }
            }
        }

        public void SendMessage(SerializedMessage message)
        {
            // send calls the receive method on each peer in the channel
            _channel.ReceiveMessage(message);
        }

        public void ReceiveMessage(SerializedMessage message)
        {
            // receive passes the message from the network to its implementation
            _msgDelegate(message);
        }

        public void SendSysMessage(PeerSysMessage message)
        {
            // send calls the receive method on each peer in the channel
            _channel.ReceiveSysMessage(message);
        }

        public void ReceiveSysMessage(PeerSysMessage message)
        {
            // receive passes the message from the network to its implementation
            _sysDelegate(message);
        }
    }
}
