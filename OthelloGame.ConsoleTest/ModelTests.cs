using System;
using OthelloGame.Models;

public static class ModelTests
{
    static void Assert(bool condition, string message)
    {
        if (condition)
            Console.WriteLine("PASS: " + message);
        else
            Console.WriteLine("FAIL: " + message);
    }

    public static void RunAll()
    {
        TestInitialBoard();
        TestValidMoves();
        TestApplyMove();
        TestInvalidMove();
        TestPassTurn();
        TestClone();
        TestGameOver();
    }
    static void TestInitialBoard()
    {
        var board = new Board();

        Assert(board.GetScore(PieceColor.Black) == 2, "Initial black = 2");
        Assert(board.GetScore(PieceColor.White) == 2, "Initial white = 2");
    }

    static void TestValidMoves()
    {
        var game = new GameState();
        var moves = game.GetValidMoves();

        Assert(moves.Count == 4, "Initial valid moves = 4");
    }

    static void TestApplyMove()
    {
        var game = new GameState();

        game.MakeMove(2, 3); // nước opening chuẩn

        var black = game.Board.GetScore(PieceColor.Black);
        var white = game.Board.GetScore(PieceColor.White);

        Assert(black == 4 && white == 1, "ApplyMove flips correctly");
    }
    static void TestInvalidMove()
    {
        var game = new GameState();

        bool ok = game.MakeMove(0, 0);

        Assert(ok == false, "Invalid move rejected");
    }
    static void TestPassTurn()
    {
        var game = new GameState();

        var before = game.CurrentPlayer;
        game.PassTurn();
        var after = game.CurrentPlayer;

        Assert(before != after, "PassTurn switches player");
    }

    static void TestClone()
    {
        var board = new Board();
        var clone = board.Clone();

        clone.SetPieceAt(0, 0, PieceColor.Black);

        Assert(board.GetPieceAt(0, 0) == PieceColor.Empty, "Clone independent");
    }

    static void TestGameOver()
    {
        var game = new GameState();

        // spam move đến khi hết (simple)
        for (int i = 0; i < 200 && !game.IsGameOver(); i++)
        {
            var moves = game.GetValidMoves();
            if (moves.Count == 0)
                game.PassTurn();
            else
                game.MakeMove(moves[0].row, moves[0].col);
        }

        Assert(game.IsGameOver(), "Game eventually ends");
    }
}


