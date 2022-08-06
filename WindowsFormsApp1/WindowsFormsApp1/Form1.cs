using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }       

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e) //Закрыть приложение
        {
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //Свернуть приложение
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {            
            pictureBox3.Enabled = true;
            if(File.Exists(clientPath))
            {
                Process.Start(clientPath);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("dota2"))
            {
                process.Kill();
            }
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        protected override void WndProc(ref Message m) // Перетаскивание окна без рамки
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if ((int)m.Result == HTCLIENT)
                    {
                        m.Result = (IntPtr)HTCAPTION;
                        return;
                    }
                    break;
            }
            base.WndProc(ref m);
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

        bool isCreated, isDllExists, isReaded = false;
        public bool isWrited = false;
        string dop = @"\steamapps\common\dota 2 beta\game\dota\bin\win64";
        string fileName = @"\client.dll";
        public string settingsFileName = "settings.properties";
        string infoPath = @"\steamapps\common\dota 2 beta\game\dota";
        string infoFileName = @"\steam.inf";
        string clientPath = @"\steamapps\common\dota 2 beta\game\bin\win64\dota2.exe";
        public string fullPath;
        public string oldParam;        
        char[] ch = { '\n', '=' };
        bool isRuLang, isEnLang;        

        void Form1_Load(object sender, EventArgs e)
        {
            notifyIcon1.BalloonTipTitle = "Cum Editor";

            if (File.Exists(settingsFileName))
            {
                txt("Открываю settings");
                StreamReader sr = new StreamReader(settingsFileName); // читаем файл с помощью делиметра после открытия
                var text = sr.ReadToEnd();

                if (text.Equals("")) // Если файл настроек пустой то ничего пока не делаем 
                {
                    button3.PerformClick();
                    isRuLang = true;
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
                        txt($"Сейчас записан {t}");
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
                    oldParam = textBox2.Text;
                }
                sr.Close();                
            }
            else
            {
                FileStream fst = new FileStream(settingsFileName, FileMode.Create);
                txt("Создаю settings");
                isCreated = true;                
                fst.Close();
                button3.PerformClick();
            }
        }

        private void textBox2_MouseHover(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            if (isEnLang)
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
            notifyIcon1.BalloonTipText = "Приложение свернуто";
            closeToolStripMenuItem.Text = "Выход";
            button1.Text = "Выбрать";
            button2.Text = "Применить";
            checkBox1.Text = "Сохранить настройки";
            label1.Text = "Готово";
            label2.Text = "Информация о клиенте:";
            if (isReaded || isWrited)
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
            notifyIcon1.BalloonTipText = "App minimized";
            closeToolStripMenuItem.Text = "Exit";
            button1.Text = "Select";
            button2.Text = "Apply";
            checkBox1.Text = "Save Settings";
            label1.Text = "Ready";
            label2.Text = "Dota Client Info:";
            if (isReaded || isWrited)
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
                    fullPath = textBox1.Text + dop + fileName;
                    textBox1.Enabled = true;
                    if (File.Exists(fullPath))
                    {
                        isDllExists = true;
                        textBox2.Enabled = true;
                        button2.Enabled = true;
                        checkBox1.Enabled = true;

                        StreamReader str = new StreamReader(textBox1.Text + infoPath + infoFileName);
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
                
                var text1 = label3.Text.Split(ch);
                var text2 = label4.Text.Split(ch);  
                var text3 = label5.Text.Split(ch);  
                var text4 = label6.Text.Split(ch);
                var index = 1;

                sw.WriteLine("Path = " + textBox1.Text);
                sw.WriteLine("Distance = " + textBox2.Text);
                sw.WriteLine("ClientVersion = " + text1[index].Trim());
                sw.WriteLine("SourceRevision = " + text2[index].Trim());
                sw.WriteLine("VersionDate = " + text3[index].Trim());
                sw.WriteLine("VersionTime = " + text4[index].Trim());
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
                new Parse(this).pars();
                
            }      
            else
            {
                txt("Меняю");
                isWrited = false;
                new Parse(this).pars();
            }
        }      

        static void txt(String s)
        {
            Console.WriteLine(s);           
        }        
    }
}