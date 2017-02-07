using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Xml.Linq;


namespace Spreadsheet
{
    public class Spreadsheet
    {
        /************************************************************************
         *                      CellChild class decleration
         ************************************************************************/
        public class CellChild : Cell
        {
           //create inherited Cell class to create cells
           public List<string> _IdependOn = new List<string>();
           public List<string> _DependOnMe = new List<string>();

            /************************************************************************
            *                      CellChild class constructor
            ************************************************************************/
            public CellChild(int row, int col, int color)
                : base(row, col, color)
            {

            }
            /************************************************************************
            *                         addIDependOn(string name)
            *                         adds cell that I depend on
            ************************************************************************/
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

            /************************************************************************
            *                         addDependOnMe(string name)
            *                         adds cell that depends on me
            ************************************************************************/
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

            /************************************************************************
            *                         removeDependOnMe(string name)
            *                         removes cell that depends on me
            ************************************************************************/
            public void removeDependOnMe(string name)
            {
                if(_DependOnMe.Contains(name))
                {
                    _DependOnMe.Remove(name);
                }
            }            
        }
        /************************************************************************************
        *                                  END OF CELLCHILD
        *************************************************************************************/

       
        /************************************************************************
         *                           Class Members
         ************************************************************************/
        public event PropertyChangedEventHandler cellPorpertyChanged;

        //col,row
        CellChild[,] _cells;
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
         *      private setValue(CellChild cell)
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
         *              private evalueateExpression:
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
            int COLORSPACE = 0xFF * 0xFF * 0xFF;
            int ALPHA = 0xFF << 24;
            for(int i = 0; i < 50; i++)
            {
                updateCellColorFromUI(r.Next(0, 50), r.Next(0, 26), r.Next(COLORSPACE)+ ALPHA);
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
            ************************************************************************************************/
            _cells = new CellChild[numRows, numCols];

            for (int i = 0; i < numRows; i++)
            {
                //i = row
                for (int j = 0; j < numCols; j++)
                {
                    //j = col
                    _cells[i, j] = new CellChild(i, j, defaultColor);
                    _cells[i, j].PropertyChanged += SpreadsheetPropertyChanged;
                }
            }
        }

        /************************************************************************************************
         *                     updateCellDataFromUI(int row, int col, string text)
         *                     updates cell text from UI
         *                     Also takes care of Redo/Undo                     
         ************************************************************************************************/
        public void updateCellDataFromUI(int row, int col, string text)
        {
            CellChild cell = getCell(row, col);
            if(text == cell.Text || text == cell.Value)
            {
                //nothing has changed 
                return;
            }

            


            RU.PushTextChange(cell);

            string result = checkCircleRef(cell, text);
            if (result == "OK")
            {
                updateCellData(row, col, text);
            }
            else
            {
                updateCellData(row, col, result);
                getCell(row, col).Value = text;

            }
        }
        /************************************************************************************************
         *            internal updateCellData(int row, int col, string text)
         *                     unified location to update text
         *                     from inside spreadsheet
         *                     this function is the only place where
         *                     dependcies are cleared when text is changed
         ************************************************************************************************/
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
         ************************************************************************************************/
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
         *            internal updateCellColorFromUI(int row, int col, int color)
         *                     updates cell bgcolor and does not handle undo/redo         
         ************************************************************************************************/
        internal void updateCellColor(int row, int col, int color)
        {
            CellChild cell = getCell(row, col);
            cell.BGColor = color;
        }
        /*******************************************************************************************************
         *                      SpreadsheetPropertyChanged(object sender, PropertyChangedEventArgs e)
         *                      triggerd when UI changes the text
         *                      of a cell
         *******************************************************************************************************/
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
         *               private PropertyChangedEventArgs(CellChild c, string name)
         *                       magic function handles property changed
         *                       notifications
         ************************************************************************************************/
        private void PropertyChangedEventArgs(CellChild c, string name)
        {
            PropertyChangedEventHandler handler = cellPorpertyChanged;

            if(handler != null)
            {
                handler(c, new PropertyChangedEventArgs(name));
            }
        }

        /**************************************************************************
         *                 private updateDependants(CellChild c)
         *                         updates cell that depends on us
         **************************************************************************/
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
         *               private INoLongerDependOnOthers(CellChild c)
         *                       utility function to remove c from
         *                       cells that c used to depend on
         *                       then clears c's _IdependOn list
         ************************************************************************************************/
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
         **************************************************************************/
        public CellChild getCell(int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return null;
            }
            if (row < _rowCount && col < _colCount)
            {
                
                return _cells[row, col];
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
            try
            {
                int row = int.Parse(name.Substring(1)) - 1;
                return getCell(row, col);
            }
            catch(FormatException)
            {
                return null;
            }
        }

        /************************************************************************************
         *                                  undoAction()
         *                                  undoes the previous action
         ************************************************************************************/
        public void undoAction()
        {
            RU.Undo(this);
        }

        /************************************************************************************
         *                                  redoAction()
         *                                  redoes the previously undon action
         ************************************************************************************/
        public void redoAction()
        {
            RU.Redo(this);
        }

        /************************************************************************************
         *                                  Copy(int row, int col)
         *                                  gets cell value at row,col
         *                                  used for clipboard
         ************************************************************************************/
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

