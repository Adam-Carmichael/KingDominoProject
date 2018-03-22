using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeerManager
{
    public class PeerConnection : Connection
    {
        private IPeerService _peerService;

        public override IPeerService Start(MessageDelegate del)
        {
            _peerService = new PeerService(del);
            _peerService.StartSvc("pass");
            return _peerService;
        }

        public override void Stop()
        {
            _peerService.StopSvc();
        }
    }
}
