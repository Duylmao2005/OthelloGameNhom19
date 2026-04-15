using System;
using OthelloGame.Models;
using OthelloGame.AI;

namespace OthelloGame
{
    internal class Program
    {
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
    }
}