using System;
using System.Collections.Generic;

namespace OthelloGame.Models
{
    public class Board
    {
        public const int Size = 8;
        private PieceColor[,] _grid;

        // 8 hướng: ngang, dọc, chéo
        private readonly int[] _dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        private readonly int[] _dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

        public Board()
        {
            _grid = new PieceColor[Size, Size];
            InitBoard();
        }

        // Đặt 4 quân ban đầu ở trung tâm
        private void InitBoard()
        {
            _grid[3, 3] = PieceColor.White;
            _grid[3, 4] = PieceColor.Black;
            _grid[4, 3] = PieceColor.Black;
            _grid[4, 4] = PieceColor.White;
        }

        // Lấy màu quân tại ô (row, col)
        public PieceColor GetPieceAt(int row, int col)
        {
            return _grid[row, col];
        }

        // Đặt quân tại ô (row, col) — dùng nội bộ
        public void SetPieceAt(int row, int col, PieceColor color)
        {
            _grid[row, col] = color;
        }

        // Kiểm tra ô có nằm trong bàn cờ không
        public bool IsInBounds(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        // Kiểm tra nước đi có hợp lệ không
        public bool IsValidMove(int row, int col, PieceColor color)
        {
            // Ô phải trống
            if (_grid[row, col] != PieceColor.Empty) return false;

            // Phải lật được ít nhất 1 quân đối thủ
            return GetFlippedPieces(row, col, color).Count > 0;
        }

        // Lấy danh sách tất cả nước đi hợp lệ
        public List<(int row, int col)> GetValidMoves(PieceColor color)
        {
            var moves = new List<(int, int)>();
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (IsValidMove(r, c, color))
                        moves.Add((r, c));
            return moves;
        }

        // Lấy danh sách quân sẽ bị lật nếu đặt tại (row, col)
        public List<(int row, int col)> GetFlippedPieces(int row, int col, PieceColor color)
        {
            var flipped = new List<(int, int)>();

            for (int d = 0; d < 8; d++)
            {
                var temp = new List<(int, int)>();
                int r = row + _dx[d];
                int c = col + _dy[d];

                // Đi theo hướng d, thu thập quân đối thủ
                while (IsInBounds(r, c) && _grid[r, c] == color.Opposite())
                {
                    temp.Add((r, c));
                    r += _dx[d];
                    c += _dy[d];
                }

                // Nếu kết thúc bằng quân cùng màu thì lật hết temp
                if (IsInBounds(r, c) && _grid[r, c] == color && temp.Count > 0)
                    flipped.AddRange(temp);
            }

            return flipped;
        }

        // Thực hiện nước đi: đặt quân + lật quân đối thủ
        public void ApplyMove(int row, int col, PieceColor color)
        {
            _grid[row, col] = color;
            foreach (var (r, c) in GetFlippedPieces(row, col, color))
                _grid[r, c] = color;
        }

        // Đếm số quân của 1 màu
        public int GetScore(PieceColor color)
        {
            int count = 0;
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (_grid[r, c] == color) count++;
            return count;
        }

        // Kiểm tra game kết thúc chưa
        public bool IsGameOver()
        {
            return GetValidMoves(PieceColor.Black).Count == 0
                && GetValidMoves(PieceColor.White).Count == 0;
        }

        // Sao chép bàn cờ — MinimaxAI cần cái này
        public Board Clone()
        {
            var clone = new Board();
            Array.Copy(_grid, clone._grid, _grid.Length);
            return clone;
        }

        // In bàn cờ ra console để test
        public void PrintToConsole()
        {
            Console.WriteLine("  0 1 2 3 4 5 6 7");
            for (int r = 0; r < Size; r++)
            {
                Console.Write(r + " ");
                for (int c = 0; c < Size; c++)
                {
                    char ch = _grid[r, c] == PieceColor.Black ? 'B'
                            : _grid[r, c] == PieceColor.White ? 'W' : '.';
                    Console.Write(ch + " ");
                }
                Console.WriteLine();
            }
        }
    }
}