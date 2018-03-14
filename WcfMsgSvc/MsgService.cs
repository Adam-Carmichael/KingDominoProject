using System;
using System.ServiceModel;

namespace WcfMsgSvc
{
    // context mode ensures only one instance of the service is created per process
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MsgService : IMsgService
    {
        private readonly MessageDelegate _msgDelegate;
        private readonly ServiceHost _host = null;
        private readonly ChannelFactory<IMsgService> _channelFactory = null;
        private IMsgService _channel;

        public MsgService() { }

        public MsgService(MessageDelegate del)
        {
            _msgDelegate = del;
            _host = new ServiceHost(this);
            _channelFactory = new ChannelFactory<IMsgService>("KDPeerEndpoint");
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
