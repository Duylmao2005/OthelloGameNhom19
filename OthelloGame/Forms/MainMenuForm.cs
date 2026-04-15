using System;
using System.Windows.Forms;
using OthelloGame.Models;

namespace OthelloGame.Forms
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();

            Load += MainMenuForm_Load;
            btnSettings.Click += BtnSettings_Click;
            btnPlaySingle.Click += BtnPlaySingle_Click;
            btnAIPlay.Click += BtnAIPlay_Click;
            btnPvP.Click += BtnPvP_Click;
        }

        private void MainMenuForm_Load(object? sender, EventArgs e)
        {
            // đảm bảo đọc cấu hình đã lưu
            AppRuntime.Load();
        }

        private void BtnSettings_Click(object? sender, EventArgs e)
        {
            using var f = new SettingForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);

            // sau khi đóng setting, load lại
            AppRuntime.Load();
        }

        private void BtnPlaySingle_Click(object? sender, EventArgs e)
        {
            // Chơi đơn = Human vs AI, dùng độ khó đã lưu
            OpenGame(enableAI: true, humanColor: PieceColor.Black);
        }

        private void BtnAIPlay_Click(object? sender, EventArgs e)
        {
            // "AI play" (ứng biến): Human vs AI nhưng human đi Trắng để AI đi trước
            OpenGame(enableAI: true, humanColor: PieceColor.White);
        }

        private void BtnPvP_Click(object? sender, EventArgs e)
        {
            // PvP = không AI
            OpenGame(enableAI: false, humanColor: PieceColor.Black);
        }

        private void OpenGame(bool enableAI, PieceColor humanColor)
        {
            var diff = AppRuntime.Difficulty;
            using var g = new GameForm(enableAI, diff, humanColor);
            Hide();
            g.ShowDialog(this);
            Show();
        }

        // giữ lại handler cũ để Designer không lỗi (đang gắn ở btnPvP)
        private void button3_Click(object sender, EventArgs e)
        {
            BtnPvP_Click(sender, e);
        }
    }
}
