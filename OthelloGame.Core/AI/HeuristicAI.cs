using System;
using System.Collections.Generic;
using OthelloGame.Models;

namespace OthelloGame.AI
{
    public class HeuristicAI : IAIPlayer
    {
        public string Name => "Heuristic AI";

        // ================================================================
        // GetMove — duyệt tất cả nước đi, chọn nước có điểm cao nhất
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
                int score = Evaluate(cloned, aiColor);
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        // ================================================================
        // EVALUATE
        // ================================================================
        public int Evaluate(Board board, PieceColor aiColor)
        {
            var opponent = aiColor.Opposite();

            int aiPieces = board.GetScore(aiColor);
            int oppPieces = board.GetScore(opponent);
            int total = aiPieces + oppPieces;

            bool isEarlyGame = total < 20;
            bool isEndGame = total > 54;

            int pieceWeight = isEndGame ? 100 : (isEarlyGame ? 0 : 10);
            int pieceScore = aiPieces - oppPieces;

            var aiMoves = board.GetValidMoves(aiColor);
            var oppMoves = board.GetValidMoves(opponent);
            int mobilityWeight = isEndGame ? 5 : (isEarlyGame ? 30 : 20);
            int mobilityScore = aiMoves.Count - oppMoves.Count;

            int cornerScore = CalculateCornerScore(board, aiColor, opponent);
            int dangerScore = CalculateDangerScore(board, aiColor, opponent);

            int edgeWeight = isEarlyGame ? 5 : 15;
            int edgeScore = CalculateEdgeScore(board, aiColor, opponent);

            return (pieceWeight * pieceScore)
                 + (mobilityWeight * mobilityScore)
                 + (50 * cornerScore)
                 + (30 * dangerScore)
                 + (edgeWeight * edgeScore);
        }

        // ================================================================
        // HELPERS
        // ================================================================
        private static readonly (int r, int c)[] CORNERS =
        {
            (0, 0), (0, 7), (7, 0), (7, 7)
        };

        private static readonly Dictionary<(int, int), (int, int)[]> CORNER_DANGER_MAP =
            new Dictionary<(int, int), (int, int)[]>
            {
                { (0, 0), new[] { (1,1), (0,1), (1,0) } },
                { (0, 7), new[] { (1,6), (0,6), (1,7) } },
                { (7, 0), new[] { (6,1), (7,1), (6,0) } },
                { (7, 7), new[] { (6,6), (7,6), (6,7) } },
            };

        private int CalculateCornerScore(Board board, PieceColor aiColor, PieceColor opponent)
        {
            int score = 0;
            foreach (var (r, c) in CORNERS)
            {
                var piece = board.GetPieceAt(r, c);
                if (piece == aiColor) score++;
                else if (piece == opponent) score--;
            }
            return score;
        }

        private int CalculateDangerScore(Board board, PieceColor aiColor, PieceColor opponent)
        {
            var activeDangerSquares = new Dictionary<(int, int), int>();

            foreach (var corner in CORNERS)
            {
                var cornerPiece = board.GetPieceAt(corner.r, corner.c);
                if (cornerPiece != PieceColor.Empty) continue;

                var dangerSquares = CORNER_DANGER_MAP[corner];
                for (int i = 0; i < dangerSquares.Length; i++)
                {
                    int weight = (i == 0) ? 3 : 1;
                    var sq = dangerSquares[i];
                    if (!activeDangerSquares.ContainsKey(sq))
                        activeDangerSquares[sq] = weight;
                    else
                        activeDangerSquares[sq] = Math.Max(activeDangerSquares[sq], weight);
                }
            }

            int score = 0;
            foreach (var kvp in activeDangerSquares)
            {
                var (r, c) = kvp.Key;
                int w = kvp.Value;
                var piece = board.GetPieceAt(r, c);
                if (piece == aiColor) score -= w;
                else if (piece == opponent) score += w;
            }
            return score;
        }

        private int CalculateEdgeScore(Board board, PieceColor aiColor, PieceColor opponent)
        {
            int score = 0;
            for (int c = 1; c < 7; c++)
            {
                var top = board.GetPieceAt(0, c);
                var bottom = board.GetPieceAt(7, c);
                if (top == aiColor) score++;
                else if (top == opponent) score--;
                if (bottom == aiColor) score++;
                else if (bottom == opponent) score--;
            }
            for (int r = 1; r < 7; r++)
            {
                var left = board.GetPieceAt(r, 0);
                var right = board.GetPieceAt(r, 7);
                if (left == aiColor) score++;
                else if (left == opponent) score--;
                if (right == aiColor) score++;
                else if (right == opponent) score--;
            }
            return score;
        }
    }
}