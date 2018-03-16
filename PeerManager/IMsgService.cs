using System;
using System.ServiceModel;

namespace PeerManager
{
    [ServiceContract]
    public interface IMsgService
    {
        int StartSvc(string password);
        void StopSvc();

        [OperationContract(IsOneWay = true)]
        void SendMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(SerializedMessage message);
    }

    // delegate declaration ensures delegated method matches signature required by IMsgService methods
    public delegate void MessageDelegate(SerializedMessage message);
}
