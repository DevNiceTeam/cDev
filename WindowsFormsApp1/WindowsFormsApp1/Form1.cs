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

        bool isCreated, isDllExists, isWrited , isReaded = false;
        string dop = @"\steamapps\common\dota 2 beta\game\dota\bin\win64";
        string fileName = @"\client.dll";
        static string settingsFileName = "settings.properties";
        string datePath = @"\steamapps\common\dota 2 beta\game\dota";
        string dateFileName = @"\steam.inf";    
        string fullPath;
        char[] ch = { '\n', '=' };
        bool isRuLang, isEnLang;


        ToolTip t = new ToolTip();

       

        void Form1_Load(object sender, EventArgs e)
        {            
            if (File.Exists(settingsFileName))
            { 
                txt("Открываю settings");
                StreamReader sr = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
                var text = sr.ReadToEnd();   

                if (text.Equals("")) // Если файл настроек пустой то ничего пока не делаем 
                {
                    button3.PerformClick();
                }
                else // Если файл настроек не пустой то cчитываем файл выбираем путь и записываем его в TextBox 
                {
                    var line = text.Split(ch);
                    string t = line[13].Trim();
                   
                    if (t.Equals("RU"))
                    {
                        button3.PerformClick();
                        textBox1.Text = line[1].Trim();
                        textBox2.Text = line[3].Trim();
                        txt($"Сейчас записан {t}") ;
                        isRuLang = true;
                        isEnLang = false;
                        label3.Text = "Версия клиента = " + line[5].Trim();
                        label4.Text = "Версия исх.кода = " + line[7].Trim();
                        label5.Text = "Дата релиза = " + line[9].Trim();
                        label6.Text = "Время релиза = " + line[11].Trim();
                    }    
                    else
                    {
                        button4.PerformClick();
                        textBox1.Text = line[1].Trim();
                        textBox2.Text = line[3].Trim();
                        txt($"Сейчас записан {t}");
                        isRuLang = false;
                        isEnLang = true;
                        label3.Text = "Client Version = " + line[5].Trim();
                        label4.Text = "Source Revision = " + line[7].Trim();
                        label5.Text = "Version Date = " + line[9].Trim();
                        label6.Text = "Version Time = " + line[11].Trim();
                    }                  
                    isReaded = true;                                       
                }
                sr.Close();
            }
            else
            {
                FileStream fst = new FileStream(settingsFileName, FileMode.Create);
                txt("Создаю settings");
                isCreated = true;
                button3.PerformClick();
                fst.Close();
           }
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            if(isEnLang)
            {
                t.SetToolTip(textBox2, @"Default range:1134
Recommended range:1550");
            }
            else
            {
                t.SetToolTip(textBox2, @"Стандартная дальность:1134
Рекомендуемая дальность:1550");
            }
           
        }

        
        /// <summary>
        /// Ru lang onBtnClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void button3_Click(object sender, EventArgs e)
        {
            isEnLang = false;
            isRuLang = true;
            StreamReader sr = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
            var line = sr.ReadToEnd().Split(ch);

            button1.Text = "Выбрать";
            button2.Text = "Применить";
            checkBox1.Text = "Сохранить настройки";            
            if (isReaded)
            {
                textBox1.Text = line[1].Trim();
                textBox2.Text = line[3].Trim();
                label3.Text = "Версия клиента = " + line[5].Trim();
                label4.Text = "Версия исх.кода = " + line[7].Trim();
                label5.Text = "Дата релиза = " + line[9].Trim();
                label6.Text = "Время релиза = " + line[11].Trim();
            }  
            else
            {
                textBox1.Text = "Путь к папке Steam";
                textBox2.Text = "Укажите дальность камеры";
                label3.Text = "Версия клиента = ";
                label4.Text = "Версия исх.кода = ";
                label5.Text = "Дата релиза = ";
                label6.Text = "Время релиза = ";
            }  
            sr.Close();
        }

        /// <summary>
        /// En lang onBtnClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            isRuLang = false;
            isEnLang = true;
            StreamReader sr = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
            var line = sr.ReadToEnd().Split(ch);
            button1.Text = "Select";
            button2.Text = "Apply";
            checkBox1.Text = "Save Settings";
            if(isReaded)
            {
                textBox1.Text = line[1].Trim();
                textBox2.Text = line[3].Trim();
                label3.Text = "Client Version = " + line[5].Trim();
                label4.Text = "Source Revision = " + line[7].Trim();
                label5.Text = "Version Date = " + line[9].Trim();
                label6.Text = "Version Time = " + line[11].Trim();
            }
            else
            {
                textBox1.Text = "Steam folder path";
                textBox2.Text = "Specify camera range";
                label3.Text = "Client Version = ";
                label4.Text = "Source Revision = ";
                label5.Text = "Version Date = ";
                label6.Text = "Version Time = ";
            }
            sr.Close();
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
                        isDllExists = true;
                        textBox2.Enabled = true;
                        button2.Enabled = true;
                        checkBox1.Enabled = true;

                        StreamReader str = new StreamReader(textBox1.Text + datePath + dateFileName);
                        var text1 = str.ReadToEnd().Split(ch);

                        if (isEnLang)
                        {
                            label3.Text = "Client Version = " + text1[1].Trim();
                            label4.Text = "Source Revision = " + text1[15].Trim();
                            label5.Text = "Version Date = " + text1[17].Trim();
                            label6.Text = "Version Time = " + text1[19].Trim();
                        }
                        else
                        {
                            label3.Text = "Версия клиента = " + text1[1].Trim();
                            label4.Text = "Версия исх.кода = " + text1[15].Trim();
                            label5.Text = "Дата релиза = " + text1[17].Trim();
                            label6.Text = "Время релиза = " + text1[19].Trim();
                        }
                        str.Close();                        



                        txt("Файл client.dll есть");
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
                string[] str = { label3.Text, label4.Text, label5.Text, label6.Text };
                foreach(string str2 in str)
                {
                    str2.Split(ch);
                    Console.WriteLine(str2);
                }
                sw.WriteLine("Path = " + textBox1.Text);
                sw.WriteLine("Distance = " + textBox2.Text);
                sw.WriteLine("Client Version = " + label3.Text.Split(ch).ToString().Trim());
                sw.WriteLine("Source Revision = " + label4.Text.Split(ch).ToString().Trim());
                sw.WriteLine("Version Date = " + label5.Text.Split(ch).ToString().Trim());
                sw.WriteLine("Version Time = " + label6.Text.Split(ch).ToString().Trim());
                if (isEnLang)
                {
                    sw.WriteLine("Language = EN");
                }
                else
                {
                    sw.WriteLine("Language = RU");
                }
                        
                sw.Close();
                isWrited = true;
            }            
        }

        static void txt(String s)
        {
            Console.WriteLine(s);           
        }
    }
}