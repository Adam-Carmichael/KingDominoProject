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
            this.playBoard[2, 2] = new Tile(originCastlePath + color + ".png", "Origin", 0);
        }

        public int CalculateScore()
        {
            int score = 0;
            Boolean[,] checkedTilePositions = new Boolean[5, 5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Tile tempTile = PlayBoard[i, j];

                    if (tempTile != null && tempTile.TileCrown > 0 && checkedTilePositions[i, j] == false)
                    {
                        score += CheckConnectedLandscape(i, j, tempTile, checkedTilePositions);
                    }

                }
            }

            return score;
        }

        private int CheckConnectedLandscape(int row, int col, Tile tile, Boolean[,] checkedTilePositions)
        {
            int crowns = 0;
            int score = 0;

            if (row >= 1)
            {
                if (PlayBoard[row - 1, col] != null && PlayBoard[row - 1, col].TileType.Equals(tile.TileType))
                {
                    crowns += CheckIfTileHasCrowns(row - 1, col);

                    score++;

                    checkedTilePositions[row - 1, col] = true;
                    score += CheckConnectedLandscape(row - 1, col, PlayBoard[row - 1, col], checkedTilePositions);
                }
            }

            if (col >= 1)
            {
                if (PlayBoard[row, col - 1] != null && PlayBoard[row, col - 1].TileType.Equals(tile.TileType))
                {
                    crowns += CheckIfTileHasCrowns(row, col - 1);

                    score++;

                    score += CheckConnectedLandscape(row, col - 1, PlayBoard[row, col - 1], checkedTilePositions);
                }
            }

            if (row <= 4)
            {
                if (PlayBoard[row + 1, col] != null && PlayBoard[row + 1, col].TileType.Equals(tile.TileType))
                {
                    crowns += CheckIfTileHasCrowns(row + 1, col);

                    score++;

                    score += CheckConnectedLandscape(row + 1, col, PlayBoard[row + 1, col], checkedTilePositions);
                }

            }

            if (col <= 4)
            {
                if (PlayBoard[row, col - 1] != null && PlayBoard[row, col + 1].TileType.Equals(tile.TileType))
                {
                    crowns += CheckIfTileHasCrowns(row, col + 1);

                    score++;

                    score += CheckConnectedLandscape(row, col - 1, PlayBoard[row, col - 1], checkedTilePositions);
                }
            }

            return score * crowns;
        }

        public int CheckIfTileHasCrowns(int row, int col)
        {

            return playBoard[row, col].TileCrown;
        }

        public Tile GetOrigin()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
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
            this.playBoard[i, j] = tile;
        }
    }
}
