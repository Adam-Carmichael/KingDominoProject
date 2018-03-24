using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace PeerManager
{
    /*
     * ServiceContract defines the interface used by the wcf service implementation
     * OperationContracts define the methods that can be remotely called
     */

    [ServiceContract]
    public interface IPeerService
    {
        void StartSvc(string password);

        void StopSvc();
        
        void SendMessage(SerializedMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveMessage(SerializedMessage message);

        void SendSysMessage(PeerSysMessage message);

        [OperationContract(IsOneWay = true)]
        void ReceiveSysMessage(PeerSysMessage message);
    }

    // delegate declaration ensures delegated method matches signature required by IPeerService methods
    public delegate void MessageDelegate(SerializedMessage message);

    public delegate void PeerSysDelegate(PeerSysMessage message);
}
