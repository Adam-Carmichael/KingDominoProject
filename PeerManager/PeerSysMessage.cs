using System;
using System.Runtime.Serialization;

namespace PeerManager
{
    [DataContract]
    public enum SysMsg
    {
        [EnumMember(Value = "hi")]
        Announce,
        [EnumMember(Value = "assn")]
        Assign,
        [EnumMember(Value = "deny")]
        Deny
    }

    [DataContract]
    public class PeerSysMessage
    {
        public PeerSysMessage(SysMsg purpose)
        {
            Purpose = purpose;
        }

        public PeerSysMessage(SysMsg purpose, int num)
        {
            Purpose = purpose;
            PeerID = num;
        }

        [DataMember] public SysMsg Purpose { get; set; }

        [DataMember] public int PeerID { get; set; }
    }
}
