using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Assignment_11__Threading_Web_Stuff_
{
    public partial class Assignment11 : Form
    {
        int ARRAYLEN = 10000000;
        bool singleDone = false;
        bool multiDone = false;

        public Assignment11()
        {
            InitializeComponent();
        }

        private void StartSortingButton_Click(object sender, EventArgs e)
        {
            //disable button while processing 
            StartSortingButton.Enabled = false;

            //clear out results text box
            SortingTextBox.Clear();

            //to prevent ui from being blocked we need to start a new thread

            ThreadStart SortClicked = delegate { GOSort(); };
            Thread SortThread = new Thread(SortClicked);
            SortThread.Start();

        }

        private void GOSort()
        {
            //get lots of numbers
            int[][] RandomVals = getRandArrays();
            int[][] singleThreadRands = RandomVals;
            int[][] multiThreadRands = RandomVals;

            //start single treaded sort

            ThreadStart SingleSort = delegate { SingleSortArrays(singleThreadRands); };
            Thread SingleSorter = new Thread(SingleSort);
            SingleSorter.Start();

            //start multi threaded sort

            ThreadStart MultiSort = delegate{MulitSortArrays(multiThreadRands);};
            Thread MultiSorter = new Thread(MultiSort);
            MultiSorter.Start();
        }

        private void SingleSortArrays(int[][] rands)
        {
            //start timer
            Stopwatch T = new Stopwatch();
            T.Reset();
            T.Start();
            //go through each array and sort it
            //this might be a minute
            foreach(var x in rands)
            {
                Array.Sort(x);
            }
            //sorted...stop timer
            T.Stop();
            //single threaded sort is done so update variable
            singleDone = true;

            //need to tell UI thread the time to update the text box
            SortingTextBox.Invoke(new MethodInvoker(delegate
            {
                SortingTextBox.Text += "Single thread: " + T.ElapsedMilliseconds + "ms \r\n";
                if (singleDone == true && multiDone == true)
                {
                    //if we are the last one done reenable the button
                    StartSortingButton.Enabled = true;
                }
            }));
        }

       private void MulitSortArrays(int[][] rands)
       {
           Stopwatch T = new Stopwatch();
           T.Reset();
           T.Start();

           //need to create a Task for each array
           Task[] tasks = new Task[8];
           int i = 0;
           //assign each task and start it
           foreach(var x in rands)
           {
               tasks[i++] = Task.Run(() =>
                   {
                       Array.Sort(x);
                   });
           }
           //wait until all tasks in tasks[] finish
           Task.WaitAll(tasks);
           //stop timer
           T.Stop();
           //update variable indicating that we are done
           multiDone = true;

           //once again notify UI therad of our time

           SortingTextBox.Invoke(new MethodInvoker(delegate
               {
                   SortingTextBox.Text += "Multithreaded time: " + T.ElapsedMilliseconds + "ms \r\n";
                   if (singleDone == true && multiDone == true)
                   {
                       //if we are the last one done (doubtful) reenable the button
                       StartSortingButton.Enabled = true;
                   }
               }));
       }
        public int [][] getRandArrays()
        {
            //fill a 2 dimensional array with lots of numbers
            int[][] array = new int[8][];
            int[] randoms = new int[ARRAYLEN];
            Random rand = new Random((int)(System.DateTime.Now.ToOADate()));
            for (int i = 0; i < 8; i++)
            {

                for (int j = 0; j < ARRAYLEN; j++)
                {
                    randoms[j] = rand.Next();
                }
                array[i] = randoms;
            }
            return array;
        }
        private void ResetButton_Click(object sender, EventArgs e)
        {
            //Reset UI to default
            SortingTextBox.Clear();
            URLBox.Clear();
            URLBox.Text = "http://www.wsu.edu";
            HTMLBox.Clear();
        }
        private void DownloadHTMLButton_Click(object sender, EventArgs e)
        {
            HTMLBox.Clear();
            URLBox.Enabled = false;

            if(URLBox.Text.Equals(""))
            {
                //empty string is an invalid web address
                if(DialogResult.Yes == MessageBox.Show("Did you forget to enter \r\n\"http://www.google.com\"?","Message", MessageBoxButtons.YesNo))
                {
                    URLBox.Text = "http://www.google.com";
                }
                else
                {
                    //can't process empty string
                    return;
                }
            }
            
            if((URLBox.Text.StartsWith("http://")) || (URLBox.Text.StartsWith("https://")))
            {
            
            }
            else
            {
                //prepend "http://"
                string temp = "http://";
                temp += URLBox.Text;
                URLBox.Text = temp;
            }
            //now begin processing after disableing button
            DownloadHTMLButton.Enabled = false;
            //start a new thread so we don't block the ui thread
            ThreadStart HTMLGet = delegate { GetHTML(); };
            Thread HTMLGetter = new Thread(HTMLGet);
            HTMLGetter.Start();
        }
        private void GetHTML()
        {
            String HTML = "";
            try
            {
                //might throw execption if the computer is not connected to the internet
                //or if the url is invalid
                using (WebClient WC = new WebClient())
                {
                    Stream s;
                    s = WC.OpenRead(URLBox.Text);
                    StreamReader reader = new StreamReader(s);
                    HTML = reader.ReadToEnd();
                }
            }
            catch(WebException)
            {
                HTML = "Invalid web address \r\nOr no internet connection \r\nPlease try again\r\n";
            }
            catch(ArgumentException)
            {
                HTML = "Invalid web address \r\nPlease try again\r\n";
            }
            catch(Exception)
            {
                //just in case
                HTML = "Unknown Error\r\nPlease try a differnt address\r\n";
            }
            //tell the ui we are done and update the ui element with the large string HTML
            HTMLBox.Invoke(new MethodInvoker(delegate
            {
                //this wil cause a short block on the ui thread as the HTMLBox is updated
                HTMLBox.Text = HTML;
                DownloadHTMLButton.Enabled = true;
                URLBox.Enabled = true;
            }));
        }
    }
}
