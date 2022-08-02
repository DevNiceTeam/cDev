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
    class Parse
    {
        static string settingsFileName = "settings.properties";
        char[] ch = { '\n', '=' };
        public void strat()
        {
            StreamReader sr2 = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
            var text = sr2.ReadToEnd();
            var param = text.Split(ch);

            string p = "client.dll";
            string line;


            Console.WriteLine(p);
           
            using (StreamReader sr = new StreamReader(p))
            {
                line = sr.ReadToEnd();
            }

            Console.WriteLine(param[3].Trim());
            Regex reg = new Regex("\\b1134|1200\\b");
            line = reg.Replace(line, param[3].Trim());

            sr2.Close();

            // line = line.Replace("1134", "1550");
            using (StreamWriter sw = new StreamWriter(p))
            {
                sw.Write(line);
                sw.Flush();
            }
        }
    }   
}