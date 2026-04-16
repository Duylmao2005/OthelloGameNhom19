using System.Collections.Generic;

namespace OthelloGame.Models
{
    public class GameState
    {
        public Board Board { get; private set; }

        // Người đang đi
        public PieceColor CurrentPlayer { get; private set; }

        // Lượt bị bỏ (pass)
        public int PassCount { get; private set; }

        public GameState()
        {
            Board = new Board();
            CurrentPlayer = PieceColor.Black; // đen đi trước
            PassCount = 0;
        }

        // Lấy danh sách nước đi hợp lệ của người hiện tại
        public List<(int row, int col)> GetValidMoves()
        {
            return Board.GetValidMoves(CurrentPlayer);
        }

        // Thực hiện 1 nước đi
        public bool MakeMove(int row, int col)
        {
            if (!Board.IsValidMove(row, col, CurrentPlayer))
                return false;

            Board.ApplyMove(row, col, CurrentPlayer);

            // Reset pass vì đã đi được
            PassCount = 0;

            SwitchPlayer();
            AutoPassIfNoMoves();
            return true;
        }

        // Bỏ lượt nếu không có nước đi
        public void PassTurn()
        {
            PassCount++;
            SwitchPlayer();
            AutoPassIfNoMoves();
        }

        // Đổi lượt
        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer.Opposite();
        }

        /// <summary>
        /// Nếu người hiện tại không có nước đi hợp lệ, tự động pass sang người còn lại.
        /// Trường hợp cả 2 đều không có nước đi thì game over (PassCount sẽ tăng tới 2).
        /// </summary>
        public bool EnsureCurrentPlayerCanMove()
        {
            if (IsGameOver()) return false;
            if (Board.GetValidMoves(CurrentPlayer).Count > 0) return false;

            PassTurn();
            return true;
        }

        private void AutoPassIfNoMoves()
        {
            // Auto-pass liên tiếp trong endgame (tối đa 2 lần để kết thúc)
            while (PassCount < 2 && !Board.IsGameOver() && Board.GetValidMoves(CurrentPlayer).Count == 0)
            {
                PassCount++;
                SwitchPlayer();
            }
        }

        // Kiểm tra có phải pass không
        public bool HasValidMove(PieceColor player)
        {
            return Board.GetValidMoves(player).Count > 0;
        }

        // Game kết thúc khi:
        // - Cả 2 không đi được (pass 2 lần)
        // - Hoặc board logic đã kết thúc
        public bool IsGameOver()
        {
            return PassCount >= 2 || Board.IsGameOver();
        }

        // Lấy điểm
        public (int blackScore, int whiteScore) GetScores()
        {
            return (
                Board.GetScore(PieceColor.Black),
                Board.GetScore(PieceColor.White)
            );
        }

        // Xác định người thắng
        public PieceColor GetWinner()
        {
            var (black, white) = GetScores();

            if (black > white) return PieceColor.Black;
            if (white > black) return PieceColor.White;
            return PieceColor.Empty; // hòa
        }
    }
}
