using System;
using System.Runtime.Serialization;
using DataModel;
/*
 * DataContract defines an object to be serialized
 * DataMember marks the fields that are to be serialized
 *   Other fields will be omitted
 *
 * DataMembers that are objects will not be serialized properly. Ex:
 *   Tile object as a DataMember will not serialize, as Tile's class does not have a DataContract
 *     Tile is not in this namespace, and referencing it would cause circular dependency
 *     this blocks using Tile as a DataMember
 *   Instead, create DataMembers for each field of the object
 *     then reconstruct the object on the receiving end
 */

namespace PeerManager
{
    [DataContract]
    public enum Purpose
    {
        [EnumMember(Value = "system")]
        System,
        [EnumMember(Value = "chat")]
        Chat,
        [EnumMember(Value = "deal")]
        Deal,
        [EnumMember(Value = "select")]
        Select,
        [EnumMember(Value = "tile")]
        Tile,
        [EnumMember(Value = "player")]
        Player
    }
    
    [DataContract]
    public class SerializedMessage
    {
        // TODO break this up and use a decorator? factory?

        public SerializedMessage()
        {
        }

        public SerializedMessage(Purpose purpose, int index)
        {
            Purpose = purpose;
            PeerId = index;
        }

        [DataMember] public Purpose Purpose { get; set; }

        [DataMember] public int PeerId { get; set; }

        [DataMember] public PlayerData Player { get; set; }

        [DataMember] public string Text { get; set; }

        [DataMember] public int[] Dominos { get; set; }

        [DataMember] public int Domino { get; set; }

        [DataMember] public string Terrain { get; set; }

        [DataMember] public int Crowns { get; set; }

        [DataMember] public int Xcoord { get; set; }

        [DataMember] public int Ycoord { get; set; }
    }
}