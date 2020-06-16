using System;
using System.Windows.Forms;

namespace OpenFileDlgApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "텍스트파일(*.txt)|*.txt";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();

            foreach(var item in openFileDialog1.FileNames)
            {
                textBox1.Text = textBox1.Text + item;
                textBox1.Text = textBox1.Text + Environment.NewLine;
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                BackColor = colorDialog1.Color;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                button3.BackColor = colorDialog1.Color;
            }
        }
    }
}
