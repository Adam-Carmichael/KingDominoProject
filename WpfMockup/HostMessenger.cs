using System;
using PeerManager;
using DataModel;

namespace WpfMockup
{
    public class HostMessenger : IMessenger
    {
        private readonly IPeerService _peerService;
        private readonly ExampleViewModel _viewModel;
        private int _thisPlayer;

        public HostMessenger(ExampleViewModel model, IPeerService peerSvc)
        {
            _viewModel = model;
            _peerService = peerSvc;
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
