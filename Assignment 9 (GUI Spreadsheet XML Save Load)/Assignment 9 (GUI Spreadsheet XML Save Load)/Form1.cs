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
using System.Threading;
using System.IO;


namespace Assignment_9
{    public partial class Form1 : Form
    {
        Spreadsheet.Spreadsheet sheet;
        string  fileName;    
        public Form1()
        {
            InitializeComponent();
        }
        //update ui when data is changed
        public void sheetPropertyChanged(object sender, EventArgs e)
        {
            Spreadsheet.Spreadsheet.CellChild cell = sender as Spreadsheet.Spreadsheet.CellChild;
            if(cell != null)
            {   
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColIndex].Value = cell.Text;
                dataGridView1.Rows[cell.RowIndex].Cells[cell.ColIndex].Style.BackColor = Color.FromArgb(cell.BGColor);
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
            int ROWS = 50;
            int COLS = 26;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //need a spreadsheet
            //spreadsheet constructor is (row, col)
            sheet = new Spreadsheet.Spreadsheet(ROWS, COLS, dataGridView1.DefaultCellStyle.BackColor.ToArgb());
           
            sheet.cellPorpertyChanged += new PropertyChangedEventHandler(sheetPropertyChanged);

            dataGridView1.Columns.Clear();
            dataGridView1.ColumnHeadersVisible = true;            
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowHeadersWidth = 50; //set height of rows
            dataGridView1.ColumnCount = COLS;
            dataGridView1.RowCount = ROWS;
            
            
            string headerText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int i = 0;
            char letter = '\0';
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
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
            DataGridViewCell UICell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (UICell.Value == null)
            {
                sheet.updateCellDataFromUI(e.RowIndex, e.ColumnIndex, "");
            }
            else
            {
                sheet.updateCellDataFromUI(e.RowIndex, e.ColumnIndex, UICell.Value.ToString());
            }

            UICell.Value = sheet.getCell(e.RowIndex, e.ColumnIndex).Text;

        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            CellText.Text    = "Cell.Text: "    + sheet.getCell(e.RowIndex, e.ColumnIndex).Text;
            CellValue.Text   = "Cell.Value: "   + sheet.getCell(e.RowIndex, e.ColumnIndex).Value;
            CellName.Text    = "Cell.Name: "    + sheet.getCell(e.RowIndex, e.ColumnIndex).Name;
            CellBGColor.Text = "Cell.BGColor: " + sheet.getCell(e.RowIndex, e.ColumnIndex).BGColor;
            
            
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
            MessageBox.Show(
                string.Format(
                        "Name: {0}\nText: {1}\nValue: {2}\nIDependOn: {3}\nDependonMe: {4}\nBackgroundColor: {5}\n",
                        x.Name, x.Text, x.Value, iDependOn, dependOnMe, x.BGColor
                        ));
            DebugButton.Text = "Inspect Cell";
        }
        
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (sheet.RU.UndoIsNotEmpty())
            {
                undoToolStripMenuItem1.Enabled = true;
                if (sheet.RU.peekUndo() == "Text")
                {
                    undoToolStripMenuItem1.Text = "Undo Text Change";
                }
                else
                {
                    undoToolStripMenuItem1.Text = "Undo Cell Color Change";
                }
            }
            else
            {
                undoToolStripMenuItem1.Enabled = false;
            }
            if (sheet.RU.RedoIsNotEmpty())
            {
                redoToolStripMenuItem1.Enabled = true;
                if (sheet.RU.peekRedo() == "Text")
                {
                    redoToolStripMenuItem1.Text = "Redo Text Change";
                }
                else
                {
                    redoToolStripMenuItem1.Text = "Redo Cell Color Change";
                }
            }
            else
            {
                redoToolStripMenuItem1.Enabled = false;
            }
        }
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sheet.undoAction();
            undoToolStripMenuItem1.Text = "Undo";
        }
        
        private void redoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sheet.redoAction();
            redoToolStripMenuItem1.Text = "Redo";
        }

        private void toolStripMenuCopy_Click(object sender, EventArgs e)
        {
            DataGridViewCell UICell = dataGridView1.CurrentCell;

            string toClipboard = sheet.Copy(UICell.RowIndex, UICell.ColumnIndex);
            if (toClipboard != null)
            {
                clipboard copy = new clipboard();
                copy.xcopy = toClipboard;

                Thread clipboardThread = new Thread(() => copy.copy());
                clipboardThread.SetApartmentState(ApartmentState.STA);
                clipboardThread.IsBackground = false;
                clipboardThread.Start();
            }
        }
        private void toolStripMenuPaste_Click(object sender, EventArgs e)
        {
            DataGridViewCell UICell = dataGridView1.CurrentCell;
            clipboard paste = new clipboard();
            Thread clipboardThread = new Thread(() => paste.paste());
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.IsBackground = false;
            clipboardThread.Start();
            clipboardThread.Join();

            if (paste.xpaste != null)
            {
                sheet.updateCellDataFromUI(UICell.RowIndex, UICell.ColumnIndex, paste.xpaste);
            }         
        }
        private void changeBackgroundColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog CD = new ColorDialog();
            if (CD.ShowDialog() == DialogResult.OK)
            {  
                foreach (DataGridViewCell UICell in dataGridView1.SelectedCells)
                {
                    sheet.updateCellColorFromUI(UICell.RowIndex, UICell.ColumnIndex, CD.Color.ToArgb());
                }
            }     
        }
       
        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                sheet.updateCellDataFromUI(dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex, "");               
            }
            if(e.KeyData == (Keys.Control | Keys.Z))
            {
                sheet.undoAction();
            }
            if(e.KeyData == (Keys.Control | Keys.Y))
            {
                sheet.redoAction();
            }
            if(e.KeyData == (Keys.Control | Keys.C))
            {
                toolStripMenuCopy_Click(this, null);
            }
            if(e.KeyData == (Keys.Control | Keys.V))
            {
                toolStripMenuPaste_Click(this, null);
            }
            if(e.KeyData == (Keys.Control | Keys.O))
            {
                openToolStripMenuItem_Click(this, e);
            }
            if(e.KeyData == (Keys.Control | Keys.S))
            {
                saveToolStripMenuItem_Click(this, e);
            }
            if(e.KeyData == (Keys.Control | Keys.W))
            {
                closeToolStripMenuItem_Click(this, e);
            }
            if(e.KeyData == (Keys.Alt | Keys.F4))
            {
                exitToolStripMenuItem_Click(this, e);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFD = new OpenFileDialog();
            openFD.FileName = "*.xml";
            openFD.Filter = "XML File (*xml) | *.xml";
            openFD.RestoreDirectory = true;

            if(openFD.ShowDialog() == DialogResult.OK)
            {
                Stream file = openFD.OpenFile();
                fileName = openFD.FileName;
                sheet.Load(file);
                file.Dispose();
                file.Close();                
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFD = new SaveFileDialog();
            saveFD.FileName = "*.xml";
            saveFD.Filter = "XML File (*.xml) | *.xml";

            saveFD.RestoreDirectory = true;

            if(saveFD.ShowDialog() == DialogResult.OK)
            {
                Stream file = saveFD.OpenFile();
                fileName = saveFD.FileName;                
                sheet.Save(file);
                file.Dispose();
                file.Close();                
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (
                 MessageBox.Show("Are you sure you want to close the current spreadsheet?\nAny unsaved work will be lost",
                 "Data Loss Warning",
                 MessageBoxButtons.YesNo)
                 ==
                 DialogResult.Yes
                )
            {
                sheet.ClearFromUI();
                fileName = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileName == null)
            {
                saveAsToolStripMenuItem_Click(this, e);
            }
            else
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);                
                sheet.Save(file);
                file.Dispose();
                file.Close();
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            dataGridView1.CurrentCell.Value = sheet.getCell(e.RowIndex, e.ColumnIndex).Value;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fileName != null)
            {
                saveToolStripMenuItem_Click(this, e);
                Application.Exit();
            }
            else
            {
                if(
                    MessageBox.Show("Are you sure you want to quit?\nAny unsaved work will be lost",
                    "Data Loss Warning",
                    MessageBoxButtons.YesNo)
                    ==
                    DialogResult.Yes
                  )
                {
                    Application.Exit();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch(e.CloseReason)
            {
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    exitToolStripMenuItem_Click(this, e);
                    break;
            }
        } 
    }
}
