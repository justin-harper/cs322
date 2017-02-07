using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_3__GUI_Notepad_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.WordWrap = !(textBox1.WordWrap);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textHandler()
        {
            
        }

        private void fibonaciToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void first50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //System.BigInteger x = new BigInteger();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            of.FilterIndex = 1;
                      
           //bool? clickedOK = 
               of.ShowDialog();
            
            

        }





    }
}
