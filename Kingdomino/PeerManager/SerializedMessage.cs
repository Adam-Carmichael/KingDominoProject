using System;
using System.Runtime.Serialization;

/*
 * DataContract defines an object to be serialized
 * DataMember marks the fields that are to be serialized
 *   Other fields will be omitted
 *
 * DataMembers that are objects will not be serialized properly. Ex:
 *   Tile from DataModel namespace will not serialize, as Tile's class does not have a DataContract
 *     Tile is not in this namespace, and referencing it would cause circular dependency
 *     this means using Tile as a DataMember will throw exceptions
 *   Instead, create DataMembers here for each field of the object
 *     then reconstruct the object on the receiving end
 *     Ex: Instead of Tile, create a string Terrain and int Crowns
 */

namespace PeerManager
{
    // Purpose enum is used to flag all SerializedMessages
    [DataContract]
    public enum Purpose
    {
        [EnumMember(Value = "sys")]
        System,
        [EnumMember(Value = "query")]
        Query,
        [EnumMember(Value = "init")]
        Init,
        [EnumMember(Value = "player")]
        Player,
        [EnumMember(Value = "chat")]
        Chat,
        [EnumMember(Value = "deal")]
        Deal,
        [EnumMember(Value = "select")]
        Select,
        [EnumMember(Value = "tile")]
        Tile
    }

    [DataContract]
    public class SerializedMessage
    {
        // Consolidated class for simplicity during development
        // TODO break this up and use a decorator? factory?

        public SerializedMessage() { }  // default req'd by Contract

        // the constructor properties are the only fields required for every message
        public SerializedMessage(Purpose purpose, int index)
        {
            Purpose = purpose;      // what this message is for
            PeerId = index;         // who it is from
        }

        [DataMember] public Purpose Purpose { get; set; }

        [DataMember] public int PeerId { get; set; }

        [DataMember] public string PlayerName { get; set; }

        [DataMember] public bool IsFull { get; set; }

        [DataMember] public string Text { get; set; }

        [DataMember] public int[] Dominos { get; set; }

        [DataMember] public int Domino { get; set; }

        [DataMember] public int Side { get; set; }

        [DataMember] public int Xcoord { get; set; }

        [DataMember] public int Ycoord { get; set; }
    }
}