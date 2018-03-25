using System;
using DataModel;

namespace PeerManager
{
    /*
     * see IMessenger
     * implementation here works with WcfMockup
     * implementation for Kingdomino will need to be created
     */
    public class Messenger : IMessenger
    {
        private readonly Connection _connection;     // network service

        public Messenger(bool host, MessageDelegate del)
        {
            _connection = Connection.CreateConnection(host, del);
        }

        public void Start()
        {
            _connection.Start();
        }

        public void SendChatMessage(int id, string text)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Chat, id)
            {
                Text = text
            };
            _connection.Send(data);
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
            _connection.Send(data);
        }
    }
}
