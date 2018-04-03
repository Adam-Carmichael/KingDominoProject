using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    class DominoHolder
    {
        private String terrainPath = "Resources/Terrain/";
        private String dominoPath = "Resources/Domino/";
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
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\ngl4\Downloads\KingDominoProject-master\Kingdomino\dominos.txt");
            foreach (string line in lines)
            {
                String[] splitter = line.Split('|');
                this.dominos.Add(new Domino(new Tile(splitter[0], splitter[1], Convert.ToInt32(splitter[2])), new Tile(splitter[3], splitter[4], Convert.ToInt32(splitter[5])), splitter[6], Convert.ToInt32(splitter[7])));
            }
        }
    } 
}
