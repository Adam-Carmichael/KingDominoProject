using System;
using System.ServiceModel;

namespace WcfMsgSvc
{
    [ServiceContract]
    public interface IMsgService
    {
        void StartSvc(string password);
        void StopSvc();

        [OperationContract(IsOneWay = true)]
        void SendMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(SerializedMessage message);
    }

    // delegate declaration ensures delegated method matches signature required by IMsgService methods
    public delegate void MessageDelegate(SerializedMessage message);
}
