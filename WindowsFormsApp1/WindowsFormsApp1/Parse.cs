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
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Extensions.Primitives;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    internal class Parse
    {
        public Form1 f;
        public void strat()
        {
            string p = "client.dll";
            string line;


            using (StreamReader sr = new StreamReader(f.getPath().Text))
            {
                line = sr.ReadToEnd();
            }

            Regex reg = new Regex("\\b1134|1200\\b");
            line = reg.Replace(line, f.getDistance().Text);


            // line = line.Replace("1134", "1550"); 
            using (StreamWriter sw = new StreamWriter(p))
            {
                sw.Write(line);
                sw.Flush();
            }
        }
    }   
}