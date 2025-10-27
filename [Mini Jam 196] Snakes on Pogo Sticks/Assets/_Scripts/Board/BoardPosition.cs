using UnityEngine;

namespace _Scripts.Board
{
    /// <summary>
    /// BoardPosition class represents a position on an 8x8 game board.
    /// The position is defined by row and column indices.
    /// The position wil be 1 to 8 
    /// define position 1,1 as the bottlem-left corner of the board
    /// </summary>
    public class BoardPosition
    {
        
        private static int _maxRows = 8;
        private static int _maxCols = 8;

        /// <summary>
        /// The row index of the board position (1 to 8). with 1 being the bottom row.
        /// </summary>
        public int Row { get; private set; }
        /// <summary>
        /// The column index of the board position (1 to 8). with 1 being the leftmost column.
        /// </summary>
        public int Col { get; private set; }

        /// <summary>
        /// Creates a BoardPosition with the given row and column. This constructor is private; use CreateBoardPosition
        /// to create instances.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private BoardPosition(int row, int col)
        {
            Row = row;
            Col = col;
        }

        /// <summary>
        /// Sets the position to the given row and column if they are valid.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetPosition(int row, int col)
        {
            if (IsValidPosition(row, col)) return;
            
            Row = row;
            Col = col;

        }
        
        /// <summary>
        /// Creates a BoardPosition if the given row and column are valid; otherwise, returns null.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static BoardPosition CreateBoardPosition(int row, int col)
        {
            return !IsValidPosition(row, col) ? null : new BoardPosition(row, col);
        }

        /// <summary>
        /// Returns true if the given row and column are within the valid range of the board.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static bool IsValidPosition(int row, int col)
        {
            return row > 0 && row <= _maxRows && col > 0 && col <= _maxCols;
        }

        
        /// <summary>
        /// Moves the position in the given direction if the new position is valid.
        /// </summary>
        /// <param name="direction"></param>
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