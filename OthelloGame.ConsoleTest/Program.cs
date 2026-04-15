using System;
using OthelloGame.Models;
using OthelloGame.AI;

namespace OthelloGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TEST GREEDY AI ===\n");

            GameState state = new GameState();
            GreedyAI greedyAI = new GreedyAI();

            int turnCount = 0;

            while (!state.IsGameOver())
            {
                turnCount++;
                Console.WriteLine($"--- Lượt {turnCount} | Người đi: {state.CurrentPlayer} ---");
                state.Board.PrintToConsole();

                var validMoves = state.GetValidMoves();

                // Không có nước đi → pass
                if (validMoves.Count == 0)
                {
                    Console.WriteLine($"{state.CurrentPlayer} không có nước đi → PASS\n");
                    state.PassTurn();
                    continue;
                }

                (int row, int col) move;

                // Đen → người chơi tự nhập
                if (state.CurrentPlayer == PieceColor.Black)
                {
                    Console.WriteLine("Nước đi hợp lệ: ");
                    foreach (var m in validMoves)
                        Console.Write($"({m.row},{m.col}) ");
                    Console.WriteLine();
                    Console.Write("Bạn chọn (row col): ");
                    int r = int.Parse(Console.ReadLine());
                    int c = int.Parse(Console.ReadLine());
                    move = (r, c);
                }
                // Trắng → GreedyAI tự chọn
                else
                {
                    move = greedyAI.GetMove(state.Board, state.CurrentPlayer);
                    Console.WriteLine($"Greedy AI chọn: ({move.row}, {move.col})");
                }

                state.MakeMove(move.row, move.col);
                Console.WriteLine();
            }

            // Kết quả
            var (black, white) = state.GetScores();
            Console.WriteLine("=== KẾT QUẢ ===");
            Console.WriteLine($"Đen (bạn):     {black} quân");
            Console.WriteLine($"Trắng (Greedy): {white} quân");

            PieceColor winner = state.GetWinner();
            if (winner == PieceColor.Empty)
                Console.WriteLine("Kết quả: HÒA!");
            else
                Console.WriteLine($"Người thắng: {winner}");

            Console.ReadKey();
        }
    }
}