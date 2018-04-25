using System;

namespace PeerManager
{
    public class HostConnection : Connection
    {
        private int PeerCount { get; set; }         // total number of players connected
        
        public HostConnection(MessageDelegate del)
        {
            PeerService = new PeerService(del, SystemMessageHandler);
            PeerService.StartSvc("pass");
        }

        public override void Start()
        {
            // this is the first client on the network, so there is no one to communicate with yet
            // take ID of 1, and keep track of player count
            PeerId = 1;
            PeerCount = 1;

            /*
             * sending a message to this client's own Receive invokes the message delegate for game info.
             * Another way would be preferred to get data up a level or tier
             *            ^ without circular dependency
             */
            SerializedMessage data = new SerializedMessage(Purpose.Init, PeerId);
            PeerService.ReceiveMessage(data);
        }

        // This first client handles assigning player numbers to subsequent clients
        protected override void SystemMessageHandler(PeerSysMessage message)
        {
            if (message.Purpose == SysMsg.Announce)
            {
                // if there are already 4 players connected
                if (PeerCount == 4)         // TODO number needs to differ for alternate game mode
                {
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Deny);
                    PeerService.SendSysMessage(data);
                }
                else       // there is room, so assign the next empty slot to the new client
                {
                    PeerCount++;
                    PeerSysMessage data = new PeerSysMessage(SysMsg.Assign)
                    {
                        PeerId = PeerCount
                    };
                    PeerService.SendSysMessage(data);
                    /*
                     * report the current player list to all players
                     * this has the dual purpose of letting the new client know who is already here
                     * and informing those already here about the new client
                     *
                     * sending a message to this client's own Receive invokes the message delegate for game info.
                     * Another way would be preferred to get commands up a level or tier
                     *            ^ without circular dependency
                     */
                    for (int i = 1; i < PeerCount; i++)
                        PeerService.ReceiveMessage(new SerializedMessage(Purpose.Query, i));
                }
            }
        }

        public override void Stop()
        {
            PeerService.StopSvc();
        }
    }
}
