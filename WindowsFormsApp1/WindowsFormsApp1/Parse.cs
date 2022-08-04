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
        readonly Form1 f;
        public Parse(Form1 f1)
        {
            f = f1;            
        }

        public void pars()
        {            
            Console.WriteLine(f.textBox1.Text);
            Console.WriteLine(f.textBox2.Text);           

            string p = "client.dll";
            string line;


            Console.WriteLine(p);

            using (StreamReader sr = new StreamReader(p))
            {
                line = sr.ReadToEnd();
            }

            Regex reg = new Regex("\\b1134|1200\\b");
            

            if (reg.Match(line).Success)
            {
                Console.WriteLine("Найдено1");
                line = reg.Replace(line, f.textBox2.Text);
            }
            else
            {
                Console.WriteLine("Не найдено1");
                Regex reg1 = new Regex($"\\b{f.textBox2.Text}\\b");
                if(reg1.Match(line).Success)
                {
                    line = reg1.Replace(line, f.oldParam);
                    Console.WriteLine("Найдено2");
                }
                else
                {
                    Console.WriteLine("Не найдено2");
                }                
            }



            using (StreamWriter sw = new StreamWriter(p))
            {
                sw.Write(line);
                sw.Flush();
            }
        }
    }   
}