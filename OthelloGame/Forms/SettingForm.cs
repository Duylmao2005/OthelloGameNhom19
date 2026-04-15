using System;
using System.Windows.Forms;
using OthelloGame;

namespace OthelloGame.Forms
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            Load += SettingForm_Load;
            FormClosing += SettingForm_FormClosing; // đóng bằng nút X cũng lưu

            ratioBtnEasy.CheckedChanged += (_, __) => SaveIfChecked();
            ratioBtnNormal.CheckedChanged += (_, __) => SaveIfChecked();
            ratioBtnHard.CheckedChanged += (_, __) => SaveIfChecked();
        }

        private void SettingForm_Load(object? sender, EventArgs e)
        {
            // Đổ cấu hình hiện tại lên UI
            switch (AppRuntime.Difficulty)
            {
                case GameForm.AIDifficulty.Easy:
                    ratioBtnEasy.Checked = true;
                    break;
                case GameForm.AIDifficulty.Normal:
                    ratioBtnNormal.Checked = true;
                    break;
                case GameForm.AIDifficulty.Hard:
                    ratioBtnHard.Checked = true;
                    break;
                default:
                    ratioBtnNormal.Checked = true;
                    break;
            }
        }

        private void SettingForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Đảm bảo luôn lưu khi đóng form
            PersistDifficulty();
        }

        private void SaveIfChecked()
        {
            // Chỉ lưu khi radio vừa được chọn (Checked = true)
            PersistDifficulty();
        }

        private void PersistDifficulty()
        {
            var diff = GameForm.AIDifficulty.Normal;
            if (ratioBtnEasy.Checked) diff = GameForm.AIDifficulty.Easy;
            else if (ratioBtnHard.Checked) diff = GameForm.AIDifficulty.Hard;
            else diff = GameForm.AIDifficulty.Normal;

            AppRuntime.SaveDifficulty(diff);
        }
    }
}
