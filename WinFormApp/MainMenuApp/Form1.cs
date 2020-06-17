using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenuApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void MnuNewFile_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + MnuNewFile.Text + Environment.NewLine;
            toolStripStatusLabel1.Text = MnuNewFile.Text;
            //실제 새 파일 로직을 입력해야함
        }

        private void 열기OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 열기OToolStripMenuItem.Text + Environment.NewLine;
            //실제 열기 로직을 입력해야함
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 저장SToolStripMenuItem.Text + Environment.NewLine;
            MessageBox.Show("저장했습니다.");
            //실제 저장 로직을 입력해야함
        }

        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 프로그램정보AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 프로그램정보AToolStripMenuItem.Text + Environment.NewLine;
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void TextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(e.Location);
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            LblMouseLocation.Text = $"X좌표 : {e.X.ToString()}, Y좌표 : {e.Y.ToString()}";
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            button1.Focus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MnuNewFile_Click(sender, e);
        }
    }
}
