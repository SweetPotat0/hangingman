using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HangingMan
{
    public partial class Home : Form
    {
        String topic = "";
        String word = "";
        String hebSaysEn = "שפה נוכחית: אנגלית. לחץ TAB כדי לשנות.";
        String enSaysEn = "Current Language: English. Press TAB to change.";
        String hebSaysHeb = "שפה נוכחית: עברית. לחץ TAB כדי לשנות.";
        String enSaysHeb = ".Current Language: Hebrew. Press TAB to change";
        String enSaysTopic = "Choose your topic:";
        String hebSaysTopic = "בחר את נושא המילה:";
        String enchooseWord = "Choose your word:";
        String hebChooseWord = "בחר את המילה שלך:";
        byte lan = 0;//0 - English, 1 - Hebrew
        string temp = "";
        public Home()
        {
            InitializeComponent();
            temp = Directory.GetCurrentDirectory();
            flowLayoutPanel1.Controls.Add(new Letter());
            this.KeyDown += new KeyEventHandler(Home_KeyDown);
        }
        bool part2 = false;
        void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (flowLayoutPanel1.Controls.Count == 1)
                {
                    MessageBox.Show("You must enter something");
                }
                else {
                    if (part2 == false)
                    {
                        topic = "";
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++)
                        {
                            topic += ((Letter)flowLayoutPanel1.Controls[i]).getLetter();
                        }
                        if (lan == 0)
                        {
                            label1.Text = enchooseWord;
                        }
                        else {
                            label1.Text = hebChooseWord;
                        }

                        while (flowLayoutPanel1.Controls.Count != 1)
                        {
                            flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 2);
                        }
                        //Return the window to the center with its original size
                        flowLayoutPanel1.Size = new System.Drawing.Size(473, 119);
                        ClientSize = new System.Drawing.Size(511, 248);
                        part2 = true;
                    }
                    else
                    {
                        word = "";
                        for (int i = 0; i < flowLayoutPanel1.Controls.Count - 1; i++){
                            word += ((Letter)flowLayoutPanel1.Controls[i]).getLetter();
                        }
                        Form1 f = new Form1(topic,word);
                        f.ShowDialog();
                        this.Close();
                    }
                }
            }
            else if (e.KeyCode == Keys.Tab) {
                if (part2 == false)
                {
                    if (flowLayoutPanel1.Controls.Count == 1)
                    {
                        if (lan == 0)
                        {
                            label2.Text = enSaysHeb;
                            label3.Text = hebSaysHeb;
                            label1.Text = hebSaysTopic;
                            lan = 1;
                            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                            this.RightToLeftLayout = true;
                        }
                        else {
                            label2.Text = enSaysEn;
                            label3.Text = hebSaysEn;
                            label1.Text = enSaysTopic;
                            lan = 0;
                            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
                            this.RightToLeftLayout = false;
                        }
                    }
                    else
                    {
                        if (lan == 0)
                        {
                            MessageBox.Show("עליך למחוק את מה שרשמת לפני שתוכל לעבור שפה","עבירת שפה",MessageBoxButtons.OK,MessageBoxIcon.Stop,MessageBoxDefaultButton.Button1,MessageBoxOptions.RtlReading);
                        }
                        else {
                            MessageBox.Show("You must erase it all before changing language.","Change Launguage",MessageBoxButtons.OK,MessageBoxIcon.Stop,MessageBoxDefaultButton.Button1);
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                if (flowLayoutPanel1.Controls.Count != 1)
                {
                    flowLayoutPanel1.Controls.Remove(flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 2]);
                }
            }
            else
            {
                if (e.KeyCode.ToString().Length == 1)
                {
                    if (lan == 0)
                    {
                        if (e.KeyCode.ToString().ToLower()[0] >= 'a' && e.KeyCode.ToString().ToLower()[0] <= 'z')
                        {
                            flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                            flowLayoutPanel1.Controls.Add(new Letter(e.KeyCode.ToString()[0]));
                            ((Letter)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).setLet();
                            flowLayoutPanel1.Controls.Add(new Letter());
                        }
                    }
                    else
                    {
                        if (isHeb(e.KeyCode.ToString().ToLower()[0]))
                        {
                            flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                            flowLayoutPanel1.Controls.Add(new Letter(FromEtoH(e.KeyCode.ToString()[0])));
                            ((Letter)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).setLet();
                            flowLayoutPanel1.Controls.Add(new Letter());
                        }
                    }
                }
                else if (e.KeyCode.ToString().ToLower() == "oemminus")
                {
                    flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                    flowLayoutPanel1.Controls.Add(new Letter('-'));
                    ((Letter)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).setLet();
                    flowLayoutPanel1.Controls.Add(new Letter());
                }
                else if(e.KeyCode.ToString().ToLower()=="oemcomma"&&lan==1)
                {
                    flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count - 1);
                    flowLayoutPanel1.Controls.Add(new Letter('ת'));
                    ((Letter)flowLayoutPanel1.Controls[flowLayoutPanel1.Controls.Count - 1]).setLet();
                    flowLayoutPanel1.Controls.Add(new Letter());
                }
            }
        }
        public bool isHeb(char ch) {
            String e = "TCDSVUZJYHFKNBXGPMERA,OI;L";
            for (int i = 0; i < e.Length; i++)
                if (ch == e[i])
                    return false;
            return true;
        }
        public static char FromEtoH(char E)
        {
            String h = "אבגדהוזחטיכלמנסעפצקרשתםןףך";
            String e = "TCDSVUZJYHFKNBXGPMERA,OI;L";
            return (h[e.IndexOf((E+"").ToUpper())]);
        }
    }
}
