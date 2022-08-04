using System;
using System.IO;
using System.Text;
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
            string line;           
            Console.WriteLine(f.fullPath);

            using (StreamReader sr = new StreamReader(f.fullPath, Encoding.Default, false))
            {
                line = sr.ReadToEnd();
            }

            if(f.isWrited)
            {
                Regex reg = new Regex("\\b1200|1134\\b");
                Console.WriteLine("+++++ " + f.oldParam);
                
                if (reg.Match(line).Success)
                {
                    Console.WriteLine("Найдено1");
                    line = reg.Replace(line, f.textBox2.Text);
                }
                else
                {
                    Console.WriteLine("Не найдено1");
                    Regex reg1 = new Regex($"\\b{f.oldParam}\\b");
                    if (reg1.Match(line).Success)
                    {
                        line = reg1.Replace(line, f.textBox2.Text);
                        Console.WriteLine("Найдено2");
                    }
                    else
                    {
                        Console.WriteLine("Не найдено2");
                    }
                }
            }
            else
            {
                Regex reg = new Regex("\\b1200|1134\\b");

                if (reg.Match(line).Success)
                {
                    Console.WriteLine("Найдено1");
                    line = reg.Replace(line, f.textBox2.Text);
                }
                else
                {
                    Console.WriteLine("Нет данных");
                }               
            }

            using (StreamWriter sw = new StreamWriter(f.fullPath, false, Encoding.Default))
            {
                sw.Write(line);               
            }
        }
    }   
}