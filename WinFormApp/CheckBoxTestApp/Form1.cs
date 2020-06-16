using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBoxTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateLabel()
        {
            string strchk1 = "", strchk2 = "", strchk3 = "", strchk4 = "";
            if(checkBox1.Checked == true)
            {
                strchk1 = checkBox1.Text;
            }
            else
            {
                strchk1 = "";
            }

            if (checkBox2.Checked == true)
            {
                strchk2 = checkBox2.Text;
            }
            else
            {
                strchk2 = "";
            }

            if (checkBox3.Checked == true)
            {
                strchk3 = checkBox3.Text;
            }
            else
            {
                strchk3 = "";
            }

            if (checkBox4.Checked == true)
            {
                strchk4 = checkBox4.Text;
            }
            else
            {
                strchk4 = "";
            }

            /*string str = "";
            if(!strchk1.Equals(""))
            {
                str = str + strchk1 + ", ";
            }
            if (!strchk2.Equals(""))
            {
                str = str + strchk2 + ", ";
            }
            if (!strchk3.Equals(""))
            {
                str = str + strchk3 + ", ";
            }
            if (!strchk4.Equals(""))
            {
                str = str + strchk4;
            }

            label1.Text = str;*/

            label1.Text = (strchk1 + " " + strchk2 + " " + strchk3 + " " + strchk4).Replace("  ", " ");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //label1.Text = checkBox1.Text;
            UpdateLabel();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //label1.Text = checkBox3.Text;
            UpdateLabel();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //label1.Text = checkBox2.Text;
            UpdateLabel();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //label1.Text = checkBox4.Text;
            UpdateLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "좋아하는 과일 : ";
        }
    }
}
