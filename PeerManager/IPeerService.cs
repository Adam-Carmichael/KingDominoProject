using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PeerManager
{
    [ServiceContract]
    public interface IPeerService
    {
        Task ChannelOpen { get; }

        void StartSvc(string password);

        void StopSvc();

        [OperationContract(IsOneWay = true)]
        void SendMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void SendSysMessage(PeerSysMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveSysMessage(PeerSysMessage message);
    }

    // delegate declaration ensures delegated method matches signature required by IPeerService methods
    public delegate void MessageDelegate(SerializedMessage message);

    public delegate void PeerSysDelegate(PeerSysMessage message);
}
