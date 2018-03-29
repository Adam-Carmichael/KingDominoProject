using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingDomino
{
    public class Domino
    {
        private Tile tile1;
        public Tile Tile1
        {
            get { return tile1; }
            set { tile1 = value; }
        }
        private Tile tile2;
        public Tile Tile2
        {
            get { return tile2; }
            set { tile2 = value; }
        }
        private String dominoBack;
        public String DominoBack
        {
            get { return dominoBack; }
            set { dominoBack = value; }
        }
        public Domino(Tile tile1, Tile tile2, String dominoBack)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
            this.dominoBack = dominoBack;
        }
    }
}
