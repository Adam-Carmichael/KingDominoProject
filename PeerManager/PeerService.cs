using System;
using System.ServiceModel;
using System.Net.PeerToPeer;

namespace PeerManager
{
    // context mode ensures only one instance of the service is created per process
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PeerService : IPeerService
    {
        private readonly MessageDelegate _msgDelegate;
        private readonly ServiceHost _host = null;
        private readonly ChannelFactory<IPeerService> _channelFactory = null;
        private IPeerService _channel;

        public PeerService() { }

        public PeerService(MessageDelegate del)
        {
            _msgDelegate = del;
            _host = new ServiceHost(this);
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
            // receive passes the message to the service caller
            _msgDelegate(message);
        }
    }
}
