using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Spreadsheet
{
   /************************************************************************
    *                           abstract cell class to be
    *                           used by 
    *                           spreadsheet application
    ************************************************************************/    
    public abstract class Cell : INotifyPropertyChanged
    {
        private int rowIndex;
        private int columnIndex;
        protected string _text;
        protected string _value;
        protected int _backgroundColor;

        public event PropertyChangedEventHandler PropertyChanged;

       public Cell(int row, int col, int color)
        {
            rowIndex = row;
            columnIndex = col;
            _backgroundColor = color;
        }

        public string Name
       {
           get
           {
               return (char)(columnIndex + 65) + (rowIndex + 1).ToString();
           }
       }

        public int RowIndex
        {
            get
            {
                return rowIndex;
            }
        }
        public int ColIndex
        {
            get
            {
                return columnIndex;
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if(value == _text)
                {
                    return;
                }
                else
                {
                    _text = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }               
        }
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if(value == _value)
                {
                    return;
                }
                else
                {
                    _value = value;                                       
                }
            }
        }
        public int BGColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BackgroundColor"));
            }
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, e);
            }
        }
    }
}
