using System;
using DataModel;

namespace PeerManager
{
    /*
     * this interface formats messeges for the application to send accross the network connection
     */
    public interface IMessenger
    {
        void SendChatMessage(int id, string text);

        void SendPlaceTile(int id, int x, int y, Tile tile);

        void SendSelectDomino(int id, int domino);

        void SendDealDominos(int[] dominos);

        void SendPlayerUpdate(int id, PlayerData update);
    }
}
