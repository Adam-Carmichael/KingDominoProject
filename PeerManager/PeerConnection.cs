using System;
using System.Threading.Tasks;

namespace PeerManager
{
    /*
     * See base class Connection for basic information
     */
    public class PeerConnection : Connection
    {
        private IPeerService _peerService;
        public new int PeerID { get; set; } = -1;           // init to -1 so number may be assigned

        // default constructor assumed

        public override IPeerService Start(MessageDelegate del)
        {
            _peerService = new PeerService(del, SystemMessageHandler);
            _peerService.StartSvc("pass");

            // send system message to the network announcing the presence of this new client
            _peerService.SendSysMessage(new PeerSysMessage(SysMsg.Announce));

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
                    if (this.PeerID <= 0) // if this player doesnt already have an id assigned (assigned ids are > 0)
                        this.PeerID = message.PeerID;

                    /*
                     * sending a message to this client's own Receive invokes the message delegate for game info.
                     * Another way would be preferred to get data up a level or tier
                     *            ^ without circular dependency
                     */
                    SerializedMessage data = new SerializedMessage(Purpose.Init, PeerID);
                    _peerService.ReceiveMessage(data);

                    break;
                case SysMsg.Deny:
                    // if there are already 4 players:
                    // TODO implement notification of full game
                    break;
            }
        }

        public override void Stop()
        {
            _peerService.StopSvc();
        }
    }
}
