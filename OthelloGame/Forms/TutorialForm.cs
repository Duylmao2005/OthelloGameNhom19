using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloGame.Forms
{
    public partial class TutorialForm : Form
    {
        public TutorialForm()
        {
            InitializeComponent();

            // Đăng ký sự kiện Resize để ảnh luôn vừa chiều rộng khi phóng to/thu nhỏ Form
            this.Load += TutorialForm_Load;
            panel1.Resize += Panel1_Resize;
        }

        private void TutorialForm_Load(object sender, EventArgs e)
        {
            AdjustImageSize();
        }

        private void Panel1_Resize(object sender, EventArgs e)
        {
            AdjustImageSize();
        }

        private void AdjustImageSize()
        {
            // Kiểm tra nếu PictureBox đã có ảnh
            if (pictureBox1.Image != null)
            {
                // 1. Đưa PictureBox về góc trên cùng bên trái
                pictureBox1.Location = new Point(0, 0);

                // 2. Ép chiều rộng PictureBox bằng chiều rộng của Panel (trừ đi phần lề cho thanh cuộn)
                // Dùng ClientSize.Width để lấy vùng làm việc thực tế của Panel
                pictureBox1.Width = panel1.ClientSize.Width;

                // 3. Tính toán chiều cao dựa trên tỉ lệ ảnh gốc để không bị méo
                // Công thức: Chiều cao mới = (Ảnh gốc Cao / Ảnh gốc Rộng) * Chiều rộng mới
                float ratio = (float)pictureBox1.Image.Height / pictureBox1.Image.Width;
                pictureBox1.Height = (int)(pictureBox1.Width * ratio);

                // Lưu ý: Khi pictureBox1.Height vượt quá panel1.Height, 
                // vì panel1 có AutoScroll = True nên thanh cuộn sẽ tự xuất hiện.
            }
        }
    }
}