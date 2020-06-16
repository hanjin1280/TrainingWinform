using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListControlTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && listBox1.Items.Contains(textBox1.Text) == false)
            {
                listBox1.Items.Add(textBox1.Text);
                comboBox1.Items.Add(textBox1.Text);
                textBox1.Text = "";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                //listBox1.Items.RemoveAt(listBox1.Items.IndexOf(textBox1.Text));
                //textBox1.Text = "";
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox1.SelectedItem.ToString();
        }
    }
}
