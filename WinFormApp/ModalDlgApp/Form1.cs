using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModalDlgApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form2 form = new Form2();
            //form.ShowDialog();
            //form.Show();
            MessageBox.Show("텍스트 입니다", "제목",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
        }
    }
}
