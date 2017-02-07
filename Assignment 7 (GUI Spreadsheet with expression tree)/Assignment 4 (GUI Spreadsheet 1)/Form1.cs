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
            //spreadsheet constructor is (row, col)
            sheet = new Spreadsheet.Spreadsheet(50, 26);
            sheet.cellPorpertyChanged += new PropertyChangedEventHandler(sheetPropertyChanged);

            InitializeComponent();
        }
        //update ui when data is changed
        public void sheetPropertyChanged(object sender, EventArgs e)
        {
            Spreadsheet.Spreadsheet.CellChild cell = sender as Spreadsheet.Spreadsheet.CellChild;
            if(cell != null)
            {
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColIndex].Value = cell.Text;
                sheet.myDict[cell.Name] = cell.Text;
            }
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
        private void demoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sheet.doDemo();
        }
        //this is what causes the event after a cell is edited....MAGIC HAPPENS HERE
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //getCell(row, col)!!!!!!!!!!
            sheet.updateCell(e.RowIndex, e.ColumnIndex, (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);                      
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CellText.Text = "Cell.Text: " + sheet.getCell(e.RowIndex, e.ColumnIndex).Text;
            CellValue.Text = "Cell.Value: " + sheet.getCell(e.RowIndex, e.ColumnIndex).Value;
            CellName.Text = "Cell.Name: " + sheet.getCell(e.RowIndex, e.ColumnIndex).Name;
        }
        private void DebugButton_Click(object sender, EventArgs e)
        {
            Spreadsheet.Spreadsheet.CellChild x = sheet.getCell(dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex);
            DebugButton.Text = "Got Cell";
            string iDependOn = "";
            foreach(string s in x._IdependOn)
            {
                iDependOn += s + " ";
            }
            string dependOnMe = "";
            foreach(string s in x._DependOnMe)
            {
                dependOnMe += s + " ";
            }
            String t = "";            
            t += string.Format("Name: {0}\nText: {1}\nValue: {2}\nIDependOn: {3}\nDependonMe: {4}\n", x.Name, x.Text, x.Value, iDependOn, dependOnMe);            
            MessageBox.Show(t);
            DebugButton.Text = "Inspect Cell";
        }
    }
}
