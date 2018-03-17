using System;

namespace WpfMockup
{
    // this interface is all that should be accessed by the main application

    public interface IMessageHandler
    {
        void SendChatMessage(string text);

        void SendPlaceTile(int x, int y, Tile tile);

        void SendSelectDomino(int domino);

        void SendDealDominos(int[] dominos);

        void SendPlayerUpdate(int index);

        void NewGame(string name, string color);

        void JoinGame(string name, string color);
    }
}
