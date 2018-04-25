using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DataModels
{
    public class Tile
        {
        private BitmapImage tileImage;
        public BitmapImage TileImage
        {
            get { return tileImage; }
            set { tileImage = value; }
        }
        private TileType tileType;
        public TileType TileType
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

        public Tile(String tileImage, TileType tileType, int tileCrown)
        {
            this.tileImage = new BitmapImage(new Uri(tileImage, UriKind.Relative));
            this.tileType = tileType;
            this.tileCrown = tileCrown;
        }
    }
}
