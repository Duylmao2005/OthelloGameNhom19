using System;
using OthelloGame.Models;
using OthelloGame.AI;

class Program
{
    static void Main()
    {
        var game = new GameState();
        var ai = new HeuristicAI();

        Console.WriteLine("=== BAT DAU GAME ===");
        game.Board.PrintToConsole();

        // Chạy 3 nước đi
        for (int turn = 1; turn <= 3; turn++)
        {
            Console.WriteLine($"\n=== LUOT {turn} ===");
            Console.WriteLine($"Nguoi choi: {game.CurrentPlayer}");

            var moves = game.GetValidMoves();

            if (moves.Count == 0)
            {
                Console.WriteLine("Khong co nuoc di -> PASS");
                game.PassTurn();
                continue;
            }

            int bestScore = int.MinValue;
            (int row, int col) bestMove = moves[0];

            // Tìm nước tốt nhất
            foreach (var move in moves)
            {
                var clone = game.Board.Clone();
                clone.ApplyMove(move.row, move.col, game.CurrentPlayer);

                int score = ai.Evaluate(clone, game.CurrentPlayer);

                Console.WriteLine($"Thu nuoc ({move.row},{move.col}) => score = {score}");

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            Console.WriteLine($"=> Chon nuoc: ({bestMove.row},{bestMove.col}) | Score: {bestScore}");

            // Thực hiện nước đi thật
            game.MakeMove(bestMove.row, bestMove.col);

            Console.WriteLine("Ban co sau khi di:");
            game.Board.PrintToConsole();
        }

        Console.WriteLine("\n=== KET THUC TEST ===");
        var (black, white) = game.GetScores();
        Console.WriteLine($"Diem: Black = {black}, White = {white}");

        Console.ReadKey();
    }
}