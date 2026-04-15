<<<<<<< Updated upstream
﻿class Program
{
    static void Main()
    {
        ModelTests.RunAll();
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
>>>>>>> Stashed changes
    }
}
