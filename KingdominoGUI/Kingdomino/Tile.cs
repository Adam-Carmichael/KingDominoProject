﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    public class Tile
        {
        private String tileImage;
        public String TileImage
        {
            get { return tileImage; }
            set { tileImage = value; }
        }
        private String tileType;
        public String TileType
        {
            get { return tileType; }
            set { tileType = value; }
        }
        private int tileCrown;
        public int TileCrown
        {
            get { return tileCrown; }
            set { tileCrown = value; }
        }

        public Tile(String tileImage, String tileType, int tileCrown)
        {
            this.tileImage = tileImage;
            this.tileType = tileType;
            this.tileCrown = tileCrown;
        }
    }
}