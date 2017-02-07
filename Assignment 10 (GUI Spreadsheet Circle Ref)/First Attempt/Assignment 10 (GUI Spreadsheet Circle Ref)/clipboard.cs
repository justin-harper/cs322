using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_9
{
    class clipboard
    {
        public string _copy;
        public string _paste;

        public string xpaste
        {
            get { return _paste; }
            set { _paste = value; }
        }

        public string xcopy
        {
            get { return _copy; }
            set { _copy = value; }
        }
        public clipboard()
        {

        }

        public void copy()
        {
            System.Windows.Forms.Clipboard.SetText(xcopy);
        }
        public void paste()
        {
            xpaste = System.Windows.Forms.Clipboard.GetText();
        }



    }
}