        /************************************************************************************
         *                          private Clear()
         *                                  clears the sheet
         *                                  used in loading
         *                                  and when file->close is clicked
         ************************************************************************************/
        private void Clear()
        {
            foreach(CellChild cell in _cells)
            {
                updateCellData(cell.RowIndex, cell.ColIndex, "");
                updateCellColor(cell.RowIndex, cell.ColIndex, -1);
            }
            //empty redo and undo stacks
            RU.ClearRedoUndo();
        }

        /************************************************************************************
        *                                  ClearFromUI()
        *                                  public function used to clear sheet
        *************************************************************************************/
        public void ClearFromUI()
        {
            Clear();
        }

        /************************************************************************************
        *                                  Save(Stream file)
        *                                  creates an XmlWriter and
        *                                  writies XML to the stream, file
        *************************************************************************************/
        public bool Save(Stream file)
        {

            XmlWriterSettings x = new XmlWriterSettings();
            x.Indent = true;          

            XmlWriter writer = XmlWriter.Create(file, x);         

            if(writer != null)
            {
                Save(writer);
                writer.Close(); //who knew this stupid function could save hours of pain...lol
                file.Close();
                return true;
            }
            return false;
        }

        /************************************************************************************
        *                           private Save(XmlWriter xWriter)
        *                                   private function to do the acutal
        *                                   writing to an XML file
        *************************************************************************************/
        private void Save(XmlWriter xWriter)
        {
            xWriter.WriteStartElement("Spreadsheet");

            var changedCells = from Spreadsheet.CellChild cell in _cells where cell.HasChanged() select cell;

            foreach(Spreadsheet.CellChild cell in changedCells)
            {
                //if spreadsheet cells are extended to 
                //support more things this will need to be
                //updated

                xWriter.WriteStartElement("Cell");
                xWriter.WriteAttributeString("Name", cell.Name);
                xWriter.WriteElementString("Text", cell.Text);
                xWriter.WriteElementString("Value", cell.Value);
                xWriter.WriteElementString("BGColor", cell.BGColor.ToString());
                xWriter.WriteEndElement();
            }
            xWriter.WriteEndElement();
            xWriter.WriteEndDocument();
        }

        /************************************************************************************
        *                                  Load(Stream file)
        *                                  public function used to load cells
        *                                  from an XML file  
        *************************************************************************************/
        public bool Load(Stream file)
        {
            XDocument document = null;
            try
            {
                document = XDocument.Load(file);
            }
            catch(Exception)
            {
                return false;
            }
            if(document == null)
            {
                return false;
            }
            else
            {
                //document is a valid XML file
                //clear spreadsheet to begin loading from file
                Clear();
                if("Spreadsheet" != document.Root.Name)
                {
                    //XML file is not a spradsheet
                    return false;
                }

                Load(document.Root);
                return true;
            }
        }

        /************************************************************************************
        *                          private Load(XElement element)
        *                                  private function to load cells from
        *                                  an XML file
        *************************************************************************************/
        private void Load(XElement element)
        {
            foreach(XElement child in element.Elements("Cell"))
            {
                CellChild cell = getCell(child.Attribute("Name").Value);

                if(null != cell)
                {
                    //x vars are XElements inside child
                    //if spreadsheet cells are extended to 
                    //support more things this will need to be
                    //updated

                    var xText = child.Element("Text");
                    var xValue = child.Element("Value");
                    var xColor = child.Element("BGColor");

                    if(xValue != null)
                    {
                        updateCellData(cell.RowIndex, cell.ColIndex, xValue.Value);
                    }
                    else if(xText != null)
                    {
                        updateCellData(cell.RowIndex, cell.ColIndex, xText.Value);
                    }

                    int color = -1;                    
                    int.TryParse(xColor.Value, out color);
                    if(color != -1)
                    {
                        updateCellColor(cell.RowIndex, cell.ColIndex, color);
                    }
                }
            }
        }

        private string checkCircleRef(CellChild cell, string text)
        {
            if(text.StartsWith("=") == false)
            {
                return "OK";
            }
            else
            {
                text = text.ToUpper();
                string[] oporators  = new string [] {"=", "+", "-", "*", "/", "(", ")"};
                //text starts with "=" need to check for self/circle ref
                //begin by tokenizing text on operators
                string[] tokens = text.Split(oporators, StringSplitOptions.RemoveEmptyEntries);

                foreach(string s in tokens)
                {                    
                    if(s == cell.Name)
                    {
                        return "!SELFREF";
                    }
                    else if(s[0] > 64 && s[0] < 91)
                    {
                        //s represents a  variable to ref
                        CellChild varCell = getCell(s);
                        if(varCell == null)
                        {
                            //posibly out of range or just invalid
                            return "!BADREF";
                        }
                        //need to check if varCell or any of its dependancies depend on cell
                       if(checkDependencies(varCell, cell.Name) == true)
                       {
                           return "!CIRREF";
                       }

                    }

                }
                return "OK";



            }

        }

        private bool checkDependencies(CellChild toCheck, string toFind)
        {
            foreach(string s in toCheck._IdependOn)
            {
                if (s == toFind)
                {
                    return true;
                }
                if(checkDependencies(getCell(s), toFind) == true)
                {
                    return true;
                }
            }

            return false;
        }

    }
    /************************************************************************************
    *                                  END OF SPREADSHEET
    *************************************************************************************/
}