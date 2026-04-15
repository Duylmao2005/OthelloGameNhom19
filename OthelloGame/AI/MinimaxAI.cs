using System;
using System.Collections.Generic;
using OthelloGame.Models;

namespace OthelloGame.AI
{
    public class MinimaxAI : IAIPlayer
    {
        public string Name => "Minimax AI";

        // Độ sâu tìm kiếm — càng cao càng mạnh nhưng càng chậm
        private readonly int _depth;
        private readonly HeuristicAI _heuristic;

        public MinimaxAI(int depth = 5)
        {
            _depth = depth;
            _heuristic = new HeuristicAI();
        }

        // ================================================================
        // GetMove — tìm nước đi tốt nhất bằng Minimax + Alpha-Beta
        // ================================================================
        public (int row, int col) GetMove(Board board, PieceColor aiColor)
        {
            var validMoves = board.GetValidMoves(aiColor);
            if (validMoves.Count == 0)
                return (-1, -1);

            int bestScore = int.MinValue;
            (int row, int col) bestMove = validMoves[0];

            foreach (var move in validMoves)
            {
                Board cloned = board.Clone();
                cloned.ApplyMove(move.row, move.col, aiColor);

                // Sau khi AI đi, đến lượt đối thủ (Minimize)
                int score = Minimax(
                    cloned,
                    _depth - 1,
                    int.MinValue,
                    int.MaxValue,
                    false,          // lượt tiếp theo là đối thủ → minimize
                    aiColor
                );

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        // ================================================================
        // Minimax + Alpha-Beta Pruning
        //
        // board      : trạng thái bàn cờ hiện tại
        // depth      : độ sâu còn lại
        // alpha      : điểm tốt nhất mà Maximizer đã tìm được
        // beta       : điểm tốt nhất mà Minimizer đã tìm được
        // isMaximizing: true = lượt AI, false = lượt đối thủ
        // aiColor    : màu của AI (để tính điểm đúng chiều)
        // ================================================================
        private int Minimax(Board board, int depth, int alpha, int beta,
                            bool isMaximizing, PieceColor aiColor)
        {
            PieceColor currentPlayer = isMaximizing
                ? aiColor
                : aiColor.Opposite();

            var validMoves = board.GetValidMoves(currentPlayer);

            // --- Điều kiện dừng ---
            // Hết độ sâu hoặc game kết thúc
            if (depth == 0 || board.IsGameOver())
                return _heuristic.Evaluate(board, aiColor);

            // Không có nước đi → pass, đổi lượt nhưng không giảm độ sâu
            if (validMoves.Count == 0)
                return Minimax(board, depth, alpha, beta, !isMaximizing, aiColor);

            if (isMaximizing)
            {
                int maxScore = int.MinValue;

                foreach (var move in validMoves)
                {
                    Board cloned = board.Clone();
                    cloned.ApplyMove(move.row, move.col, currentPlayer);

                    int score = Minimax(cloned, depth - 1, alpha, beta, false, aiColor);
                    maxScore = Math.Max(maxScore, score);
                    alpha = Math.Max(alpha, score);

                    // Beta cut-off — đối thủ sẽ không chọn nhánh này
                    if (beta <= alpha) break;
                }

                return maxScore;
            }
            else
            {
                int minScore = int.MaxValue;

                foreach (var move in validMoves)
                {
                    Board cloned = board.Clone();
                    cloned.ApplyMove(move.row, move.col, currentPlayer);

                    int score = Minimax(cloned, depth - 1, alpha, beta, true, aiColor);
                    minScore = Math.Min(minScore, score);
                    beta = Math.Min(beta, score);

                    // Alpha cut-off — AI sẽ không chọn nhánh này
                    if (beta <= alpha) break;
                }

                return minScore;
            }
        }
    }
}