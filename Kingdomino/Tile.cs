using System;
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
    public class Tile
        {
<<<<<<< HEAD
        private String tileImage;
        public String TileImage
=======
        private BitmapImage tileImage;
        public BitmapImage TileImage
>>>>>>> UI-Development2
        {
            get { return tileImage; }
            set { tileImage = value; }
        }
<<<<<<< HEAD
        private String tileType;
        public String TileType
=======
        private TileType tileType;
        public TileType TileType
>>>>>>> UI-Development2
        {
            get { return tileType; }
            set { tileType = value; }
        }
        private int tileCrown;
        public int TileCrown
        {
            get { return tileCrown; }
            set { tileCrown = value; }
        }

<<<<<<< HEAD
        public Tile(String tileImage, String tileType, int tileCrown)
        {
            this.tileImage = tileImage;
=======
        public Tile(String tileImage, TileType tileType, int tileCrown)
        {
            this.tileImage = new BitmapImage(new Uri(tileImage, UriKind.Relative));
>>>>>>> UI-Development2
            this.tileType = tileType;
            this.tileCrown = tileCrown;
        }
    }
}
