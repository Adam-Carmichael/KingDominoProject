using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Board
    {
        private Tile[][] playBoard;
        public Tile[][] PlayBoard
        {
            get { return playBoard; }
            set { playBoard = value; }
        }
        private Domino chosen;
        public Domino Chosen
        {
            get { return chosen; }
            set { this.chosen = value; }
        }

        private String originCastlePath = "Resources/OriginCastle/";
        public Board(String color)
        {
            this.playBoard = new Tile[5][];
            this.playBoard[0] = new Tile[5];
            this.playBoard[1] = new Tile[5];
            this.playBoard[2] = new Tile[5];
            this.playBoard[3] = new Tile[5];
            this.playBoard[4] = new Tile[5];
            this.playBoard[2][2] = new Tile(originCastlePath + color + ".png", TileType.Origin, 0);
        }

        public int CalculateScore()
        {
            int totalScore = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Tile tempTile = PlayBoard[i][j];
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

            Boolean north = row > 0;
            Boolean west = col > 0;
            Boolean south = row < 4;
            Boolean east = col < 4;

            if (north)
            {
                score += CheckDirection(row - 1, col, tile, checkedTilePositions);
            }

            if (west)
            {
                score += CheckDirection(row, col - 1, tile, checkedTilePositions);
            }

            if (south)
            {
                score += CheckDirection(row + 1, col, tile, checkedTilePositions);
            }

            if (east)
            {
                score += CheckDirection(row, col + 1, tile, checkedTilePositions);
            }

            return score;
        }

        private int CheckDirection(int row, int col, Tile tile, Boolean[,] checkedTilePositions)
        {
            int score = 0;

            Tile tempTile = PlayBoard[row][col];

            if (tempTile != null && tempTile.TileType.Equals(tile.TileType) && !checkedTilePositions[row, col])
            {
                score++;
                checkedTilePositions[row, col] = true;
                score += CheckConnectedLandscape(row, col, tempTile, checkedTilePositions);
            }

            return score;
        }

        public Tile GetOrigin()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (this.playBoard[i][j] != null)
                    {
                        if (this.playBoard[i][j].TileType.Equals("Origin"))
                        {
                            return this.playBoard[i][j];
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Tile tile, int i, int j)
        {
            this.playBoard[i][j] = tile;
        }
    }
}
