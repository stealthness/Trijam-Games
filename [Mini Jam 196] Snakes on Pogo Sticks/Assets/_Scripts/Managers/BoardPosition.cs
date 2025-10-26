using UnityEngine;

namespace _Scripts.Managers
{
    public class BoardPosition
    {
        
        private static int MaxRows = 8;
        private static int MaxCols = 8;

        public int row { get; set; }
        public int col { get; set; }

        private BoardPosition(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public void SetPosition(int row, int col)
        {
            if ((row >= MaxRows || col >= MaxCols) && (row < 0 || col < 0)) return;
            
            this.row = row;
            this.col = col;

        }
        
        public static BoardPosition CreateBoardPosition(int row, int col)
        {
            if ((row >= MaxRows || col >= MaxCols) && (row < 0 || col < 0)) return null;
            return new BoardPosition(row, col);
        }

        public static bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < MaxRows && col >= 0 && col < MaxCols;
        }

        public void MovePosition(Vector2 direction)
        {
            if (direction == Vector2.up)
            {
                if (IsValidPosition(row + 1, col))
                {
                    row -= 1;
                }
            }
            else if (direction == Vector2.down)
            {
                if (IsValidPosition(row - 1, col))
                {
                    row += 1;
                }
            }
            else if (direction == Vector2.left)
            {
                if (IsValidPosition(row, col - 1))
                {
                    col -= 1;
                }
            }
            else if (direction == Vector2.right)
            {
                if (IsValidPosition(row, col + 1))
                {
                    col += 1;
                }
            }
        }
    }
}