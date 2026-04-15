using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloGame.Models;

namespace OthelloGame.AI
{
    public class GreedyAI : IAIPlayer
    {
        public string Name => "Greedy AI";

        // ==========================================
        // Trả về nước đi tốt nhất
        // Ưu tiên nước lật được nhiều quân nhất
        // ==========================================
        public (int row, int col) GetMove(Board board, PieceColor aiColor)
        {
            List<(int row, int col)> validMoves = board.GetValidMoves(aiColor);

            if (validMoves.Count == 0)
                return (-1, -1);

            int bestScore = -1;
            (int row, int col) bestMove = validMoves[0];

            foreach (var move in validMoves)
            {
                int score = CountFlippedPieces(board, move.row, move.col, aiColor);

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        // ==========================================
        // Đếm số quân sẽ lật nếu đi nước này
        // ==========================================
        private int CountFlippedPieces(Board board, int row, int col, PieceColor aiColor)
        {
            PieceColor enemy =
                aiColor == PieceColor.Black
                ? PieceColor.White
                : PieceColor.Black;

            int total = 0;

            int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int d = 0; d < 8; d++)
            {
                int r = row + dr[d];
                int c = col + dc[d];
                int count = 0;

                // ✅ SỬA 1: IsInside → IsInBounds
                // ✅ SỬA 2: Cells → GetPieceAt
                while (board.IsInBounds(r, c) &&
                       board.GetPieceAt(r, c) == enemy)
                {
                    count++;
                    r += dr[d];
                    c += dc[d];
                }

                if (count > 0 &&
                    board.IsInBounds(r, c) &&
                    board.GetPieceAt(r, c) == aiColor)
                {
                    total += count;
                }
            }

            return total;
        }
    }
}