<<<<<<< HEAD
using System;
using OthelloGame.Models;
using OthelloGame.AI;

<<<<<<< HEAD
class Program
=======
namespace OthelloGame
>>>>>>> f915b2c226942b50ae0e39b73c45c31826da018a
{
    internal class Program
    {
<<<<<<< HEAD
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
=======
        static void Main(string[] args)
        {
            // ===============================
            // Tạo bàn cờ mặc định
            // ===============================
            Board board = new Board();

            // ===============================
            // Tạo AI Greedy
            // ===============================
            IAIPlayer ai = new GreedyAI();

            // ===============================
            // In bàn cờ hiện tại
            // ===============================
            Console.WriteLine("=== BAN CO HIEN TAI ===");
            PrintBoard(board);

            // ===============================
            // AI tìm nước đi cho quân Đen
            // ===============================
            var move = ai.GetMove(board, PieceColor.Black);

            Console.WriteLine();
            Console.WriteLine("AI: " + ai.Name);
            Console.WriteLine("Nuoc di tot nhat cho BLACK:");
            Console.WriteLine($"Row = {move.row}, Col = {move.col}");

            Console.ReadKey();
        }

        // ===================================
        // In bàn cờ ra màn hình
        // ===================================
        static void PrintBoard(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PieceColor p = board.GetPieceAt(i, j);

                    if (p == PieceColor.Empty)
                        Console.Write(". ");
                    else if (p == PieceColor.Black)
                        Console.Write("B ");
                    else
                        Console.Write("W ");
                }

                Console.WriteLine();
            }
        }
>>>>>>> f915b2c226942b50ae0e39b73c45c31826da018a
=======
﻿using System;
using OthelloGame.AI;
using OthelloGame.Models;

namespace OthelloGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();

            IAIPlayer ai = new MinimaxAI(depth: 3);

            PieceColor aiColor = PieceColor.Black;
            PieceColor humanColor = PieceColor.White;

            PieceColor current = PieceColor.Black;

            while (!board.IsGameOver())
            {
                Console.Clear();
                board.PrintToConsole();

                Console.WriteLine($"\nCurrent turn: {current}");

                var validMoves = board.GetValidMoves(current);

                if (validMoves.Count == 0)
                {
                    Console.WriteLine("No valid moves -> pass turn");
                    current = current.Opposite();
                    Console.ReadKey();
                    continue;
                }

                if (current == aiColor)
                {
                    Console.WriteLine("AI is thinking...");

                    var move = ai.GetMove(board, aiColor);

                    Console.WriteLine($"AI plays: {move.row}, {move.col}");

                    board.ApplyMove(move.row, move.col, aiColor);
                }
                else
                {
                    Console.WriteLine("Your valid moves:");

                    for (int i = 0; i < validMoves.Count; i++)
                    {
                        Console.WriteLine($"{i}: {validMoves[i].row}, {validMoves[i].col}");
                    }

                    Console.Write("Choose move index: ");

                    string input = Console.ReadLine();

                    if (!int.TryParse(input, out int idx) || idx < 0 || idx >= validMoves.Count)
                    {
                        Console.WriteLine("Invalid input!");
                        Console.ReadKey();
                        continue;
                    }

                    var move = validMoves[idx];
                    board.ApplyMove(move.row, move.col, humanColor);
                }

                current = current.Opposite();
            }

            Console.Clear();
            board.PrintToConsole();

            Console.WriteLine("\nGame Over!");

            Console.WriteLine($"Black: {board.GetScore(PieceColor.Black)}");
            Console.WriteLine($"White: {board.GetScore(PieceColor.White)}");

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
>>>>>>> feature/ai
    }
}