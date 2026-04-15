using System;
using System.IO;
using System.Text.Json;
using OthelloGame.Forms;

namespace OthelloGame
{
    /// <summary>
    /// Lưu cấu hình runtime đơn giản (không phụ thuộc Settings.settings).
    /// File lưu tại %AppData%\OthelloGame\settings.json
    /// </summary>
    internal static class AppRuntime
    {
        private static readonly string _dir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OthelloGame");

        private static readonly string _file = Path.Combine(_dir, "settings.json");

        public static GameForm.AIDifficulty Difficulty { get; private set; } = GameForm.AIDifficulty.Normal;

        public static void Load()
        {
            try
            {
                if (!File.Exists(_file)) return;
                var json = File.ReadAllText(_file);
                var data = JsonSerializer.Deserialize<SettingsData>(json);
                if (data == null) return;
                Difficulty = data.Difficulty;
            }
            catch
            {
                // Nếu lỗi đọc file thì dùng mặc định
                Difficulty = GameForm.AIDifficulty.Normal;
            }
        }

        public static void SaveDifficulty(GameForm.AIDifficulty difficulty)
        {
            Difficulty = difficulty;
            try
            {
                Directory.CreateDirectory(_dir);
                var json = JsonSerializer.Serialize(new SettingsData { Difficulty = difficulty }, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(_file, json);
            }
            catch
            {
                // Không crash app nếu không ghi được file
            }
        }

        private sealed class SettingsData
        {
            public GameForm.AIDifficulty Difficulty { get; set; } = GameForm.AIDifficulty.Normal;
        }
    }
}

