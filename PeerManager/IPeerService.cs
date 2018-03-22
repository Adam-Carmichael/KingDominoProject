using System;
using System.ServiceModel;

namespace PeerManager
{
    [ServiceContract]
    public interface IPeerService
    {
        void StartSvc(string password);
        void StopSvc();

        [OperationContract(IsOneWay = true)]
        void SendMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(SerializedMessage message);
    }

    // delegate declaration ensures delegated method matches signature required by IPeerService methods
    public delegate void MessageDelegate(SerializedMessage message);
}
