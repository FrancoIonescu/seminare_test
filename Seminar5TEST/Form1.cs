using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Seminar5TEST
{ 
    public partial class Form1 : Form
    {
        List<Pompier> pompieri = new List<Pompier>();
        public Form1()
        {
            InitializeComponent();
            Pompier p1 = new Pompier("Franco Ionescu",29,4,new DateTime(2024,07,01));
            Pompier p2 = new Pompier("Andrei Ionescu", 34, 3.5f, new DateTime(2024,06,30));
            Pompier p3 = new Pompier("Daniel Ionescu", 23, 4.2f, new DateTime(2023, 05, 03));
            Pompier p4 = new Pompier("Mircea Ionescu", 49, 3.6f, new DateTime(2020, 04, 27));

            pompieri.Add(p1);
            pompieri.Add(p2);
            pompieri.Add(p3);
            pompieri.Add(p4);

            pompieri.Sort();
            foreach(Pompier p in pompieri)
            {
                MessageBox.Show(p.ToString());
            }
            ListViewItem lv1 = new ListViewItem(p1.Nume);
            lv1.SubItems.Add(p1.Varsta.ToString());
            lv1.SubItems.Add(p1.Gpa.ToString());
            lv1.SubItems.Add(p1.DataAngajare.ToString());
            lv1.Tag = p1;
            lvPompieri.Items.Add(lv1);

            ListViewItem lv2 = new ListViewItem(p2.Nume);
            lv2.SubItems.Add(p2.Varsta.ToString());
            lv2.SubItems.Add(p2.Gpa.ToString());
            lv2.SubItems.Add(p2.DataAngajare.ToString());
            lv2.Tag = p2;
            lvPompieri.Items.Add(lv2);

            lbPompieri.Items.Add(p1.Nume);
            lbPompieri.Items.Add(p2.Nume);
            lbPompieri.Items.Add(p1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lvPompieri.SelectedItems.Count > 0)
            {
                ListViewItem lv = lvPompieri.SelectedItems[0];
                Pompier p = lvPompieri.SelectedItems[0].Tag as Pompier;
                lv.SubItems[0].Text = tbNume.Text;
                p.Nume = tbNume.Text;
                lv.SubItems[1].Text = tbVarsta.Text;
                p.Varsta = Int32.Parse(tbVarsta.Text);
                lv.SubItems[2].Text = tbGPA.Text;
                p.Gpa = float.Parse(tbGPA.Text);
                lv.SubItems[3].Text = tbData.Text;
                p.DataAngajare = tbData.Value;
            }
            else
            {
                Pompier p = new Pompier();
                p.Nume = tbNume.Text;
                p.Varsta = Int32.Parse(tbVarsta.Text);
                p.Gpa = float.Parse(tbGPA.Text);
                p.DataAngajare = tbData.Value;
                ListViewItem lv = new ListViewItem(p.Nume);
                lv.SubItems.Add(p.Varsta.ToString());
                lv.SubItems.Add(p.Gpa.ToString());
                lv.SubItems.Add(p.DataAngajare.ToString());
                lv.Tag = p;
                lvPompieri.Items.Add(lv);
            }

            tbNume.Clear();
            tbVarsta.Clear() ;
            tbGPA.Clear();
        }

        private void lvPompieri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPompieri.SelectedItems.Count > 0)
            {
                button1.Text = "Modifica";
                Pompier p = lvPompieri.SelectedItems[0].Tag as Pompier;
                tbNume.Text = p.Nume;
                tbVarsta.Text = p.Varsta.ToString();
                tbGPA.Text = p.Gpa.ToString();
                tbData.Value = p.DataAngajare;

                ListViewItem lv = lvPompieri.SelectedItems[0];
                tbPompieri.Text = lv.SubItems[0].Text + " " + lv.SubItems[1].Text + " " + lv.SubItems[2].Text + " " + lv.SubItems[3].Text;
            }
        }

        private void salvareBinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "fisier (*.ff)|*.ff";
            fd.CheckPathExists = true;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                List<Pompier> pompieri = new List<Pompier>();
                foreach (ListViewItem lvi in lvPompieri.Items)
                {
                    pompieri.Add((Pompier)lvi.Tag);
                }

                try
                {
                    BinaryFormatter serializer = new BinaryFormatter();
                    Stream file = File.Create(fd.FileName);
                    serializer.Serialize(file, pompieri);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void restaurareBinarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "fisier pompier (*.ff)|*.ff";
            fd.CheckFileExists = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                BinaryFormatter serializer = new BinaryFormatter();
                Stream file = File.OpenRead(fd.FileName);
                List<Pompier> pompieri = (List<Pompier>)serializer.Deserialize(file);

                foreach (Pompier p in pompieri)
                {
                    ListViewItem lvi = new ListViewItem(p.Nume);
                    lvi.SubItems.Add(p.Varsta.ToString());
                    lvi.SubItems.Add(p.Gpa.ToString());
                    lvi.SubItems.Add(p.DataAngajare.ToString());
                    lvi.Tag = p;
                    lvPompieri.Items.Add(lvi);
                }

                file.Close();
            }
        }

        private void stergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvPompieri.SelectedItems.Count > 0)
            {
                lvPompieri.SelectedItems[0].Remove();
            }
        }

        private void salvareXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "fisier xml (*.xml)|*.xml";
            fd.CheckPathExists = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                List<Pompier> pompieri = new List<Pompier>();
                foreach (ListViewItem lvi in lvPompieri.Items)
                {
                    pompieri.Add((Pompier)lvi.Tag);
                }

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pompier>));
                    Stream file = File.Create(fd.FileName);
                    serializer.Serialize(file, pompieri);
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void restaurareXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "fisier xml (*.xml)|*.xml";
            fd.CheckFileExists = true;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pompier>));
                    Stream file = File.OpenRead(fd.FileName);
                    List<Pompier> pompieri = (List<Pompier>)serializer.Deserialize(file);

                    foreach(Pompier p in pompieri)
                    {
                        ListViewItem lvi = new ListViewItem(p.Nume);
                        lvi.SubItems.Add(p.Varsta.ToString());
                        lvi.SubItems.Add(p.Gpa.ToString());
                        lvi.SubItems.Add(p.DataAngajare.ToString());
                        lvi.Tag = p;
                        lvPompieri.Items.Add(lvi);
                    }
                    file.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pd_print(object sender, PrintPageEventArgs e)
        {
            Graphics gr = e.Graphics;
            Font font = new Font(Font.FontFamily, 15);
            Brush brush = new SolidBrush(Color.Black);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            gr.DrawString("NUME                    VARSTA          GPA                   DATA ANGAJARE", font, brush, x, y);

            for (int i = 0; i < pompieri.Count; i++)
            {
                gr.DrawString(pompieri[i].Nume, font, brush, x, y + 30 + 30 * i);
                gr.DrawString(pompieri[i].Varsta.ToString(), font, brush, x + 200, y + 30 + 30 * i);
                gr.DrawString(pompieri[i].Gpa.ToString(), font, brush, x + 330, y + 30 + 30 * i);
                gr.DrawString(pompieri[i].DataAngajare.ToString(), font, brush, x + 450, y + 30 + 30 * i);
            }
           
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_print);
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = pd;
            dlg.ShowDialog();
        }

        private void treeViewToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {
            Form2 formTreeView = new Form2();
            formTreeView.Show();
        }

        private void lvPompieri_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvPompieri.SelectedItems.Count > 0)
            {
                lvPompieri.DoDragDrop((Pompier)lvPompieri.SelectedItems[0].Tag,
                    DragDropEffects.Copy);
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(new Pompier().GetType().ToString()))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy && e.Data.GetDataPresent(new Pompier().GetType().ToString())) {
                Pompier p = (Pompier)e.Data.GetData(new Pompier().GetType().ToString());
                textBox1.Text = "" + p.ToString();
            }
        }
    }
}
