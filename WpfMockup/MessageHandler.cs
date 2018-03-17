using System;
using PeerManager;

namespace WpfMockup
{
    /*
     * MessageHandler packs SerializedMessages to send to the p2p channel
     * MessageRouter method unpacks SerializedMessages receieved from other p2p clients
     *    Data is sent to the ViewModel for update
     */

    public class MessageHandler : IMessageHandler
    {
        private readonly int SYSTEM = 0;            // constant to id system's player number

        private readonly IPeerService _peerService;
        private readonly ExampleViewModel _viewModel;
        private int _thisPlayer;

        public MessageHandler(ExampleViewModel model)
        {
            _viewModel = model;
            _peerService = new PeerService(MessageRouter);
        }

        // delegate method to handle incoming messages from p2p clients
        public void MessageRouter(SerializedMessage message)
        {
            switch (message.Purpose)
            {
                case Purpose.Chat:
                    _viewModel.DisplayChatMessage(message.PeerId, message.Text);
                    break;
                case Purpose.Deal:
                    break;
                case Purpose.Select:
                    break;
                case Purpose.Tile:
                    break;
                case Purpose.Player:
                    _viewModel.UpdatePlayerData(message.PeerId, message.IsOccupied, message.Name, message.Color);
                    break;
                case Purpose.System:
                    SystemMessageHandler(message);
                    break;
                default:
                    _viewModel.DisplayChatMessage(SYSTEM, "Error: Network message not recognized");
                    break;
            }
        }

        private void SystemMessageHandler(SerializedMessage message)
        {
            switch (message.Text)
            {
                case "rollcall":
                    for (int i = 1; i <= 4; i++)        // TODO eliminate Magic Numbers
                    {
                        PlayerData source = _viewModel.PlayerList[i];
                        SerializedMessage data = new SerializedMessage(Purpose.Player, i)
                        {
                            Name = source.Name,
                            Color = source.Color
                        };
                        _peerService.SendMessage(data);
                    }
                    break;
            }
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
                IsOccupied = update.IsOccupied,
                Name = update.Name,
                Color = update.Color
            };
            _peerService.SendMessage(data);
        }


        public void NewGame(string name, string color)
        {
            _peerService.StartSvc("pass");
            _thisPlayer = 1;
            _viewModel.InitThisPlayer(_thisPlayer, name, color);
            
        }

        public void JoinGame(string name, string color)
        {
            _peerService.StartSvc("pass");
            SerializedMessage data = new SerializedMessage(Purpose.System, SYSTEM)
            {
                Text = "rollcall"
            };
            _peerService.SendMessage(data);

            _viewModel.ChatHistory += "Connecting...\n";
            System.Threading.Thread.Sleep(2000);

            foreach (PlayerData player in _viewModel.PlayerList)
            {
                if (!player.IsOccupied)
                {
                    _thisPlayer = _viewModel.PlayerList.IndexOf(player);
                    break;
                }
            }

            if (_thisPlayer == 0)
                _viewModel.ChatHistory += "Sorry, there are no available player slots\n";
            else
                _viewModel.InitThisPlayer(_thisPlayer, name, color);
        }
    }
}
