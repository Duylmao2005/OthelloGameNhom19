using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using OthelloGame.Forms;
using OthelloGame.Models;

namespace OthelloGame
{
    internal static class SavedGameStore
    {
        private static readonly string _dir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OthelloGame");

        private static readonly string _file = Path.Combine(_dir, "savegame.json");

        public static bool Exists() => File.Exists(_file);

        public static void Clear()
        {
            try
            {
                if (File.Exists(_file)) File.Delete(_file);
            }
            catch
            {
                // ignore
            }
        }

        public static bool TryLoad(out SavedGameData? data)
        {
            data = null;
            try
            {
                if (!File.Exists(_file)) return false;
                var json = File.ReadAllText(_file);
                data = JsonSerializer.Deserialize<SavedGameData>(json);
                return data != null && data.Grid != null && data.Grid.Length == Board.Size * Board.Size;
            }
            catch
            {
                data = null;
                return false;
            }
        }

        public static void Save(SavedGameData data)
        {
            try
            {
                Directory.CreateDirectory(_dir);
                var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_file, json);
            }
            catch
            {
                // ignore
            }
        }

        public static SavedGameData FromGameState(Board board, PieceColor currentPlayer, int passCount,
            bool enableAI, bool aiVsAi, GameForm.AIDifficulty difficulty, PieceColor humanColor)
        {
            return new SavedGameData
            {
                Grid = ExportGrid(board),
                CurrentPlayer = currentPlayer,
                PassCount = passCount,
                EnableAI = enableAI,
                AiVsAi = aiVsAi,
                Difficulty = difficulty,
                HumanColor = humanColor
            };
        }

        public static Board ToBoard(SavedGameData data)
        {
            var b = new Board();

            // Board không có API reset grid, nên gán thẳng private field _grid.
            try
            {
                var f = typeof(Board).GetField("_grid", BindingFlags.Instance | BindingFlags.NonPublic);
                if (f != null)
                {
                    var grid = new PieceColor[Board.Size, Board.Size];
                    int i = 0;
                    for (int r = 0; r < Board.Size; r++)
                        for (int c = 0; c < Board.Size; c++)
                            grid[r, c] = (PieceColor)data.Grid[i++];
                    f.SetValue(b, grid);
                }
                else
                {
                    // Fallback: set từng ô (không xoá được 4 quân init, nhưng giữ để an toàn)
                    int i = 0;
                    for (int r = 0; r < Board.Size; r++)
                        for (int c = 0; c < Board.Size; c++)
                            b.SetPieceAt(r, c, (PieceColor)data.Grid[i++]);
                }
            }
            catch
            {
                // ignore
            }

            return b;
        }

        private static int[] ExportGrid(Board board)
        {
            var arr = new int[Board.Size * Board.Size];
            int i = 0;
            for (int r = 0; r < Board.Size; r++)
                for (int c = 0; c < Board.Size; c++)
                    arr[i++] = (int)board.GetPieceAt(r, c);
            return arr;
        }
    }

    public sealed class SavedGameData
    {
        public int[] Grid { get; set; } = Array.Empty<int>();
        public PieceColor CurrentPlayer { get; set; } = PieceColor.Black;
        public int PassCount { get; set; }

        public bool EnableAI { get; set; }
        public bool AiVsAi { get; set; }
        public GameForm.AIDifficulty Difficulty { get; set; } = GameForm.AIDifficulty.Normal;
        public PieceColor HumanColor { get; set; } = PieceColor.Black;
    }
}

