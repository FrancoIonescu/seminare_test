using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Seminar5TEST
{
    public partial class Form2 : Form
    {

        List<Pompier> pompieri = new List<Pompier> ();
        public Form2()
        {
            InitializeComponent();
 
            Pompier p1 = new Pompier("Franco Ionescu", 29, 4, new DateTime(2024, 07, 01));
            Pompier p2 = new Pompier("Andrei Ionescu", 34, 3.5f, new DateTime(2024, 06, 30));
            Pompier p3 = new Pompier("Daniel Ionescu", 23, 4.2f, new DateTime(2023, 05, 03));
            Pompier p4 = new Pompier("Mircea Ionescu", 49, 3.6f, new DateTime(2020, 04, 27));
            pompieri.Add(p1);
            pompieri.Add(p2);
            pompieri.Add(p3);
            pompieri.Add(p4);

            TreeNode functie1 = new TreeNode("Pompier Sef");
            TreeNode functie2 = new TreeNode("Pompier Asistent");
            treeView1.Nodes.Add(functie1);
            treeView1.Nodes.Add(functie2);
            TreeNode t1 = new TreeNode(p1.Nume + " " + p1.Varsta + " " + p1.DataAngajare);
            TreeNode t2 = new TreeNode(p2.Nume + " " + p2.Varsta + " " + p2.DataAngajare);
            TreeNode t3 = new TreeNode(p3.Nume + " " + p3.Varsta + " " + p3.DataAngajare);
            TreeNode t4 = new TreeNode(p4.Nume + " " + p4.Varsta + " " + p4.DataAngajare);
            t1.Tag = p1;
            t2.Tag = p2;
            t3.Tag = p3;
            t4.Tag = p4;
            treeView1.Nodes[0].Nodes.Add(t1);
            treeView1.Nodes[0].Nodes.Add(t2);
            treeView1.Nodes[1].Nodes.Add(t3);
            treeView1.Nodes[1].Nodes.Add(t4);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = treeView1.SelectedNode.Text;
;        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag != null)
            {
                treeView1.SelectedNode.Remove();
            }
        }
    }
}
