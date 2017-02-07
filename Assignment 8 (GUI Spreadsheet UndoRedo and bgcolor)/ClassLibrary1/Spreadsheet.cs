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
        /************************************************************************
         *                      CellChild class decleration
         ************************************************************************/
        public class CellChild : Cell
        {
           public List<string> _IdependOn = new List<string>();
           public List<string> _DependOnMe = new List<string>();
            
            

            //create inherited Cell class to create cells
            public CellChild(int row, int col, int color)
                : base(row, col, color)
            {

            }
            public void addIDependOn(string name)
            {
                foreach (string s in _IdependOn)
                {
                    if (s == name)
                    {
                        return;
                    }
                }
                _IdependOn.Add(name);
            }
            public void addDependOnMe(string name)
            {
                foreach(string s in _DependOnMe)
                {
                    if(s == name)
                    {
                        return;
                    }
                }
                _DependOnMe.Add(name);
            }           
            public void removeDependOnMe(string name)
            {
                if(_DependOnMe.Contains(name))
                {
                    _DependOnMe.Remove(name);
                }
            }
            public bool IDependOnOthers
            {
                get
                {
                    if(_IdependOn.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }                
            }
        }





       
        /************************************************************************
         *                           Class Members
         ************************************************************************/
        public event PropertyChangedEventHandler cellPorpertyChanged;

        //col,row
        CellChild[,] cells;
        int _rowCount;
        int _colCount;
        public Dictionary<string, string> myDict = new Dictionary<string, string>();

        public RedoUndo RU = new RedoUndo();

        /***************************************************************************
         *                       End of class members
         ***************************************************************************/

        /************************************************************************
         *                          RowCount property
         ************************************************************************/ 
        public int RowCount
        {
            get
            {
                return _rowCount;
            }
        }
        /**********************************************************************
         *                          ColumnCount property  
         **********************************************************************/
        public int ColumnCount
        {
            get
            {
                return _colCount;
            }
        }        
        /*************************************************************
         *          setValue:
         *              evaluates expression if cell.Text starts
         *              with "="
         *              if not
         *              sets the value property of the cell
         *              to cell.Text
         *************************************************************/
        private void setValue(CellChild cell)
        {   
         
            string cellText = cell.Text;
            if (cellText != null && cellText != "")
            {
                if (cellText.StartsWith("="))
                {
                    cell.Value = cell.Text;
                    evaluateExpression(cell);
                }                
            }            
            
            //no matter what change took place I need to update
            //cells that depend on me
            updateCellsThatDependOnMe(cell);
        }
        /***************************************************************************************************
         *                 evalueateExpression:
         *                      finds the cell to copy text from and 
         *                      copys that cells text into current cell
         * *************************************************************************************************/
        private void evaluateExpression(CellChild cell)
        {
            //we already know that text starts with "="

            string text = cell.Text;
            cell.Value = cell.Text;
            String expression = "";
            expression = expression.ToUpper();           
            expression += text.Substring(1);
            

            // now need to determine opperation

            if (expression.Length < 2)
            {
                cell.Text = expression;
                return;
            }

            ExpressionTree.Tree expTree = new ExpressionTree.Tree();
            char startChar = expression[0];
            startChar = char.ToUpper(startChar);

            for (int j = 0; j < expression.Length; j++)
            {
                if (expression.ToUpper()[j] >= 'A' && expression.ToUpper()[j] <= 'Z')
                {
                    //variable
                    bool noOp = true;
                    string varName = "";
                    for (int i = j; i < expression.Length && noOp; i++)
                    {

                        switch (expression[i])
                        {
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '(':
                            case ')':
                                j = i;
                                noOp = false;
                                break;
                            default:
                                varName += expression[i];
                                continue;

                        }
                    }

                    //we have a var name to get the value of
                    CellChild variable = getCell(varName.ToUpper());
                    
                    //this cell depends on variable cell
                    cell.addIDependOn(variable.Name);

                    //notify varible cell that i depend on it
                    variable.addDependOnMe(cell.Name);
                    
                    try
                    {
                        expTree.setVariable(variable.Name, double.Parse(variable.Text));
                        
                    }
                    catch (Exception)
                    {
                        //variable is not a double...humm
                        //is it a string
                        if (variable.Text != null)
                        {
                            cell.Text = variable.Text;
                        }
                        else
                        {
                            cell.Text = "";
                        }
                        return;
                    }
                    
                }
                //not a variable...perhaps a constant
                else if (expression[j] >= '0' && expression[j] <= '9')
                {
                    bool noOp = true;
                    string constant = "";
                    for (int i = j; i < expression.Length && noOp; i++)
                    {
                        switch (expression[i])
                        {
                            case '+':
                            case '-':
                            case '*':
                            case '/':
                            case '(':
                            case ')':
                                j = i;
                                noOp = false;
                                break;
                            default:
                                constant += expression[i];
                                continue;
                        }
                    }
                    
                }
                
            }
            expTree.buildTree(expression.ToUpper());
            try
            {
                cell.Text = expTree.Evaluate().ToString();
            }
            catch (DivideByZeroException e)
            {
                cell.Text = e.Message;
            }          
            return;
        }

        /**********************************************************************
         *                         doDemo
         *                         runs demo code
         *                         shows that expressions are working
         **********************************************************************/
        public void doDemo()
        {
            Random r = new Random();
            //first fill in column B
            for (int i = 0; i < _rowCount; i++)
            {
                updateCellDataFromUI(i, 1, string.Format("This is cell B{0}", i + 1));

            }
            //then do column A
            for (int i = 1; i < _rowCount + 1; i++)
            {
                updateCellDataFromUI(i - 1, 0, string.Format("=B{0}", i));
            }
            //now do random stuff and things
            for (int i = 0; i < 50; i++)
            {
                updateCellDataFromUI(r.Next(0, 50), r.Next(0, 26), "Stuff and Things");
            }            
        }

        /************************************************************************************************
                                Constructor for spreadsheet class       
        *************************************************************************************************/
        public Spreadsheet(int numRows, int numCols, int defaultColor)
        {
            _rowCount = numRows;
            _colCount = numCols;
            /***********************************************************************************************
                                    ROWS, COLS!!!!!!!!!!!!!!!!!!!!!!!
            ***********************************************************************************************/
            cells = new CellChild[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                //i = row
                for (int j = 0; j < numCols; j++)
                {
                    //j = col
                    cells[i, j] = new CellChild(i, j, defaultColor);
                    cells[i, j].PropertyChanged += SpreadsheetPropertyChanged;
                }
            }
        }

        /************************************************************************************************
         *                     updateCellDataFromUI(int row, int col, string text)
         *                     updates cell text from UI
         *                     Also takes care of Redo/Undo                     
         *************************************************************************************************/
        public void updateCellDataFromUI(int row, int col, string text)
        {
            CellChild cell = getCell(row, col);
            if(text == cell.Text || text == cell.Value)
            {
                //nothing has changed 
                return;
            }


            RU.PushTextChange(cell);
            updateCellData(row, col, text);
        }
        /************************************************************************************************
         *               internal updateCellData(int row, int col, string text)
         *                     unified location to update text
         *                     from inside spreadsheet
         *                     this function is the only place where
         *                     dependcies are cleared when text is changed
         *************************************************************************************************/
        internal void updateCellData(int row, int col, string text)
        {   
            //get cell to update
            CellChild cell = getCell(row, col);

            //remove old dependcies
            INoLongerDependOnOthers(cell);

            cell.Value = text;
            cell.Text = text;
            //updateCellsThatDependOnMe(cell);
        }
        /************************************************************************************************
         *                     updateCellColorFromUI(int row, int col, int color)
         *                     updates cell bgcolor and handles undo/redo         
         *************************************************************************************************/
        public void updateCellColorFromUI(int row, int col, int color)
        {
            CellChild cell = getCell(row, col);
            if(color == cell.BGColor)
            {
                //no change has occured
                return;
            }


            RU.PushBGColorChange(cell);
            updateCellColor(row, col, color);
        }
        /************************************************************************************************
         *              internal updateCellColorFromUI(int row, int col, int color)
         *                     updates cell bgcolor and does not handle undo/redo         
         *************************************************************************************************/
        internal void updateCellColor(int row, int col, int color)
        {
            CellChild cell = getCell(row, col);
            cell.BGColor = color;
        }
        /***************************************************************************************
         *                                 SpreadsheetPropertyChanged
         *                                 triggerd when UI changes the text
         *                                 of a cell
         ***************************************************************************************/
        public void SpreadsheetPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CellChild c = sender as CellChild;
            if (e.PropertyName == "Text")
            {
                setValue(c);
            }
            if(e.PropertyName == "BGColor")
            {
                //not sure if i need to do something here
            }            

            PropertyChangedEventArgs(c, e.PropertyName);
        }
        /************************************************************************************************
         *                       PropertyChangedEventArgs(CellChild c, string name)
         *                       magic function handles property changed
         *                       notifications
         *************************************************************************************************/
        private void PropertyChangedEventArgs(CellChild c, string name)
        {
            PropertyChangedEventHandler handler = cellPorpertyChanged;

            if(handler != null)
            {
                handler(c, new PropertyChangedEventArgs(name));
            }
        }
        /**************************************************************************
         *                          updateDependants(CellChild c)
         *                          updates cell that depends on us
         ***************************************************************************/
        private void updateCellsThatDependOnMe(CellChild c)
        {
            if(c._DependOnMe.Count == 0)
            {
                return;
            }

            for (int i = 0; i < c._DependOnMe.Count; i++)                
            {
                string s = c._DependOnMe[i];
                CellChild x = getCell(s);
                x.Text = x.Value;
                setValue(x);
            }
        }    
        /************************************************************************************************
         *                       INoLongerDependOnOthers(CellChild c)
         *                       utility function to remove c from
         *                       cells that c used to depend on
         *                       then clears c's _IdependOn list
         *************************************************************************************************/
        private void INoLongerDependOnOthers(CellChild c)
        {
            if(c._IdependOn.Count == 0)
            {
                return;
            }

            for(int i = 0; i < c._IdependOn.Count; i++)
            {
                string s = c._IdependOn[i];
                CellChild x = getCell(s);
                x.removeDependOnMe(c.Name);
            }
            c._IdependOn.Clear();
        }
        /**************************************************************************
         *                          getCell(row, col)
         *                          returns cell at [row, col]
         ***************************************************************************/
        public CellChild getCell(int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return null;
            }
            if (row < _rowCount && col < _colCount)
            {
                
                return cells[row, col];
            }
            else
            {
                return null;
            }
        }
        /************************************************************************************
         *                                  getCell(string name)
         *                                  retuns cell by name
         ************************************************************************************/
        public CellChild getCell(string name)
        {
            int col = name[0] - 65;
            int row = int.Parse(name.Substring(1)) - 1;
            return getCell(row, col);
        }
        public void undoAction()
        {
            RU.Undo(this);
        }
        public void redoAction()
        {
            RU.Redo(this);
        }

        public string Copy(int row, int col)
        {
            CellChild cell = getCell(row, col);

            if(cell.Value != null)
            {
                return cell.Value;
            }
            else
            {
                return null;
            }
        }


       
    }
}
