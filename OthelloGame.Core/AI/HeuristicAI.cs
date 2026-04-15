using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloGame.Models;

namespace OthelloGame.AI
{
    public class HeuristicAI
    {
        // ----------------------------------------------------------------
        // X-squares: ô chéo góc — nguy hiểm nhất (dễ cho đối thủ lấy góc)
        // ----------------------------------------------------------------
        private static readonly (int r, int c)[] X_SQUARES =
        {
            (1, 1), (1, 6), (6, 1), (6, 6)
        };

        // ----------------------------------------------------------------
        // C-squares: ô cạnh góc theo hàng/cột — cũng nguy hiểm
        // ----------------------------------------------------------------
        private static readonly (int r, int c)[] C_SQUARES =
        {
            (0, 1), (1, 0),
            (0, 6), (1, 7),
            (6, 0), (7, 1),
            (6, 7), (7, 6)
        };

        // ----------------------------------------------------------------
        // 4 góc
        // ----------------------------------------------------------------
        private static readonly (int r, int c)[] CORNERS =
        {
            (0, 0), (0, 7), (7, 0), (7, 7)
        };

        // Map góc -> X-square và 2 C-square liên quan
        // Dùng để bỏ qua penalty khi góc đó đã bị chiếm
        private static readonly Dictionary<(int, int), (int, int)[]> CORNER_DANGER_MAP =
            new Dictionary<(int, int), (int, int)[]>
            {
                { (0, 0), new[] { (1,1), (0,1), (1,0) } },
                { (0, 7), new[] { (1,6), (0,6), (1,7) } },
                { (7, 0), new[] { (6,1), (7,1), (6,0) } },
                { (7, 7), new[] { (6,6), (7,6), (6,7) } },
            };

        // ================================================================
        // EVALUATE
        // ================================================================
        public int Evaluate(Board board, PieceColor aiColor)
        {
            var opponent = aiColor.Opposite();

            int aiPieces = board.GetScore(aiColor);
            int oppPieces = board.GetScore(opponent);
            int total = aiPieces + oppPieces;

            // --- Giai đoạn game ---
            bool isEarlyGame = total < 20;
            bool isEndGame = total > 54;

            // --- 1. Piece count ---
            // Đầu/giữa game: ít quân hơn thường tốt hơn (linh hoạt hơn)
            // Cuối game: nhiều quân = thắng
            int pieceWeight = isEndGame ? 100 : (isEarlyGame ? 0 : 10);
            int pieceScore = aiPieces - oppPieces;

            // --- 2. Mobility ---
            // Quan trọng nhất ở đầu/giữa game
            var aiMoves = board.GetValidMoves(aiColor);
            var oppMoves = board.GetValidMoves(opponent);
            int mobilityWeight = isEndGame ? 5 : (isEarlyGame ? 30 : 20);
            int mobilityScore = aiMoves.Count - oppMoves.Count;

            // --- 3. Corner ---
            int cornerScore = CalculateCornerScore(board, aiColor, opponent);

            // --- 4. Corner Danger (X-square & C-square) ---
            // Chỉ phạt khi góc tương ứng còn trống
            int dangerScore = CalculateDangerScore(board, aiColor, opponent);

            // --- 5. Edge ---
            // Ít quan trọng hơn đầu game, tăng dần về cuối
            int edgeWeight = isEarlyGame ? 5 : 15;
            int edgeScore = CalculateEdgeScore(board, aiColor, opponent);

            // --- Tổng hợp ---
            return (pieceWeight * pieceScore)
                 + (mobilityWeight * mobilityScore)
                 + (50 * cornerScore)
                 + (30 * dangerScore)
                 + (edgeWeight * edgeScore);
        }

        // ================================================================
        // HELPERS
        // ================================================================

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
            // Xác định các ô nguy hiểm vẫn còn hiệu lực
            // (góc tương ứng phải còn trống thì mới phạt)
            var activeDangerSquares = new Dictionary<(int, int), int>(); // ô -> weight

            foreach (var corner in CORNERS)
            {
                var cornerPiece = board.GetPieceAt(corner.r, corner.c);
                // Góc đã bị chiếm → X/C-square xung quanh không còn nguy hiểm
                if (cornerPiece != PieceColor.Empty) continue;

                var dangerSquares = CORNER_DANGER_MAP[corner];
                // X-square (index 0): nguy hiểm hơn C-square (index 1, 2)
                for (int i = 0; i < dangerSquares.Length; i++)
                {
                    int weight = (i == 0) ? 3 : 1; // X-square nặng hơn
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
                if (piece == aiColor) score -= w; // AI chiếm ô nguy hiểm → bất lợi
                else if (piece == opponent) score += w;
            }
            return score;
        }

        private int CalculateEdgeScore(Board board, PieceColor aiColor, PieceColor opponent)
        {
            int score = 0;

            // Hàng trên và dưới (bỏ góc vì đã tính riêng)
            for (int c = 1; c < 7; c++)
            {
                var top = board.GetPieceAt(0, c);
                var bottom = board.GetPieceAt(7, c);

                if (top == aiColor) score++;
                else if (top == opponent) score--;

                if (bottom == aiColor) score++;
                else if (bottom == opponent) score--;
            }

            // Cột trái và phải (bỏ góc)
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