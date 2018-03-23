using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PeerManager
{
    // context mode ensures only one instance of the service is created per process
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PeerService : IPeerService
    {
        private readonly MessageDelegate _msgDelegate;
        private readonly PeerSysDelegate _sysDelegate;
        private readonly ServiceHost _host = null;
        private readonly ChannelFactory<IPeerService> _channelFactory = null;
        private IPeerService _channel;

        public Task ChannelOpen { get; private set; }

        public PeerService() { }

        public PeerService(MessageDelegate del, PeerSysDelegate sysDel)
        {
            _msgDelegate = del;
            _sysDelegate = sysDel;
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

            ChannelOpen = Task.Factory.FromAsync(((ICommunicationObject) _channel).BeginOpen,
                ((ICommunicationObject) _channel).EndOpen, null);
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

        public void SendSysMessage(PeerSysMessage message)
        {
            // send calls the receive method on each peer in the channel
            _channel.ReceiveSysMessage(message);
            _channel.ReceiveMessage(new SerializedMessage(Purpose.Chat, 0) {Text = "Message sent"});
        }

        public void ReceiveSysMessage(PeerSysMessage message)
        {
            // receive passes the message to the service caller
            _channel.ReceiveMessage(new SerializedMessage(Purpose.Chat, 0) { Text = "Receiving Message" });
            _sysDelegate(message);
        }
    }
}
