using System;

namespace PeerManager
{
    /*
     * See base class Connection for basic information
     */
    public class ClientConnection : Connection
    {
        public ClientConnection(MessageDelegate del)
        {
            PeerId = -1;           // init to -1 so number may be assigned
            PeerService = new PeerService(del, SystemMessageHandler);
            PeerService.StartSvc("pass");
        }

        public override void Start()
        {
            // announce presence on the network, and await an assignment
            PeerService.SendSysMessage(new PeerSysMessage(SysMsg.Announce));
        }

        protected override void SystemMessageHandler(PeerSysMessage message)
        {
            switch (message.Purpose)
            {
                case SysMsg.Assign:
                    if (this.PeerId > 0) // if this player already has an id assigned (assigned ids are > 0)
                        break;

                    this.PeerId = message.PeerId;

                    /*
                     * sending a message to this client's own Receive invokes the message delegate for game info.
                     * Another way would be preferred to get data up a level or tier
                     *            ^ without circular dependency
                     */
                    SerializedMessage data = new SerializedMessage(Purpose.Init, PeerId);
                    PeerService.ReceiveMessage(data);
                    break;
                case SysMsg.Deny:
                    // there are already 4 players:
                    // TODO implement notification of full game and gracefully exit
                    // maybe throw custom 'FullGameException' to be caught by application?
                    PeerService.ReceiveMessage(new SerializedMessage(Purpose.Chat, 0) {Text = "Connection Request Denied"});
                    break;
            }
        }

        public override void Stop()
        {
            PeerService.StopSvc();
        }
    }
}
