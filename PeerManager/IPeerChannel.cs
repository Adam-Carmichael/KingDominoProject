using System;
using System.ServiceModel;

namespace PeerManager
{
    interface IPeerChannel : IPeerService, IClientChannel
    {
    }
}
