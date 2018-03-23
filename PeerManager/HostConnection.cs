using System;

namespace PeerManager
{
    public class HostConnection : Connection
    {
        private IPeerService _peerService;
        private int PeerCount { get; set; }
        public new int PeerID { get; set; }

        public override IPeerService Start(MessageDelegate del)
        {
            _peerService = new PeerService(del, SystemMessageHandler);
            _peerService.StartSvc("pass");
            PeerID = 1;
            PeerCount = 1;
            SerializedMessage data = new SerializedMessage(Purpose.Init, PeerID);
            _peerService.ReceiveMessage(data);
            return _peerService;
        }

        protected override void SystemMessageHandler(PeerSysMessage message)
        {
            // ignore your own system messages
            if (message.PeerID == PeerID)
                return;


            SerializedMessage debug = new SerializedMessage(Purpose.Chat, 0)
            {
                Text = String.Format("Request Received")
            };
            _peerService.ReceiveMessage(debug);


            if (message.Purpose == SysMsg.Announce)
            {
                if (PeerCount == 4)         // TODO number needs to differ for alternate game mode
                {
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Deny);
                    _peerService.SendSysMessage(data);
                    SerializedMessage debug2 = new SerializedMessage(Purpose.Chat, 0)
                    {
                        Text = String.Format("DENIED")
                    };
                    _peerService.ReceiveMessage(debug2);
                }
                else
                {
                    PeerCount++;
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Assign, PeerCount);
                    _peerService.SendSysMessage(data);
                    SerializedMessage debug3 = new SerializedMessage(Purpose.Chat, 0)
                    {
                        Text = String.Format("Assigning {0} to new player", PeerCount)
                    };
                    _peerService.ReceiveMessage(debug3);
                }
            }
        }

        public override void Stop()
        {
            _peerService.StopSvc();
        }
    }
}
