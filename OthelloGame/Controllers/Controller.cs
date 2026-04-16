using System.Collections.Generic;
using OthelloGame.Models;   // Models: Board, GameState, PieceColor
using OthelloGame.AI;       // AI: IAIPlayer, GreedyAI, HeuristicAI, MinimaxAI

namespace OthelloGame.Core.Controllers
{
    internal class GameController
    {
        private GameState _gameState;
        private IAIPlayer _aiPlayer;

        public GameController(IAIPlayer aiPlayer = null)
        {
            _gameState = new GameState();
            _aiPlayer = aiPlayer;
        }

        // Bắt đầu game mới
        public void StartGame()
        {
            _gameState = new GameState();
        }

        // Người chơi thực hiện nước đi
        public bool HandlePlayerMove(int row, int col)
        {
            return _gameState.MakeMove(row, col);
        }

        // AI thực hiện nước đi
        public void HandleAIMove()
        {
            if (_aiPlayer != null)
            {
                var move = _aiPlayer.GetMove(_gameState.Board, _gameState.CurrentPlayer);
                if (move.row >= 0 && move.col >= 0)
                {
                    _gameState.MakeMove(move.row, move.col);
                }
                else
                {
                    _gameState.PassTurn();
                }
            }
        }

        // Lấy danh sách nước đi hợp lệ của người hiện tại
        public List<(int row, int col)> GetValidMoves()
        {
            return _gameState.GetValidMoves();
        }

        // Lấy điểm số hiện tại
        public (int blackScore, int whiteScore) GetScores()
        {
            return _gameState.GetScores();
        }

        // Kiểm tra game kết thúc chưa
        public bool IsGameOver()
        {
            return _gameState.IsGameOver();
        }

        // In bàn cờ ra console (dùng để test)
        public void PrintBoard()
        {
            _gameState.Board.PrintToConsole();
        }

        // Lấy người thắng
        public PieceColor GetWinner()
        {
            return _gameState.GetWinner();
        }

        // Lấy người chơi hiện tại
        public PieceColor GetCurrentPlayer()
        {
            return _gameState.CurrentPlayer;
        }

        // Nếu người hiện tại hết nước đi, tự pass sang người kia
        public bool EnsureCurrentPlayerCanMove()
        {
            return _gameState.EnsureCurrentPlayerCanMove();
        }
    }
}
