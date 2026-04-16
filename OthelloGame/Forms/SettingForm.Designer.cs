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
            groupBoxDifficult.SuspendLayout();
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
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.image_6a6e5188;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 450);
            Controls.Add(groupBoxDifficult);
            DoubleBuffered = true;
            Name = "SettingForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cài Đặt";
            groupBoxDifficult.ResumeLayout(false);
            groupBoxDifficult.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxDifficult;
        private RadioButton ratioBtnHard;
        private RadioButton ratioBtnNormal;
        private RadioButton ratioBtnEasy;
    }
}