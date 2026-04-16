using System;
using System.Windows.Forms;
using OthelloGame;
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
            btnContinue.Click += BtnContinue_Click;
            btnHuongDan.Click += BtnHuongDan_Click;
        }

        private void MainMenuForm_Load(object? sender, EventArgs e)
        {
            // đảm bảo đọc cấu hình đã lưu
            AppRuntime.Load();

            // enable/disable Continue theo savegame
            btnContinue.Enabled = SavedGameStore.Exists();
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
            // AI play: mặc định cho 2 AI tự chơi, nhưng cho phép chọn mode
            var choice = MessageBox.Show(
                this,
                "Chọn chế độ AI play:\n\nYes = AI vs AI\nNo = Bạn vs AI (AI đi trước)\nCancel = Hủy",
                "AI play",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (choice == DialogResult.Cancel) return;
            if (choice == DialogResult.Yes)
            {
                OpenAIVsAIGame();
                return;
            }

            // No: Human vs AI nhưng human đi Trắng để AI đi trước
            OpenGame(enableAI: true, humanColor: PieceColor.White);
        }

        private void BtnPvP_Click(object? sender, EventArgs e)
        {
            // PvP = không AI
            OpenGame(enableAI: false, humanColor: PieceColor.Black);
        }

        private void BtnContinue_Click(object? sender, EventArgs e)
        {
            if (!SavedGameStore.TryLoad(out var saved) || saved == null)
            {
                btnContinue.Enabled = false;
                MessageBox.Show(this, "Không tìm thấy ván đã lưu.", "Continue", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var g = new GameForm(saved);
            Hide();
            g.ShowDialog(this);
            Show();

            // sau khi quay lại menu, refresh trạng thái nút Continue
            btnContinue.Enabled = SavedGameStore.Exists();
        }

        private void BtnHuongDan_Click(object? sender, EventArgs e)
        {
            using var f = new TutorialForm();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog(this);
        }

        private void OpenGame(bool enableAI, PieceColor humanColor)
        {
            var diff = AppRuntime.Difficulty;
            using var g = new GameForm(enableAI, diff, humanColor);
            Hide();
            g.ShowDialog(this);
            Show();

            // New game đã chạy → nếu có save cũ, người chơi có thể đã overwrite/clear
            btnContinue.Enabled = SavedGameStore.Exists();
        }

        private void OpenAIVsAIGame()
        {
            var diff = AppRuntime.Difficulty;
            using var g = GameForm.CreateAIVsAI(diff);
            Hide();
            g.ShowDialog(this);
            Show();
            btnContinue.Enabled = SavedGameStore.Exists();
        }

        // giữ lại handler cũ để Designer không lỗi (đang gắn ở btnPvP)
        private void button3_Click(object sender, EventArgs e)
        {
            BtnPvP_Click(sender, e);
        }
    }
}
