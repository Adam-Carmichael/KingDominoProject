using System;
using PeerManager;

namespace WpfWcfExample
{
    /*
     * MessageHandler packs SerializedMessages to send to the p2p channel
     * MessageRouter method unpacks SerializedMessages receieved from other p2p clients
     *    Data is sent to the ViewModel for update
     */

    public class MessageHandler : IMessageHandler
    {
        private readonly MsgService _messenger;
        private readonly ExampleViewModel _model;
        private readonly int _thisPlayer;

        public MessageHandler(ExampleViewModel model)
        {
            _model = model;
            _model.Init();
            _messenger = new MsgService(MessageRouter);
            _thisPlayer = _messenger.StartSvc("pass");
            _model.ThisPlayer = _model.Players[_thisPlayer];
        }

        // delegate method to handle incoming messages from p2p clients
        public void MessageRouter(SerializedMessage message)
        {
            switch (message.Purpose)
            {
                case Purpose.Chat:
                    _model.DisplayChatMessage(message.Player, message.Text);
                    break;
                case Purpose.Deal:
                    break;
                case Purpose.Select:
                    break;
                case Purpose.Tile:
                    break;
                case Purpose.Color:
                    _model.UpdatePlayerColor(message.Player, message.Color);
                    break;
                default:
                    _model.DisplayChatMessage(9, "Error: Network message not recognized");
                    break;
            }
        }

        public void SendChatMessage(string text)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Chat, _thisPlayer)
            {
                Text = text
            };
            _messenger.SendMessage(data);
        }

        public void SendPlaceTile(string player, int x, int y, Tile tile)
        {

        }

        public void SendSelectDomino(string player, int domino)
        {

        }

        public void SendDealDominos(int[] dominos)
        {

        }

        public void SendPlayerColor(string color)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Color, _thisPlayer)
            {
                Color = color
            };
            _messenger.SendMessage(data);
        }
    }
}
