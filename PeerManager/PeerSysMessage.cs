using System;
using System.Runtime.Serialization;

/*
 * See SerializedMessage for basic info on contracts
 */

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
        public PeerSysMessage() { }

        public PeerSysMessage(SysMsg purpose)
        {
            Purpose = purpose;      // what this message says
        }

        // overloaded constructor for easier message creation, since there are only two fields
        public PeerSysMessage(SysMsg purpose, int num)
        {
            Purpose = purpose;      // what this message says
            PeerID = num;           // who it is to (this is for assigning numbers to the receiver)
        }

        [DataMember] public SysMsg Purpose { get; set; }

        [DataMember] public int PeerID { get; set; }
    }
}
