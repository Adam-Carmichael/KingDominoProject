using System;
using System.Runtime.Serialization;
/*
 * DataContract defines an object to be serialized
 * DataMember marks the fields that are to be serialized
 * Other fields will be omitted
 *
 * DataMembers that are objects will not be serialized properly
 * see: PlaceTileMessage for example
 * Tile object as a member would not serialize, as it does not have a contract
 * Tile is not in this namespace, and referencing it would cause circular dependency
 * Instead, create DataMembers for each field of the object
 * then reconstruct the object on the receiving end
 */

namespace WcfMsgSvc
{
    [DataContract]
    public enum Purpose
    {
        [EnumMember(Value = "chat")]
        Chat,
        [EnumMember(Value = "deal")]
        Deal,
        [EnumMember(Value = "sel")]
        Select,
        [EnumMember(Value = "tile")]
        Tile,
        [EnumMember(Value = "color")]
        Color
    }

    [DataContract]
    public class SerializedMessage
    {
        public SerializedMessage()
        {
        }

        public SerializedMessage(Purpose purpose)
        {
            Purpose = purpose;
        }
        
        [DataMember] public Purpose Purpose { get; set; }

        [DataMember] public string Player { get; set; }

        [DataMember] public string Text { get; set; }

        [DataMember] public int[] Dominos { get; set; }

        [DataMember] public int Domino { get; set; }

        [DataMember] public string Terrain { get; set; }

        [DataMember] public int Crowns { get; set; }

        [DataMember] public int Xcoord { get; set; }

        [DataMember] public int Ycoord { get; set; }

        [DataMember] public string Color { get; set; }
    }
}