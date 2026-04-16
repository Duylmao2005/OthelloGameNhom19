namespace OthelloGame.Forms
{
    partial class SettingForm
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
            groupBoxDifficult = new GroupBox();
            ratioBtnHard = new RadioButton();
            ratioBtnNormal = new RadioButton();
            ratioBtnEasy = new RadioButton();
            groupBox3 = new GroupBox();
            radioButton7 = new RadioButton();
            radioButton8 = new RadioButton();
            radioButton9 = new RadioButton();
            radioButton6 = new RadioButton();
            radioButton5 = new RadioButton();
            groupBox2 = new GroupBox();
            groupBoxDifficult.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxDifficult
            // 
            groupBoxDifficult.Controls.Add(ratioBtnHard);
            groupBoxDifficult.Controls.Add(ratioBtnNormal);
            groupBoxDifficult.Controls.Add(ratioBtnEasy);
            groupBoxDifficult.Location = new Point(109, 49);
            groupBoxDifficult.Name = "groupBoxDifficult";
            groupBoxDifficult.Size = new Size(572, 78);
            groupBoxDifficult.TabIndex = 0;
            groupBoxDifficult.TabStop = false;
            groupBoxDifficult.Text = "ĐỘ KHÓ:";
            // 
            // ratioBtnHard
            // 
            ratioBtnHard.AutoSize = true;
            ratioBtnHard.Location = new Point(434, 38);
            ratioBtnHard.Name = "ratioBtnHard";
            ratioBtnHard.Size = new Size(56, 24);
            ratioBtnHard.TabIndex = 2;
            ratioBtnHard.TabStop = true;
            ratioBtnHard.Text = "Khó";
            ratioBtnHard.UseVisualStyleBackColor = true;
            // 
            // ratioBtnNormal
            // 
            ratioBtnNormal.AutoSize = true;
            ratioBtnNormal.Location = new Point(229, 38);
            ratioBtnNormal.Name = "ratioBtnNormal";
            ratioBtnNormal.Size = new Size(81, 24);
            ratioBtnNormal.TabIndex = 1;
            ratioBtnNormal.TabStop = true;
            ratioBtnNormal.Text = "Thường";
            ratioBtnNormal.UseVisualStyleBackColor = true;
            // 
            // ratioBtnEasy
            // 
            ratioBtnEasy.AutoSize = true;
            ratioBtnEasy.Location = new Point(50, 38);
            ratioBtnEasy.Name = "ratioBtnEasy";
            ratioBtnEasy.Size = new Size(49, 24);
            ratioBtnEasy.TabIndex = 0;
            ratioBtnEasy.TabStop = true;
            ratioBtnEasy.Text = "Dễ";
            ratioBtnEasy.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(radioButton7);
            groupBox3.Controls.Add(radioButton8);
            groupBox3.Controls.Add(radioButton9);
            groupBox3.Location = new Point(109, 289);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(572, 78);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "MÀU CỜ:";
            // 
            // radioButton7
            // 
            radioButton7.AutoSize = true;
            radioButton7.Location = new Point(434, 38);
            radioButton7.Name = "radioButton7";
            radioButton7.Size = new Size(99, 24);
            radioButton7.TabIndex = 2;
            radioButton7.TabStop = true;
            radioButton7.Text = "Cam/Xanh";
            radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            radioButton8.AutoSize = true;
            radioButton8.Location = new Point(229, 38);
            radioButton8.Name = "radioButton8";
            radioButton8.Size = new Size(89, 24);
            radioButton8.TabIndex = 1;
            radioButton8.TabStop = true;
            radioButton8.Text = "Đỏ/Vàng";
            radioButton8.UseVisualStyleBackColor = true;
            // 
            // radioButton9
            // 
            radioButton9.AutoSize = true;
            radioButton9.Location = new Point(50, 38);
            radioButton9.Name = "radioButton9";
            radioButton9.Size = new Size(100, 24);
            radioButton9.TabIndex = 0;
            radioButton9.TabStop = true;
            radioButton9.Text = "Đen/Trắng";
            radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            radioButton6.AutoSize = true;
            radioButton6.Location = new Point(145, 38);
            radioButton6.Name = "radioButton6";
            radioButton6.Size = new Size(52, 24);
            radioButton6.TabIndex = 0;
            radioButton6.TabStop = true;
            radioButton6.Text = "Bật";
            radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            radioButton5.AutoSize = true;
            radioButton5.Location = new Point(368, 38);
            radioButton5.Name = "radioButton5";
            radioButton5.Size = new Size(51, 24);
            radioButton5.TabIndex = 1;
            radioButton5.TabStop = true;
            radioButton5.Text = "Tắt";
            radioButton5.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton5);
            groupBox2.Controls.Add(radioButton6);
            groupBox2.Location = new Point(109, 167);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(572, 78);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "ÂM THANH:";
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.image_6a6e5188;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBoxDifficult);
            DoubleBuffered = true;
            Name = "SettingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cài Đặt";
            groupBoxDifficult.ResumeLayout(false);
            groupBoxDifficult.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxDifficult;
        private RadioButton ratioBtnHard;
        private RadioButton ratioBtnNormal;
        private RadioButton ratioBtnEasy;
        private GroupBox groupBox3;
        private RadioButton radioButton7;
        private RadioButton radioButton8;
        private RadioButton radioButton9;
        private RadioButton radioButton6;
        private RadioButton radioButton5;
        private GroupBox groupBox2;
    }
}