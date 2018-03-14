using System;

namespace WpfWcfExample
{
    // this interface is all that should be accessed by the main application

    public interface IMessageHandler
    {
        void SendChatMessage(string player, string text);

        void SendPlaceTile(string player, int x, int y, Tile tile);

        void SendSelectDomino(string player, int domino);

        void SendDealDominos(int[] dominos);

        void SendPlayerColor(string player, string color);
    }
}
