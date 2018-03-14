using System;
using WcfMsgSvc;

namespace WpfWcfExample
{
    /*
     * MessageHandler packs SerializedMessages to send to the p2p channel
     * MessageRouter method unpacks SerializedMessages receieved from other p2p clients
     *    Data is sent to the ViewModel for update
     */

    public class MessageHandler : IMessageHandler
    {
        private readonly MsgService _chatSvc;
        private readonly ExampleViewModel _model;

        public MessageHandler(ExampleViewModel model)
        {
            _model = model;
            _chatSvc = new MsgService(MessageRouter);
            _chatSvc.StartSvc("pass");
        }

        // delegate method to handle incoming messages from p2p clients
        public void MessageRouter(SerializedMessage message)
        {
            switch (message.Purpose)
            {
                case Purpose.Chat:
                    // ChatMessage chatMessage = (ChatMessage)message;
                    _model.DisplayChatMessage(message.Player, message.Text);
                    break;
                case Purpose.Deal:
                    break;
                case Purpose.Select:
                    break;
                case Purpose.Tile:
                    break;
                case Purpose.Color:
                    // PlayerUpdateMessage colorMessage = (PlayerUpdateMessage)message;
                    _model.UpdatePlayerColor(message.Player, message.Color);
                    break;
                default:
                    _model.DisplayChatMessage("System", "Error: Network message not recognized");
                    break;
            }
        }

        public void SendChatMessage(string player, string text)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Chat)
            {
                Player = player,
                Text = text
            };
            _chatSvc.SendMessage(data);
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

        public void SendPlayerColor(string player, string color)
        {
            SerializedMessage data = new SerializedMessage(Purpose.Color)
            {
                Player = player,
                Color = color
            };
            _chatSvc.SendMessage(data);
        }
    }
}
