using System;
using System.Threading.Tasks;

namespace PeerManager
{
    public class PeerConnection : Connection
    {
        private IPeerService _peerService;
        public new int PeerID { get; set; } = -1;

        public override IPeerService Start(MessageDelegate del)
        {
            _peerService = new PeerService(del, SystemMessageHandler);
            _peerService.StartSvc("pass");

            Task.WaitAll(_peerService.ChannelOpen);

            _peerService.SendSysMessage(new PeerSysMessage(SysMsg.Announce));

            SerializedMessage debug = new SerializedMessage(Purpose.Chat, 0)
            {
                Text = String.Format("Announcement sent")
            };
            del(debug);

            return _peerService;
        }

        protected override void SystemMessageHandler(PeerSysMessage message)
        {
            // ignore your own system messages
            if (message.PeerID == this.PeerID)
                return;

            switch (message.Purpose)
            {
                case SysMsg.Assign:
                    SerializedMessage debug = new SerializedMessage(Purpose.Chat, 0)
                    {
                        Text = String.Format("Value Received")
                    };
                    _peerService.ReceiveMessage(debug);
                    if (this.PeerID < 0)
                        this.PeerID = message.PeerID;
                    SerializedMessage data = new SerializedMessage(Purpose.Init, PeerID);
                    _peerService.ReceiveMessage(data);
                    break;
                case SysMsg.Deny:
                    break;
            }
        }

        public override void Stop()
        {
            _peerService.StopSvc();
        }
    }
}
