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

namespace Assignment_12__GUI_Tries_
{
    public partial class Form1 : Form
    {
        Trie sTrie;
        int stopIndex = 0;
        bool stage2 = false;
        public Form1()
        {
            InitializeComponent();            
        }
        private void GetInputButton_Click(object sender, EventArgs e)
        {
            GetInputButton.Enabled = false;
            DictionaryBox.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            DialogResult result = ofd.ShowDialog();
            if(result == DialogResult.OK)
            {
                Task getStuff;
                getStuff = Task.Run(() =>
                {
                    stage2 = true;
                    TaskInput(ofd.OpenFile());

                    if (AutoBuildTreieCheckBox.Checked)
                    {
                        BuildTrieButton_Click(null, null);
                    }
                });               
            }
            else 
            {
                GetInputButton.Enabled = true;
            }
        }
        private void TaskInput(Stream file)
        {
            string input;
            using(StreamReader r = new StreamReader(file))
            {
               input = r.ReadToEnd();
            }
            DictionaryBox.Invoke(new MethodInvoker(delegate
            {
                DictionaryBox.Text = input;
                GetInputButton.Enabled = true;
            }));
        }

        private void BuildTrieButton_Click(object sender, EventArgs e)
        {
            BuildTrieButton.Enabled = false;
            sTrie = new Trie();
            OutputTextBox.AppendText("Building trie ...\r\n");
            OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
            OutputTextBox.ScrollToCaret();
            Task buildTrie;
            buildTrie = Task.Run(() =>
                {
                    buildTrieFromTextBox();
                });            
        }

        private void buildTrieFromTextBox()
        {
            if(DictionaryBox.Text == "")
            {
                OutputTextBox.Invoke(new MethodInvoker(delegate
                {
                    OutputTextBox.AppendText("Dictionary has not been loaded\r\n");
                    OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                    OutputTextBox.ScrollToCaret();
                }));
                return;
            }
            List<string> tokens = new List<string>(DictionaryBox.Text.Split('\r','\n'));
            tokens.RemoveAll(str => String.IsNullOrEmpty(str));

            List<Task> tasks = new List<Task>();
            OutputTextBox.Invoke(new MethodInvoker(delegate
            {
                OutputTextBox.AppendText("Starting Tasks\r\n");
                OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                OutputTextBox.ScrollToCaret();
            }));
            for(int startChar = 'a'; startChar <= 'z'; startChar++)
            {
                List<string> a = GetAllFromTokens(tokens, (char)startChar);
                tasks.Add(Task.Run(delegate { addListOfStringsToTrie(a); }));
                OutputTextBox.Invoke(new MethodInvoker(delegate
                {
                    OutputTextBox.AppendText("Starting task " + ((char)startChar).ToString() + "\r\n");
                    OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                    OutputTextBox.ScrollToCaret();
                }));
            }
            while(tasks.Count > 0)
            {
                int t = Task.WaitAny(tasks.ToArray(), 15000);
                tasks.Remove(tasks.ElementAt(t));
            }
            MessageBox.Show("Done building trie!");
            bool error = false;
            for(int c = 0; c < tasks.Count; c++)
            {
                Task x = tasks.ElementAt(c);
                if(x.Status == TaskStatus.Running)
                {
                    error = true;
                    OutputPanel.Invoke(new MethodInvoker(delegate
                        {
                            MessageBox.Show("task " + c.ToString() + " did NOT finish!\r\n");
                        }));
                }
            }
            if(!error)
            {
                OutputTextBox.Invoke(new MethodInvoker(delegate 
                {
                    OutputTextBox.AppendText("All tasks finished\r\n");
                    OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                    OutputTextBox.ScrollToCaret();

                    GetChildrenOfRootButton.Enabled = true;
                    InputBox.Enabled = true;
                }));                
                return;
            }
            else
            {
                OutputPanel.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show("I don't know what happend....!");
                    }));
            }            
        }
        private void addListOfStringsToTrie(List<string> a)
        {
            char task = a.ElementAt(0).ElementAt(0);
            
            foreach (string x in a)
            {
                sTrie.AddString(x);
            }

            OutputTextBox.Invoke(new MethodInvoker(delegate
                {
                    OutputTextBox.AppendText("Task " + task.ToString() + " finished!\r\n");
                    OutputTextBox.SelectionStart = OutputTextBox.Text.Length;
                    OutputTextBox.ScrollToCaret();
                }));
        }
        private List<string> GetAllFromTokens(List<string> tokens, char startsWith)
        {
            List<string> toReturn = new List<string>();

            for (int i = stopIndex; i < tokens.Count; i++)
            {
                string x = tokens.ElementAt(i);
                if (x.StartsWith(startsWith.ToString()))
                {
                    toReturn.Add(x);
                    stopIndex++;
                }
                else break;
            }
            if (toReturn.Count == 0)
            {
                return null;
            }
            else
            {
                return toReturn;
            }
        }
        private void GetChildrenOfRootButton_Click(object sender, EventArgs e)
        {
            GetChildrenOfRootButton.Enabled = false;
            if(sTrie == null)
            {
                GetChildrenOfRootButton.Enabled = true;
                MessageBox.Show("Oh no! it looks like the trie is null");
                return;
            }
            Task Go;
            Go = Task.Run(() =>
                {
                    GetChildrenAndUpdateUI();
                });
        }
        private void GetChildrenAndUpdateUI()
        {
            string children = sTrie.childrenOfRoot();

            OutputTextBox.Invoke(new MethodInvoker(delegate
            {
                MessageBox.Show(children);
                GetChildrenOfRootButton.Enabled = true;
            }));
        }
        private void updateInputBox(string prefix)
        {
            lock (sTrie)
            {
                List<string> suffixs = sTrie.getSuffixes(InputBox.Text);
                MessageBox.Show("suffixs count: " + suffixs.Count.ToString());
                suffixs.Sort();

                OutputPanel.Invoke(new MethodInvoker(delegate
                     {
                         StringsListBox.Items.Clear();
                         StringsListBox.Items.AddRange(suffixs.ToArray());
                     }));

            }
        }
        private void InputBox_KeyUp(object sender, KeyEventArgs e)
        {
           Task q = Task.Run(() =>
            {
                updateInputBox(InputBox.Text);
            });
        }
        private void AutoBuildTreieCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(AutoBuildTreieCheckBox.Checked)
            {
                BuildTrieButton.Enabled = false;                
            }
            else
            {
                if(!stage2)
                {
                    BuildTrieButton.Enabled = true;
                }
            }
        }
        private void StringsListBox_DoubleClick(object sender, EventArgs e)
        {
            InputBox.Text = StringsListBox.SelectedItem.ToString();
            InputBox_KeyUp(null, null);
        }
    }
}
