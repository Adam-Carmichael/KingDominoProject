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
            // 1-12
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "01.png")); // Field & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "02.png")); // Field & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "03.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "04.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "05.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "06.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Lake01.png", "Lake", 0), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "07.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Lake01.png", "Lake", 0), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "08.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Lake01.png", "Lake", 0), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "09.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Grass01.png", "Grass", 0), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "10.png")); // Grass & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Grass01.png", "Grass", 0), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "11.png")); // Grass & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), dominoPath + "12.png")); // Swamp & Swamp
            // 13-24
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "13.png")); // Field & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "14.png")); // Field & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "15.png")); // Field & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), dominoPath + "16.png")); // Field & Swamp
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "17.png")); // Forest & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Forest01.png", "Forest", 0), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "18.png")); // Forest & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Field01.png", "Field", 1), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "19.png")); // Field1 & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Field01.png", "Field", 1), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "20.png")); // Field1 & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Field01.png", "Field", 1), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "21.png")); // Field1 & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Field01.png", "Field", 1), new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), dominoPath + "22.png")); // Field1 & Swamp
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Field01.png", "Field", 1), new Tile(terrainPath + "0Mine01.png", "Mine", 0), dominoPath + "23.png")); // Field1 & Mine
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "24.png")); // Forest1 & Field
            // 25-36
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "25.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "26.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "27.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Lake01.png", "Lake", 0), dominoPath + "28.png")); // Forest1 & Lake
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Forest01.png", "Forest", 1), new Tile(terrainPath + "0Grass01.png", "Grass", 0), dominoPath + "29.png")); // Forest1 & Grass
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "30.png")); // Lake1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "31.png")); // Lake1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "32.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "33.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "34.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Lake01.png", "Lake", 1), new Tile(terrainPath + "0Forest01.png", "Forest", 0), dominoPath + "35.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "1Grass01.png", "Grass", 1), dominoPath + "36.png")); // Field & Grass1
            // 37-48
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Lake01.png", "Lake", 0), new Tile(terrainPath + "1Grass01.png", "Grass", 1), dominoPath + "37.png")); // Lake & Grass1
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "1Swamp01.png", "Swamp", 1), dominoPath + "38.png")); // Field & Swamp1
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Grass01.png", "Grass", 0), new Tile(terrainPath + "1Swamp01.png", "Swamp", 1), dominoPath + "39.png")); // Grass & Swamp1
            this.dominos.Add(new Domino(new Tile(terrainPath + "1Mine01.png", "Mine", 1), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "40.png")); // Mine1 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "2Grass01.png", "Grass", 2), dominoPath + "41.png")); // Field & Grass2
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Lake01.png", "Lake", 0), new Tile(terrainPath + "2Grass01.png", "Grass", 2), dominoPath + "42.png")); // Lake & Grass2
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "2Swamp01.png", "Swamp", 2), dominoPath + "43.png")); // Field & Swamp2
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Grass01.png", "Grass", 0), new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), dominoPath + "44.png")); // Grass & Swamp
            this.dominos.Add(new Domino(new Tile(terrainPath + "2Mine01.png", "Mine", 2), new Tile(terrainPath + "0Field01.png", "Field", 0), dominoPath + "45.png")); // Mine2 & Field
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), new Tile(terrainPath + "2Mine01.png", "Mine", 2), dominoPath + "46.png")); // Swamp & Mine2
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Swamp01.png", "Swamp", 0), new Tile(terrainPath + "2Mine01.png", "Mine", 2), dominoPath + "47.png")); // Swamp & Mine2
            this.dominos.Add(new Domino(new Tile(terrainPath + "0Field01.png", "Field", 0), new Tile(terrainPath + "3Mine01.png", "Mine", 3), dominoPath + "48.png")); // Field & Mine3
        }
    }
}
