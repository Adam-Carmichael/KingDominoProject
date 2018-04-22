using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    class DominoHolder
    {
        public readonly String defaultTilePath = "Resources/Misc/logo.png";

        private ArrayList dominos;
        private Random rnd = new Random();

        public Random Rnd { get => rnd; set => rnd = value; }

        public DominoHolder()
        {
            dominos = new ArrayList();
            DominoHolderGenerator();
        }

        public Domino RandomDomino()
        {
            int random = Rnd.Next(dominos.Count);
            Domino test = (Domino) dominos[random];
            dominos.RemoveAt(random);
            return test;
        }

        public void DominoHolderGenerator()
        {
            string fileName = "dominos.txt";
            string dominosPath = Path.GetFullPath(fileName);
            string[] lines = System.IO.File.ReadAllLines(@dominosPath);
            foreach (string line in lines)
            {
                String[] splitter = line.Split('|');
                this.dominos.Add(new Domino(new Tile(splitter[0], ConvertStringToTileType(splitter[1]), Convert.ToInt32(splitter[2])), new Tile(splitter[3], ConvertStringToTileType(splitter[4]), Convert.ToInt32(splitter[5])), splitter[6], Convert.ToInt32(splitter[7])));
            }
        }

        private TileType ConvertStringToTileType(String terrain)
        {
            switch(terrain)
            {
                case ("Field"):
                    return TileType.Field;
                case ("Forest"):
                    return TileType.Forest;
                case ("Grass"):
                    return TileType.Grass;
                case ("Lake"):
                    return TileType.Lake;
                case ("Mine"):
                    return TileType.Mine;
                case ("Swamp"):
                    return TileType.Swamp;

                default:
                    return TileType.Origin;
            }
        }
    } 
}
