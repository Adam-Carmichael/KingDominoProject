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
            int totalScore = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Tile tempTile = PlayBoard[i, j];
                    Boolean[,] checkedTilePositions = new Boolean[5, 5];

                    if (tempTile != null && tempTile.TileCrown > 0)
                    {
                        checkedTilePositions[i, j] = true;

                        int tempLandscapeScore = 1;

                        tempLandscapeScore += CheckConnectedLandscape(i, j, tempTile, checkedTilePositions);
                        tempLandscapeScore *= tempTile.TileCrown;

                        totalScore += tempLandscapeScore;

                    }

                }
            }

            return totalScore;
        }

        private int CheckConnectedLandscape(int row, int col, Tile tile, Boolean[,] checkedTilePositions)
        {
            int score = 0;

            if (row >= 1)
            {
                Tile northTile = PlayBoard[row - 1, col];

                if (northTile != null && northTile.TileType.Equals(tile.TileType) && checkedTilePositions[row - 1, col] == false)
                {
                    score++;
                    checkedTilePositions[row - 1, col] = true;
                    score += CheckConnectedLandscape(row - 1, col, northTile, checkedTilePositions);
                }
            }

            if (col >= 1)
            {
                Tile westTile = PlayBoard[row, col - 1];

                if (westTile != null && westTile.TileType.Equals(tile.TileType) && checkedTilePositions[row, col - 1] == false)
                {
                    score++;
                    checkedTilePositions[row, col - 1] = true;
                    score += CheckConnectedLandscape(row, col - 1, westTile, checkedTilePositions);
                }
            }

            if (row < 4)
            {
                Tile southTile = PlayBoard[row + 1, col];

                if (southTile != null && southTile.TileType.Equals(tile.TileType) && checkedTilePositions[row + 1, col] == false)
                {
                    score++;
                    checkedTilePositions[row + 1, col] = true;
                    score += CheckConnectedLandscape(row + 1, col, southTile, checkedTilePositions);

                }

            }

            if (col < 4)
            {
                Tile eastTile = PlayBoard[row, col + 1];

                if (eastTile != null && eastTile.TileType.Equals(tile.TileType) && checkedTilePositions[row, col + 1] == false)
                {
                    score++;
                    checkedTilePositions[row, col + 1] = true;
                    score += CheckConnectedLandscape(row, col + 1, eastTile, checkedTilePositions);
                }
            }

            return score;
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
