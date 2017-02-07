using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Spreadsheet
{
   public class Spreadsheet 
    {

       public class CellChild : Cell
        {
           //create inherited Cell class to create cells
            public CellChild(int row, int col) : base(row, col)
            {

            }
        }

       //need an event to trigger magic
       public event PropertyChangedEventHandler cellPorpertyChanged;

       //col,row
       CellChild [,] cells;
       int _rowCount;
       int _colCount;

       //RowCount property
       public int RowCount
       {
           get
           {
               return _rowCount;
           }
       }
       //column count property
       public int ColumnCount
       {
           get
           {
               return _colCount;
           }
       }
       
       //magic happens when event is triggerd
       protected void OnPropertyChanged(Cell cell, string t)
       {
           PropertyChangedEventHandler handler = cellPorpertyChanged;
           if(handler != null)
           {
               handler(this, new PropertyChangedEventArgs(t));
           }
       }

       //handels events when they are triggerd
       private void handler (object sender, PropertyChangedEventArgs e)
       {
           CellChild cell = sender as CellChild;

           setValue(cell);

           OnPropertyChanged(sender as Cell, e.PropertyName);

           


       }


       //sets the value property of the cell
       //will be more useful later
       private void setValue(CellChild cell)
       {
           string cellText = cell.Text;

           if (cellText.StartsWith("="))
           {
               evaluateExpression(cell);

           }
           else
           {
               //not an expression "write" cellText to value

               cell.Value = "cellText";
           }
       }

       //finds the cell to copy text from and 
       //copys that cells text into current cell
       private void evaluateExpression(CellChild cell)
       {
           //we already know that text starts with "="

           string text = cell.Text;
           String expression = "";
           for (int i = 1; i < text.Length; i++)
           {
               expression += text.ElementAt(i);
           }
           // now need to determine opperation
           
           //in A4 we only need to pull values from another cell

           //Note for now i will hardcode this part
           char colLetter = expression.ElementAt(0);
           char rowLetter = expression.ElementAt(1);
           char rowLetter2 = '\0';
           if(expression.Length > 2)
           {
               //could be done in a loop for larger values
               rowLetter2 = expression.ElementAt(2);
           }
           //string to parse as int for row number
           string rowString = rowLetter.ToString() + rowLetter2.ToString();





           int r = -1;
           bool isNum = int.TryParse(rowString, out r);
           bool inRange = false;
           
           colLetter = char.ToUpper(colLetter);
           //determins if column is in range for our simple sheet
           if(colLetter >= 'A' || colLetter <= 'Z')
           {
               inRange = true;
           }

           if(inRange && isNum)
           {
               //expression is valid and in range

               int col = (int)colLetter - 65;
               //get text to copy and copys to current cell
               cell.Text = getCell(r, col).Text;


           }
           
       }

       //demo code when menu item is selected
       public void doDemo()
       {
           Random r = new Random();
           //first fill in column B
           for(int i = 0; i < _rowCount; i++)
           {
               cells[1, i].Text = string.Format("This is cell B{0}", i + 1);
           }
           //then do column A
           for (int i = 0; i < _rowCount; i++)
           {
               cells[0, i].Text = string.Format("=B{0}", i);
           }
           //now do random stuff and things
           for (int i = 0; i < 50; i++)
           {
              //cells [col, row]
              cells[r.Next(2, 26), r.Next(0, 50)].Text = "Stuff and Things";
           }
           


       }



       //constructor for spreadsheet class
        public Spreadsheet(int numRows, int numCols)
        {
            _rowCount = numRows;
            _colCount = numCols;

            cells = new CellChild[numCols, numRows];

            for(int i = 0; i < numCols; i++)
            {
                //i = col
                for(int j = 0; j < numRows; j++)
                {
                    //j = row
                    cells[i, j] = new CellChild(i, j);
                    cells[i, j].PropertyChanged += SpreadsheetPropertyChanged;
                }
            }
        }
       //handles events when text is changed by ui
       public void SpreadsheetPropertyChanged(object sender, PropertyChangedEventArgs e)
       {
           CellChild c = sender as CellChild;
           PropertyChangedEventHandler handler = cellPorpertyChanged;

           if (handler != null && c.Text != null && '=' != c.Text.ElementAt(0))
           {
               handler(c, new PropertyChangedEventArgs(c.Text));

           }
           else if (handler != null && c.Text != null &&'=' == c.Text.ElementAt(0))
           {
               // c.Text starts with "="...need to evaluate expression

               evaluateExpression(c);




           }

       }
       //returns cell at given row and index
       public Cell getCell(int row, int col)
       {
           if (row <= _rowCount && col <= _colCount)
           {
               //changed here
               return cells[col, row];
           }
           else
           {
               return null;
           }
       }


    }
}
