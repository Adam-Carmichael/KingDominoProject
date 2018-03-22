using System;
using System.Diagnostics;
using PeerManager;
using DataModel;

namespace WpfMockup
{
    public class PeerMessenger : IMessenger
    {
        private readonly IPeerService _peerService;
        private readonly ExampleViewModel _viewModel;
        private int _thisPlayer;

        public PeerMessenger(ExampleViewModel model, IPeerService peerService)
        {
            _viewModel = model;
            _peerService = peerService;
        }

        public void SendChatMessage(string text)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Chat, _thisPlayer)
            {
                Text = text
            };
            _peerService.SendMessage(data);
        }

        public void SendPlaceTile(int x, int y, Tile tile)
        {

        }

        public void SendSelectDomino(int domino)
        {

        }

        public void SendDealDominos(int[] dominos)
        {

        }

        public void SendPlayerUpdate(int index)
        {
            PlayerData update = _viewModel.PlayerList[index];
            SerializedMessage data = new SerializedMessage(Purpose.Player, _thisPlayer)
            {
                Player = update
            };
            _peerService.SendMessage(data);
        }
    }
}
