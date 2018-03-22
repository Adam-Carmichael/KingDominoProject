using System;
using DataModel;

namespace PeerManager
{
    // TODO factory method = check connection;
    /*
     * this interface formats messeges for the application to send accross the connection
     */
    public interface IMessenger
    {
        void SendChatMessage(string text);

        void SendPlaceTile(int x, int y, Tile tile);

        void SendSelectDomino(int domino);

        void SendDealDominos(int[] dominos);

        void SendPlayerUpdate(int index);
    }
}
