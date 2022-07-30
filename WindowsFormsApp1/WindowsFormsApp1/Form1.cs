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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        bool isCreated, fileExists, isWrited = false;
        string dop = @"\steamapps\common\dota 2 beta\game\dota\bin\win64";
        string fileName = @"\client.dll";
        static string settingsFileName = "settings.properties";
        string datePath = @"\steamapps\common\dota 2 beta\game\dota";
        string dateFileName = @"\steam.inf";
        string fullPath;

        void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(settingsFileName))
            {                
                txt("Открываю settings");
                StreamReader sr = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
                char[] ch = { '\n', '=' };
                var text = sr.ReadToEnd();   
                





                if (text.Equals("")) // Если файл настроек пустой то ничего пока не делаем 
                {
                    txt("Файл пустой");                   
                }
                else // Если файл настроек не пустой то cчитываем файл выбираем путь и записываем его в TextBox
                {
                    var line = text.Split(ch);                   
                    textBox1.Text = line[1].Trim();
                    textBox2.Text = line[3].Trim();
                    isWrited = true;
                    sr.Close();




                    StreamReader str = new StreamReader(textBox1.Text + datePath + dateFileName);
                    var text1 = str.ReadToEnd();
                    txt(text1);
                    txt("Файл не пустой"); //TODO:                    
                } 
            }
            else
            {
                FileStream fst = new FileStream(settingsFileName, FileMode.Create);
                txt("Создаю settings");
                isCreated = true;
                fst.Close();
            }
        }

        void button1_Click(object sender, EventArgs e)
        {
            using (CommonOpenFileDialog ofd = new CommonOpenFileDialog())
            {
                ofd.InitialDirectory = "";
                ofd.IsFolderPicker = true;

                if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    textBox1.Text = ofd.FileName;
                    fullPath = ofd.FileName;
                    textBox1.Enabled = true;
                    if (File.Exists(textBox1.Text + dop + fileName))
                    {
                        fileExists = true;
                        textBox2.Enabled = true;
                        button2.Enabled = true;
                        checkBox1.Enabled = true;
                        txt("Файл 1client.dll есть");
                    }
                    else
                    {
                        txt("Файла client.dll нет");
                    }
                }
            }
        }

        void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txt("Записываю");
                StreamWriter sw = new StreamWriter(settingsFileName);

                sw.WriteLineAsync("Path = " + textBox1.Text);
                sw.WriteAsync("Distance = " + textBox2.Text);
                sw.Close();
                isWrited = true;
            }
            else if(isWrited == true)
            {
                txt("уже записан");
            }
            else
            {
                txt("Не Записываю");
            }
        }

        static void txt(String s)
        {
            Console.WriteLine(s);           
        }
    }
}