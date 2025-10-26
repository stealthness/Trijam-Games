using UnityEngine;

namespace _Scripts.Managers
{
    /// <summary>
    /// BoardPosition class represents a position on an 8x8 game board.
    /// The position is defined by row and column indices.
    /// The position wil be 1 to 8 
    /// define position 1,1 as the bottlem-left corner of the board
    /// </summary>
    public class BoardPosition
    {
        
        private static int MaxRows = 8;
        private static int MaxCols = 8;

        public int Row { get; private set; }
        public int Col { get; private set; }

        private BoardPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public void SetPosition(int row, int col)
        {
            if (IsValidPosition(row, col)) return;
            
            Row = row;
            Col = col;

        }
        
        public static BoardPosition CreateBoardPosition(int row, int col)
        {
            return !IsValidPosition(row, col) ? null : new BoardPosition(row, col);
        }

        public static bool IsValidPosition(int row, int col)
        {
            return row > 0 && row <= MaxRows && col > 0 && col <= MaxCols;
        }

        public void MovePosition(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                if (IsValidPosition(Row + 1, Col))
                {
                    Row += 1;
                }
            }
            else if (direction == Vector2.down)
            {
                if (IsValidPosition(Row - 1, Col))
                {
                    Row -= 1;
                }
            }
            else if (direction == Vector2.left)
            {
                if (IsValidPosition(Row, Col - 1))
                {
                    Col -= 1;
                }
            }
            else if (direction == Vector2.right)
            {
                if (IsValidPosition(Row, Col + 1))
                {
                    Col += 1;
                }
            }
        }
    }
}