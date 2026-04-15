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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Showcard Gothic", 49.8000031F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(194, 9);
            label1.Name = "label1";
            label1.Size = new Size(422, 103);
            label1.TabIndex = 0;
            label1.Text = "OTHELLO";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaptionText;
            button1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button1.ForeColor = SystemColors.ButtonHighlight;
            button1.Location = new Point(313, 111);
            button1.Name = "button1";
            button1.Size = new Size(189, 56);
            button1.TabIndex = 1;
            button1.Text = "Tiếp tục";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaptionText;
            button2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(313, 173);
            button2.Name = "button2";
            button2.Size = new Size(189, 56);
            button2.TabIndex = 2;
            button2.Text = "Chơi đơn";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaptionText;
            button3.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(313, 235);
            button3.Name = "button3";
            button3.Size = new Size(189, 56);
            button3.TabIndex = 3;
            button3.Text = "PvP";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaptionText;
            button4.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button4.ForeColor = SystemColors.ButtonHighlight;
            button4.Location = new Point(313, 297);
            button4.Name = "button4";
            button4.Size = new Size(189, 56);
            button4.TabIndex = 4;
            button4.Text = "AI play";
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaptionText;
            button5.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button5.ForeColor = SystemColors.ButtonHighlight;
            button5.Location = new Point(313, 359);
            button5.Name = "button5";
            button5.Size = new Size(189, 56);
            button5.TabIndex = 5;
            button5.Text = "Hướng dẫn";
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaptionText;
            button6.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 163);
            button6.ForeColor = SystemColors.ButtonHighlight;
            button6.Location = new Point(713, 396);
            button6.Name = "button6";
            button6.Size = new Size(85, 42);
            button6.TabIndex = 6;
            button6.Text = "Cài đặt";
            button6.UseVisualStyleBackColor = false;
            // 
            // MainMenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.image_6a6e5188;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            DoubleBuffered = true;
            Name = "MainMenuForm";
            Text = "MainMenuForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
    }
}