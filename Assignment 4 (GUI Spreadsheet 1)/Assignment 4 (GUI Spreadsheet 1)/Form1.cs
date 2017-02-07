using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spreadsheet;

namespace Assignment_4__GUI_Spreadsheet_1_
{
    public partial class Form1 : Form
    {
        Spreadsheet.Spreadsheet sheet;


        public Form1()
        {
            //need a spreadsheet
            sheet = new Spreadsheet.Spreadsheet(50, 26); //note hardcoded values for now
            sheet.cellPorpertyChanged += new PropertyChangedEventHandler(sheetPropertyChanged);


            InitializeComponent();

            

        }
        //update ui when data is changed
        public void sheetPropertyChanged(object sender, EventArgs e)
        {
            Spreadsheet.Spreadsheet.CellChild cell = sender as Spreadsheet.Spreadsheet.CellChild;
            if(cell != null)
            {
                dataGridView1.Rows[cell.ColIndex].Cells[cell.RowIndex].Value = cell.Text;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            BuildWinForm();
        }
        //builds datagrid and sets some properties
        private void BuildWinForm()
        {
            dataGridView1.Columns.Clear();

            dataGridView1.ColumnHeadersVisible = true;
            
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowHeadersWidth = 50;

            dataGridView1.ColumnCount = sheet.ColumnCount;
            dataGridView1.RowCount = sheet.RowCount;

            
            string headerText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int i = 0;
            char letter = '\0';
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (i < 26)
                {
                    letter = headerText.ElementAt(i);
                }
                column.HeaderText = (letter.ToString());
                letter = '#';
                i++;
            }

            i = 1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = i.ToString();
                i++;
            }

        }
        //just a placeholder...nothing needs to happen when file is clicked on
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        //start the demo
        private void demoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sheet.doDemo();
        }
        //this is what causes the event after a cell is edited....MAGIC HAPPENS HERE
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //getCell(row, col)!!!!!!!!!!
            //gridview uses the same index so no need to adjust
            sheet.getCell(e.RowIndex, e.ColumnIndex).Text = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }

    }
}
