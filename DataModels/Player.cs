using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Player
    {
        private bool isOccupied;
        public bool IsOccupied{ get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }

        private Meeple meeple;
        public Meeple Meeple
        {
            get { return meeple; }
            set { this.meeple = value; }
        }

        private Board board;
        public Board Board
        {
            get { return board; }
            set { this.board = value; }
        }

        public Player(bool full, string name)
        {
            IsOccupied = full;
            Name = name;
            this.Board = new Board("blue");
        }
    }
}
