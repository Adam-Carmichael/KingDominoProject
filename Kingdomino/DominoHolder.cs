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

        public DominoHolder()
        {
            dominos = new ArrayList();
            DominoHolderGenerator();
        }

        public Domino RandomDomino()
        {
            int random = rnd.Next(dominos.Count);
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

        private Enums ConvertStringToTileType(String terrain)
        {
            switch(terrain)
            {
                case ("Field"):
                    return Enums.Field;
                case ("Forest"):
                    return Enums.Forest;
                case ("Grass"):
                    return Enums.Grass;
                case ("Lake"):
                    return Enums.Lake;
                case ("Mine"):
                    return Enums.Mine;
                case ("Swamp"):
                    return Enums.Swamp;

                default:
                    return Enums.Origin;
            }
        }
    } 
}
