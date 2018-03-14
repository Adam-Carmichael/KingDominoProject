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
            Domino test = (Domino) dominos[Rnd.Next(dominos.Count)];
            return test;
        }

        public void DominoHolderGenerator()
        {
            // 1-12
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/01.png")); // Field & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/02.png")); // Field & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/03.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/04.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/05.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/06.png")); // Forest & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/07.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/08.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/09.png")); // Lake & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/10.png")); // Grass & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/11.png")); // Grass & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), "Resources/Domino/12.png")); // Swamp & Swamp
            // 13-24
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/13.png")); // Field & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/14.png")); // Field & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/15.png")); // Field & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), "Resources/Domino/16.png")); // Field & Swamp
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/17.png")); // Forest & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/18.png")); // Forest & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Field01.png", "Field", 1), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/19.png")); // Field1 & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Field01.png", "Field", 1), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/20.png")); // Field1 & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Field01.png", "Field", 1), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/21.png")); // Field1 & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Field01.png", "Field", 1), new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), "Resources/Domino/22.png")); // Field1 & Swamp
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Field01.png", "Field", 1), new Tile("Resources/Terrain/0Mine01.png", "Mine", 0), "Resources/Domino/23.png")); // Field1 & Mine
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/24.png")); // Forest1 & Field
            // 25-36
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/25.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/26.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/27.png")); // Forest1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), "Resources/Domino/28.png")); // Forest1 & Lake
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Forest01.png", "Forest", 1), new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), "Resources/Domino/29.png")); // Forest1 & Grass
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/30.png")); // Lake1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/31.png")); // Lake1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/32.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/33.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/34.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Lake01.png", "Lake", 1), new Tile("Resources/Terrain/0Forest01.png", "Forest", 0), "Resources/Domino/35.png")); // Lake1 & Forest
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/1Grass01.png", "Grass", 1), "Resources/Domino/36.png")); // Field & Grass1
            // 37-48
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), new Tile("Resources/Terrain/1Grass01.png", "Grass", 1), "Resources/Domino/37.png")); // Lake & Grass1
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/1Swamp01.png", "Swamp", 1), "Resources/Domino/38.png")); // Field & Swamp1
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), new Tile("Resources/Terrain/1Swamp01.png", "Swamp", 1), "Resources/Domino/39.png")); // Grass & Swamp1
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/1Mine01.png", "Mine", 1), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/40.png")); // Mine1 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/2Grass01.png", "Grass", 2), "Resources/Domino/41.png")); // Field & Grass2
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Lake01.png", "Lake", 0), new Tile("Resources/Terrain/2Grass01.png", "Grass", 2), "Resources/Domino/42.png")); // Lake & Grass2
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/2Swamp01.png", "Swamp", 2), "Resources/Domino/43.png")); // Field & Swamp2
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Grass01.png", "Grass", 0), new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), "Resources/Domino/44.png")); // Grass & Swamp
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/2Mine01.png", "Mine", 2), new Tile("Resources/Terrain/0Field01.png", "Field", 0), "Resources/Domino/45.png")); // Mine2 & Field
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), new Tile("Resources/Terrain/2Mine01.png", "Mine", 2), "Resources/Domino/46.png")); // Swamp & Mine2
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Swamp01.png", "Swamp", 0), new Tile("Resources/Terrain/2Mine01.png", "Mine", 2), "Resources/Domino/47.png")); // Swamp & Mine2
            this.dominos.Add(new Domino(new Tile("Resources/Terrain/0Field01.png", "Field", 0), new Tile("Resources/Terrain/3Mine01.png", "Mine", 3), "Resources/Domino/48.png")); // Field & Mine3
        }
    }
}
