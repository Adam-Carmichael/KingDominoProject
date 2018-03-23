using System;
using DataModel;

namespace PeerManager
{
    public class Messenger : IMessenger
    {
        private readonly IPeerService _peerService;

        public Messenger(IPeerService peerSvc)
        {
            _peerService = peerSvc;
        }

        public void SendChatMessage(int id, string text)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Chat, id)
            {
                Text = text
            };
            _peerService.SendMessage(data);
        }

        public void SendPlaceTile(int id, int x, int y, Tile tile)
        {

        }

        public void SendSelectDomino(int id, int domino)
        {

        }

        public void SendDealDominos(int[] dominos)
        {

        }

        public void SendPlayerUpdate(int id, PlayerData update)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Player, id)
            {
                PlayerName = update.Name,
                Color = update.Color
            };
            _peerService.SendMessage(data);
        }
    }
}
