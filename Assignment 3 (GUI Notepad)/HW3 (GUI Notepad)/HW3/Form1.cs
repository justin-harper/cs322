//Justin Harper
//10696738

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace HW3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //need file name to pass to FILE
            string fileName = saveFileDialog1.FileName;

            //write to the file all of textBox1.Text
            File.WriteAllText(fileName, textBox1.Text);
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //when the "save" button is clicked, show the save dialog box

            //set defaults in sFD
            saveFileDialog1.Filter = "Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;

            //opens sFD
            saveFileDialog1.ShowDialog();
        }

        private void LoadText(TextReader tr)
        {
            //load text from TextReader

            //first prepare the textbox by clearing it
            textBox1.Text = "";
            //read text from TextReader object and set textbox text equal to it.
            textBox1.Text = tr.ReadToEnd();
        }

        private void openFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //when "Load from file" button is clicked, show the open file dialog box
            //set defualts for oFD window
            openFileDialog1.Filter = "Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            //check if result was OK
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //creating StreamReader to read the selected file
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                //call to LoadText and pass streamReader
                LoadText(sr);
            }
            //if not OK don't do anything
        }

        private void loadFibonacciNumbersfirst50ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //create new FibTR and pass 49 to the constructor
            FibonacciSeq first50 = new FibonacciSeq(50);
            //LoadText will fill the text box with the first 50 fibs
            LoadText(first50);
        }

        private void loadFibonacciNumbersfirst100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //same as above execpt we want the first 100 fibs
            //FibonacciTextReader first100 = new FibonacciTextReader(100);

            FibonacciSeq first100 = new FibonacciSeq(100);

            LoadText(first100);
        }
       
    }
}
