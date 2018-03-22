using System;
using System.Diagnostics;

namespace PeerManager
{
    public class HostConnection : Connection
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
