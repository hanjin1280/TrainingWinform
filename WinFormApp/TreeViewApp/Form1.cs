using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeViewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            //treeView1.ExpandAll(); // 트리 전체 열기
            //treeView1.CollapseAll(); // 트리 전체 닫기
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.Nodes.Add(new TreeNode(textBox1.Text, 2, 2));
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("추가할 노드의 이름을 넣고, 노드를 추가할 위치를 선택하세요");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Remove(treeView1.SelectedNode);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                treeView1.Nodes.Add(new TreeNode(textBox1.Text, 0, 1));
                textBox1.Text = "";
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("추가할 노드의 이름을 넣으세요");
            }
        }
    }
}
