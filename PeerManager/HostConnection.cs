using System;

namespace PeerManager
{
    public class HostConnection : Connection
    {
        private IPeerService _peerService;
        private int PeerCount { get; set; }         // total number of players connected
        public new int PeerID { get; set; }         // this player will be assigned 1

        // default constructor assumed

        public override IPeerService Start(MessageDelegate del)
        {
            _peerService = new PeerService(del, SystemMessageHandler);
            _peerService.StartSvc("pass");

            // this is the first client on the network, so there is no one to communicate with yet
            // take ID of 1, and keep track of player count
            PeerID = 1;
            PeerCount = 1;
            
            /*
             * sending a message to this client's own Receive invokes the message delegate for game info.
             * Another way would be preferred to get data up a level or tier
             *            ^ without circular dependency
             */
            SerializedMessage data = new SerializedMessage(Purpose.Init, PeerID);
            _peerService.ReceiveMessage(data);

            return _peerService;
        }

        protected override void SystemMessageHandler(PeerSysMessage message)
        {
            // ignore your own system messages
            if (message.PeerID == PeerID)
                return;

            if (message.Purpose == SysMsg.Announce)
            {
                // if there are already 4 players connected
                if (PeerCount == 4)         // TODO number needs to differ for alternate game mode
                {
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Deny);
                    _peerService.SendSysMessage(data);
                }
                else       // there is room, so assign the next empty slot to the new client
                {
                    PeerCount++;
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Assign, PeerCount);
                    _peerService.SendSysMessage(data);
                }
            }
        }

        public override void Stop()
        {
            _peerService.StopSvc();
        }
    }
}
