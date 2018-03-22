using System;

namespace DataModel
{
    public class Tile
    {
        public string Terrain { get; set; }
        public int Crowns { get; set; }

        public Tile(string terrain, int crowns)
        {
            this.Terrain = terrain;
            this.Crowns = crowns;
        }
    }
}
