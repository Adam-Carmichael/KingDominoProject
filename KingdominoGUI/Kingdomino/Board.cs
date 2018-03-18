using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    class Board
    {
        private Tile[,] playBoard;
        public Tile[,] PlayBoard
        {
            get { return playBoard; }
            set { playBoard = value; }
        }
        private String originCastlePath = "Resources/OriginCastle/";
        public Board(String color)
        {
            this.playBoard = new Tile[5, 5];
            this.playBoard[2,2] = new Tile(originCastlePath + color + ".png", "Origin", 0);
        }

        

        public Tile GetOrigin()
        {
            for(int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    if (this.playBoard[i, j] != null)
                    {
                        if (this.playBoard[i, j].TileType.Equals("Origin"))
                        {
                            return this.playBoard[i, j];
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Tile tile, int i, int j)
        {
            this.playBoard[i,j] = tile;
        }
    }
}
