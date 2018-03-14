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

        private ArrayList board;
        private String terrainPath = "Resources/Terrain/";
        private String dominoPath = "Resources/Domino/";
        private String originPath = "Resources/OriginTile/";
        private String castlePath = "Resources/Castle/";
        public Board(String color)
        {
            board = new ArrayList();
            board.Add(new Domino(new Tile(castlePath + color + ".png", "Origin", 0), new Tile(castlePath + color + ".png", "Origin", 0), originPath + color + ".png"));
        }

        public Domino GetOrigin()
        {
            foreach(Domino origin in board)
            {
                if(origin.Tile1.TileType.Equals("Origin"))
                {
                    return (Domino)board[board.IndexOf(origin)];
                }
            }
            return null;
        }
    }
}
