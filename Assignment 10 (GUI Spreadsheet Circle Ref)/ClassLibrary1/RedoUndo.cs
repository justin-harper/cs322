using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spreadsheet
{
    public class RedoUndo
    {
        private Stack<RedoUndoCmd> UndoStuff = new Stack<RedoUndoCmd>();
        private Stack<RedoUndoCmd> RedoStuff = new Stack<RedoUndoCmd>();

        //a redo can only occur after an undo!!!

        public void ClearRedoUndo()
        {
            UndoStuff.Clear();
            RedoStuff.Clear();
        }
        public void PushTextChange(Spreadsheet.CellChild cell)
        {
            //save current state of cell onto the undo stack
            UndoStuff.Push(new RedoUndoCmd(cell, "Text"));
        }
        public void PushBGColorChange(Spreadsheet.CellChild cell)
        {
            UndoStuff.Push(new RedoUndoCmd(cell, "BGColor"));
        }
        public void Undo(Spreadsheet spreadsheet)
        {
            if (UndoIsNotEmpty())
            {
                //stack is not empty...proceed
                RedoUndoCmd cmd = UndoStuff.Pop();
                RedoUndoCmd inverseCmd = cmd.Restore(spreadsheet);
                RedoStuff.Push(inverseCmd);
            }
        }
        public void Redo(Spreadsheet spreadsheet)
        {
            if (RedoIsNotEmpty())
            {
                RedoUndoCmd cmd = RedoStuff.Pop();
                UndoStuff.Push(cmd.Restore(spreadsheet));
            }
        }
        public bool RedoIsNotEmpty()
        {
            if (RedoStuff.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool UndoIsNotEmpty()
        {
            if (UndoStuff.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string peekUndo()
        {
            return UndoStuff.Peek().OP;            
        }
        public string peekRedo()
        {
            return RedoStuff.Peek().OP;
        }


    }    
    public class RedoUndoCmd
    {
        string _OP;
        string _string_ToSave;
        //public List<string> _IdependOn_ToSave;
        //public List<string> _DependOnMe_ToSave;
        int _BGColor_ToSave;
        int _Row;
        int _Col;

        public string OP
        {
            get
            {
                return _OP;
            }
        }


        private RedoUndoCmd()
        {

        }

        public RedoUndoCmd(Spreadsheet.CellChild cell, string op)
        {
            _Row = cell.RowIndex;
            _Col = cell.ColIndex;

            if (op == "Text")
            {   
                //a text change is about to occur...perserve current text or value to the "stack"                

                _OP = "Text";

                if(cell.Value == "" || cell.Value == null)
                {
                    _string_ToSave = "";
                }
                else if (cell.Value.StartsWith("="))
                {
                    //_IdependOn_ToSave = cell._IdependOn.ToList();
                    //_DependOnMe_ToSave = cell._DependOnMe.ToList();

                    _string_ToSave = cell.Value;
                }
                else
                {
                    _string_ToSave = cell.Text;
                }
            }
            else if (op == "BGColor")
            {
                //a Background color change is about to occur

                _OP = "BGColor";

                _BGColor_ToSave = cell.BGColor;
            }
            else
            {
                //if we get here than throw execption
                //this is where further devlopment would occur if we 
                //want to preserve more funtionality in the undo redo stacks

                throw new Exception("OP NOT Implemented");
            }
        }

        public RedoUndoCmd Restore(Spreadsheet spreadsheet)
        {
            Spreadsheet.CellChild cell = spreadsheet.getCell(_Row, _Col);

            RedoUndoCmd inverse = new RedoUndoCmd();

            inverse._Row = _Row;
            inverse._Col = _Col;
            inverse._OP = _OP;

            if ("Text" == _OP)
            {   
                if (cell.Value != null && cell.Value != "")
                {

                    if (cell.Value.StartsWith("="))
                    {
                        inverse._string_ToSave = cell.Value;
                    }
                }
                else
                {
                    if (cell.Text != null)
                    {
                        inverse._string_ToSave = cell.Text;
                    }
                    else
                    {
                        inverse._string_ToSave = "";
                    }
                }
                spreadsheet.updateCellDataFromUI(_Row, _Col, _string_ToSave);
            }
            else if ("BGColor" == _OP)
            {
                inverse._BGColor_ToSave = cell.BGColor;
                spreadsheet.updateCellColor(_Row, _Col, _BGColor_ToSave);
            }
            else
            {
                throw new Exception("OP NOT Implemented");
            }
            return inverse;
        }
    }

}

 
