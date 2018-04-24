<<<<<<< HEAD
﻿using System;
=======
using System;
>>>>>>> UI-Development2
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using System.Windows.Media.Imaging;
>>>>>>> UI-Development2

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
<<<<<<< HEAD
        private String dominoBack;
        public String DominoBack
=======
        private BitmapImage dominoBack;
        public BitmapImage DominoBack
>>>>>>> UI-Development2
        {
            get { return dominoBack; }
            set { dominoBack = value; }
        }
<<<<<<< HEAD
        public Domino(Tile tile1, Tile tile2, String dominoBack)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
            this.dominoBack = dominoBack;
=======

        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public Domino(Tile tile1, Tile tile2, String dominoBack, int number)
        {
            this.tile1 = tile1;
            this.tile2 = tile2;
            this.dominoBack = new BitmapImage(new Uri(dominoBack, UriKind.Relative));
            this.number = number;
>>>>>>> UI-Development2
        }
    }
}
