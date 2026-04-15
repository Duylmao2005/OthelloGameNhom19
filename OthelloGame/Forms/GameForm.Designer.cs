namespace OthelloGame.Forms
{
    partial class GameForm
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
            tlpRoot = new TableLayoutPanel();
            pnlTop = new Panel();
            pnlBottom = new Panel();
            lblPlayer1 = new Label();
            lblScore1 = new Label();
            lblPlayer2 = new Label();
            lblScore2 = new Label();
            lblTurn = new Label();
            flpLeft = new FlowLayoutPanel();
            flpRight = new FlowLayoutPanel();
            btnNewGame = new Button();
            btnSetting = new Button();
            btnSurrender = new Button();
            btnHint = new Button();
            btnUndo = new Button();
            btnQuit = new Button();
            pnlBoard = new Panel();
            tlpRoot.SuspendLayout();
            pnlTop.SuspendLayout();
            pnlBottom.SuspendLayout();
            flpLeft.SuspendLayout();
            flpRight.SuspendLayout();
            SuspendLayout();
            // 
            // tlpRoot
            // 
            tlpRoot.ColumnCount = 3;
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220F));
            tlpRoot.Controls.Add(pnlTop, 1, 0);
            tlpRoot.Controls.Add(pnlBottom, 1, 2);
            tlpRoot.Controls.Add(flpLeft, 0, 1);
            tlpRoot.Controls.Add(flpRight, 2, 1);
            tlpRoot.Controls.Add(pnlBoard, 1, 1);
            tlpRoot.Dock = DockStyle.Fill;
            tlpRoot.Location = new Point(0, 0);
            tlpRoot.Margin = new Padding(0);
            tlpRoot.Name = "tlpRoot";
            tlpRoot.RowCount = 3;
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 92F));
            tlpRoot.Size = new Size(1000, 720);
            tlpRoot.TabIndex = 0;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.Transparent;
            pnlTop.Controls.Add(lblTurn);
            pnlTop.Controls.Add(lblScore1);
            pnlTop.Controls.Add(lblPlayer1);
            pnlTop.Dock = DockStyle.Fill;
            pnlTop.Location = new Point(220, 0);
            pnlTop.Margin = new Padding(0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(18, 12, 18, 12);
            pnlTop.Size = new Size(560, 92);
            pnlTop.TabIndex = 0;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.Transparent;
            pnlBottom.Controls.Add(lblScore2);
            pnlBottom.Controls.Add(lblPlayer2);
            pnlBottom.Dock = DockStyle.Fill;
            pnlBottom.Location = new Point(220, 628);
            pnlBottom.Margin = new Padding(0);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(18, 12, 18, 12);
            pnlBottom.Size = new Size(560, 92);
            pnlBottom.TabIndex = 1;
            // 
            // lblPlayer1
            // 
            lblPlayer1.AutoSize = true;
            lblPlayer1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPlayer1.ForeColor = Color.Black;
            lblPlayer1.Location = new Point(18, 14);
            lblPlayer1.Name = "lblPlayer1";
            lblPlayer1.Size = new Size(102, 32);
            lblPlayer1.TabIndex = 0;
            lblPlayer1.Text = "Player 1";
            // 
            // lblScore1
            // 
            lblScore1.AutoSize = true;
            lblScore1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblScore1.ForeColor = Color.Black;
            lblScore1.Location = new Point(180, 10);
            lblScore1.Name = "lblScore1";
            lblScore1.Size = new Size(132, 41);
            lblScore1.TabIndex = 1;
            lblScore1.Text = "Score: 2";
            // 
            // lblTurn
            // 
            lblTurn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTurn.AutoSize = true;
            lblTurn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTurn.ForeColor = Color.FromArgb(30, 30, 30);
            lblTurn.Location = new Point(420, 18);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(121, 28);
            lblTurn.TabIndex = 2;
            lblTurn.Text = "Lượt: Đen";
            // 
            // lblPlayer2
            // 
            lblPlayer2.AutoSize = true;
            lblPlayer2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblPlayer2.ForeColor = Color.Black;
            lblPlayer2.Location = new Point(18, 14);
            lblPlayer2.Name = "lblPlayer2";
            lblPlayer2.Size = new Size(102, 32);
            lblPlayer2.TabIndex = 0;
            lblPlayer2.Text = "Player 2";
            // 
            // lblScore2
            // 
            lblScore2.AutoSize = true;
            lblScore2.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblScore2.ForeColor = Color.Black;
            lblScore2.Location = new Point(180, 10);
            lblScore2.Name = "lblScore2";
            lblScore2.Size = new Size(132, 41);
            lblScore2.TabIndex = 1;
            lblScore2.Text = "Score: 2";
            // 
            // flpLeft
            // 
            flpLeft.BackColor = Color.Transparent;
            flpLeft.Controls.Add(btnNewGame);
            flpLeft.Controls.Add(btnSetting);
            flpLeft.Controls.Add(btnSurrender);
            flpLeft.Dock = DockStyle.Fill;
            flpLeft.FlowDirection = FlowDirection.TopDown;
            flpLeft.Location = new Point(0, 92);
            flpLeft.Margin = new Padding(0);
            flpLeft.Name = "flpLeft";
            flpLeft.Padding = new Padding(24, 32, 24, 32);
            flpLeft.Size = new Size(220, 536);
            flpLeft.TabIndex = 2;
            flpLeft.WrapContents = false;
            // 
            // flpRight
            // 
            flpRight.BackColor = Color.Transparent;
            flpRight.Controls.Add(btnHint);
            flpRight.Controls.Add(btnUndo);
            flpRight.Controls.Add(btnQuit);
            flpRight.Dock = DockStyle.Fill;
            flpRight.FlowDirection = FlowDirection.TopDown;
            flpRight.Location = new Point(780, 92);
            flpRight.Margin = new Padding(0);
            flpRight.Name = "flpRight";
            flpRight.Padding = new Padding(24, 32, 24, 32);
            flpRight.Size = new Size(220, 536);
            flpRight.TabIndex = 3;
            flpRight.WrapContents = false;
            // 
            // btnNewGame
            // 
            btnNewGame.FlatStyle = FlatStyle.Flat;
            btnNewGame.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnNewGame.Location = new Point(24, 32);
            btnNewGame.Margin = new Padding(0, 0, 0, 24);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(172, 68);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "New game";
            btnNewGame.UseVisualStyleBackColor = true;
            // 
            // btnSetting
            // 
            btnSetting.FlatStyle = FlatStyle.Flat;
            btnSetting.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSetting.Location = new Point(24, 124);
            btnSetting.Margin = new Padding(0, 0, 0, 24);
            btnSetting.Name = "btnSetting";
            btnSetting.Size = new Size(172, 68);
            btnSetting.TabIndex = 1;
            btnSetting.Text = "Setting";
            btnSetting.UseVisualStyleBackColor = true;
            // 
            // btnSurrender
            // 
            btnSurrender.FlatStyle = FlatStyle.Flat;
            btnSurrender.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnSurrender.Location = new Point(24, 216);
            btnSurrender.Margin = new Padding(0, 0, 0, 24);
            btnSurrender.Name = "btnSurrender";
            btnSurrender.Size = new Size(172, 68);
            btnSurrender.TabIndex = 2;
            btnSurrender.Text = "Surrender";
            btnSurrender.UseVisualStyleBackColor = true;
            // 
            // btnHint
            // 
            btnHint.FlatStyle = FlatStyle.Flat;
            btnHint.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnHint.Location = new Point(24, 32);
            btnHint.Margin = new Padding(0, 0, 0, 24);
            btnHint.Name = "btnHint";
            btnHint.Size = new Size(172, 68);
            btnHint.TabIndex = 0;
            btnHint.Text = "Hint";
            btnHint.UseVisualStyleBackColor = true;
            // 
            // btnUndo
            // 
            btnUndo.FlatStyle = FlatStyle.Flat;
            btnUndo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnUndo.Location = new Point(24, 124);
            btnUndo.Margin = new Padding(0, 0, 0, 24);
            btnUndo.Name = "btnUndo";
            btnUndo.Size = new Size(172, 68);
            btnUndo.TabIndex = 1;
            btnUndo.Text = "Undo";
            btnUndo.UseVisualStyleBackColor = true;
            // 
            // btnQuit
            // 
            btnQuit.FlatStyle = FlatStyle.Flat;
            btnQuit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnQuit.Location = new Point(24, 216);
            btnQuit.Margin = new Padding(0, 0, 0, 24);
            btnQuit.Name = "btnQuit";
            btnQuit.Size = new Size(172, 68);
            btnQuit.TabIndex = 2;
            btnQuit.Text = "Quit";
            btnQuit.UseVisualStyleBackColor = true;
            // 
            // pnlBoard
            // 
            pnlBoard.BackColor = Color.Transparent;
            pnlBoard.Dock = DockStyle.Fill;
            pnlBoard.Location = new Point(220, 92);
            pnlBoard.Margin = new Padding(0);
            pnlBoard.Name = "pnlBoard";
            pnlBoard.Padding = new Padding(10);
            pnlBoard.Size = new Size(560, 536);
            pnlBoard.TabIndex = 4;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 720);
            Controls.Add(tlpRoot);
            DoubleBuffered = true;
            MinimumSize = new Size(920, 650);
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Othello - Game";
            tlpRoot.ResumeLayout(false);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            flpLeft.ResumeLayout(false);
            flpRight.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpRoot;
        private Panel pnlTop;
        private Panel pnlBottom;
        private Label lblPlayer1;
        private Label lblScore1;
        private Label lblTurn;
        private Label lblPlayer2;
        private Label lblScore2;
        private FlowLayoutPanel flpLeft;
        private FlowLayoutPanel flpRight;
        private Button btnNewGame;
        private Button btnSetting;
        private Button btnSurrender;
        private Button btnHint;
        private Button btnUndo;
        private Button btnQuit;
        private Panel pnlBoard;
    }
}