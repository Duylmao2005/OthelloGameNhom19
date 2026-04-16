namespace OthelloGame.Forms
{
    partial class MainMenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btnContinue = new Button();
            btnPlaySingle = new Button();
            btnPvP = new Button();
            btnAIPlay = new Button();
            btnHuongDan = new Button();
            btnSettings = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Microsoft Sans Serif", 49.8000031F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(194, 9);
            label1.Name = "label1";
            label1.Size = new Size(439, 95);
            label1.TabIndex = 0;
            label1.Text = "OTHELLO";
            // 
            // btnContinue
            // 
            btnContinue.BackColor = SystemColors.ActiveCaptionText;
            btnContinue.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnContinue.ForeColor = SystemColors.ButtonHighlight;
            btnContinue.Location = new Point(313, 111);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(189, 56);
            btnContinue.TabIndex = 1;
            btnContinue.Text = "Tiếp tục";
            btnContinue.UseVisualStyleBackColor = false;
            // 
            // btnPlaySingle
            // 
            btnPlaySingle.BackColor = SystemColors.ActiveCaptionText;
            btnPlaySingle.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnPlaySingle.ForeColor = SystemColors.ButtonHighlight;
            btnPlaySingle.Location = new Point(313, 173);
            btnPlaySingle.Name = "btnPlaySingle";
            btnPlaySingle.Size = new Size(189, 56);
            btnPlaySingle.TabIndex = 2;
            btnPlaySingle.Text = "Chơi đơn";
            btnPlaySingle.UseVisualStyleBackColor = false;
            // 
            // btnPvP
            // 
            btnPvP.BackColor = SystemColors.ActiveCaptionText;
            btnPvP.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnPvP.ForeColor = SystemColors.ButtonHighlight;
            btnPvP.Location = new Point(313, 235);
            btnPvP.Name = "btnPvP";
            btnPvP.Size = new Size(189, 56);
            btnPvP.TabIndex = 3;
            btnPvP.Text = "PvP";
            btnPvP.UseVisualStyleBackColor = false;
            btnPvP.Click += button3_Click;
            // 
            // btnAIPlay
            // 
            btnAIPlay.BackColor = SystemColors.ActiveCaptionText;
            btnAIPlay.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnAIPlay.ForeColor = SystemColors.ButtonHighlight;
            btnAIPlay.Location = new Point(313, 297);
            btnAIPlay.Name = "btnAIPlay";
            btnAIPlay.Size = new Size(189, 56);
            btnAIPlay.TabIndex = 4;
            btnAIPlay.Text = "AI play";
            btnAIPlay.UseVisualStyleBackColor = false;
            // 
            // btnHuongDan
            // 
            btnHuongDan.BackColor = SystemColors.ActiveCaptionText;
            btnHuongDan.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnHuongDan.ForeColor = SystemColors.ButtonHighlight;
            btnHuongDan.Location = new Point(313, 359);
            btnHuongDan.Name = "btnHuongDan";
            btnHuongDan.Size = new Size(189, 56);
            btnHuongDan.TabIndex = 5;
            btnHuongDan.Text = "Hướng dẫn";
            btnHuongDan.UseVisualStyleBackColor = false;
            btnHuongDan.Click += btnHuongDan_Click;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = SystemColors.ActiveCaptionText;
            btnSettings.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSettings.ForeColor = SystemColors.ButtonHighlight;
            btnSettings.Location = new Point(713, 396);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(85, 42);
            btnSettings.TabIndex = 6;
            btnSettings.Text = "Cài đặt";
            btnSettings.UseVisualStyleBackColor = false;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.image_6a6e5188;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSettings);
            Controls.Add(btnHuongDan);
            Controls.Add(btnAIPlay);
            Controls.Add(btnPvP);
            Controls.Add(btnPlaySingle);
            Controls.Add(btnContinue);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "MainMenuForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MainMenuForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnContinue;
        private Button btnPlaySingle;
        private Button btnPvP;
        private Button btnAIPlay;
        private Button btnHuongDan;
        private Button btnSettings;
    }
}