using System.Collections.Generic;

namespace TicTacToe.Entities
{
    public class Board
    {
        public List<DiscPosition> DiscsOnBoard { get; set; }
        public int BoundaryX { get; set; }
        public int BoundaryY { get; set; }

    }
}