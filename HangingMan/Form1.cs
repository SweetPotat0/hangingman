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
    enum stickManState {
        idle,randomize,mortal
    }
    public partial class Form1 : Form
    {
        int WrongChoices = 0;
        stickManState state = stickManState.idle;
        String word = "";
        Letter[] words = null;
        string path = "";
        String tempo = "";
        Letter[] letts = null;
        byte language = 0;//0 - E, 1 - H
        public Form1()
        {
            InitializeComponent();
        }
        public Form1(String Topic,String word) {
            //Initializes the components
            InitializeComponent();

            label3.Text = (int)(word.Length + 3)+"";

            tempo = Directory.GetCurrentDirectory();
            path = tempo.Substring(0, tempo.Length - 9);
            Bitmap b =(Bitmap) Image.FromFile(path + @"Resources\1.png");
            
            
            //Initializes the Letters array of the chosen word
            words = new Letter[word.Length];
            //Sets the listener key Handler for form1
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            //Initializes the values
            //The counter for the letters
            int count = 0;
            //The same counter but it gets zeroed every new line.
            int defCount = 0;
            pictureBox7.BringToFront();
            //if the word is in english it will bring the letters in english
            if (word[0] >= 'A' && word[0] <= 'Z')
            {
                language = 0;
                b.RotateFlip(RotateFlipType.Rotate180FlipY);
                string en = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                letts = new Letter[en.Length];
                for (int i = 0; i < en.Length; i++) {
                    count++;
                    defCount++;
                    letts[i] = new Letter(en[i]);
                    letts[i].setSize(new System.Drawing.Size(80, 80));
                    letts[i].Location = new System.Drawing.Point(51 * (defCount - 1), 8);
                    if (count == 18||count == 9) {
                        defCount = 0;
                    }
                    if (count >= 19)
                    {
                        flowLayoutPanel4.Controls.Add(letts[i]);
                    }
                    else if (count >= 10)
                    {
                        flowLayoutPanel3.Controls.Add(letts[i]);
                    }
                    else
                    {
                        flowLayoutPanel2.Controls.Add(letts[i]);
                    }
                    letts[i].setLet();
                }
            }
            //If the word is in hebrew it will bring the letters in hebrew
            else {
                language = 1;
                
                this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.RightToLeftLayout = true;
                string en = "אבגדהוזחטיכלמנסעפצקרשתךםןף";
                letts = new Letter[en.Length];
                for (int i = 0; i < en.Length; i++)
                {
                    count++;
                    defCount++;
                    letts[i] = new Letter(en[i]);
                    letts[i].setSize(new System.Drawing.Size(80, 80));
                    letts[i].Location = new System.Drawing.Point(51 + 51 * (defCount - 1), 8);
                    if (count == 18 || count == 9)
                    {
                        defCount = 0;
                    }
                    if (count >= 19)
                    {
                        flowLayoutPanel4.Controls.Add(letts[i]);
                    }
                    else if (count >= 10)
                    {
                        flowLayoutPanel3.Controls.Add(letts[i]);
                    }
                    else
                    {
                        flowLayoutPanel2.Controls.Add(letts[i]);
                    }
                    letts[i].setLet();
                }
            }
            label1.Text = Topic;
            this.word = word;
            for (int i = 0; i < word.Length; i++) {
                words[i] =  new Letter(word[i]);
                flowLayoutPanel1.Controls.Add(words[i]);
                if (words[i].getLetter() == '-') {
                    ((Letter)flowLayoutPanel1.Controls[i]).setLet();
                }
            }
            pictureBox2.Image = b;
        }
        bool flagForWin = true;
        bool flag;
        Letter temp;
        public void setStickState(int level) {
            Bitmap b = (Bitmap)Image.FromFile(path + @"Resources\"+level+".png");
            if (language == 0&&level!=3&&level!=6) {
                b.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
            pictureBox2.Image = b;
        }
        Random rnd;
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            if (flagForWin)
            {
                //if (e.KeyCode == Keys.Tab) {
                //  MessageBox.Show("Controls on layouts : lay1 - "+flowLayoutPanel1.Controls.Count+", lay2 - "+flowLayoutPanel2.Controls.Count+", lay3 - "+flowLayoutPanel3.Controls.Count+", lay4 - "+flowLayoutPanel4.Controls.Count+", lay5 - "+flowLayoutPanel5.Controls.Count+", lay5[0].width - "+flowLayoutPanel5.Controls[0].Size.Width);
                //}
                if (language == 0)
                {
                    flag = false;
                    temp = null;
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (e.KeyCode.ToString().Length == 1)
                        {
                            try
                            {
                                if (words[i].getLetter() == Char.Parse(e.KeyCode.ToString()))
                                {
                                    flag = true;
                                    words[i].setLet();
                                    temp = words[i];
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        else {

                        }
                    }
                    if (flag == true)
                    {
                        bool flag2 = false;
                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {
                            if (temp.getLetter() == ((Letter)flowLayoutPanel2.Controls[i]).getLetter())
                            {
                                ((Letter)flowLayoutPanel2.Controls[i]).animateDisAppear(ref flowLayoutPanel2);
                                flag2 = true;
                            }
                        }
                        if (flag2 == false)
                        {
                            for (int i = 0; i < flowLayoutPanel3.Controls.Count; i++)
                            {
                                if (temp.getLetter() == ((Letter)flowLayoutPanel3.Controls[i]).getLetter())
                                {
                                    ((Letter)flowLayoutPanel3.Controls[i]).animateDisAppear(ref flowLayoutPanel3);
                                    flag2 = true;
                                }
                            }
                            if (flag2 == false)
                            {
                                for (int i = 0; i < flowLayoutPanel4.Controls.Count; i++)
                                {
                                    if (temp.getLetter() == ((Letter)flowLayoutPanel4.Controls[i]).getLetter())
                                    {
                                        ((Letter)flowLayoutPanel4.Controls[i]).animateDisAppear(ref flowLayoutPanel4);
                                        flag2 = true;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        label3.Text = int.Parse(label3.Text) - 1 + "";
                        WrongChoices++;
                        if (state == stickManState.idle)
                        {
                            state = stickManState.randomize;
                            setStickState(2);
                        }
                        else if (state == stickManState.randomize)
                        {
                            if (WrongChoices > word.Length)
                            {
                                state = stickManState.mortal;
                            }
                            rnd = new Random();
                            setStickState(rnd.Next(3, 7));
                        }
                        else {
                            if (WrongChoices == word.Length + 2)
                            {
                                setStickState(7);
                            }
                            else {
                                setStickState(8);
                                flagForWin = false;
                            }
                        }
                        for (int i = 0; i < letts.Length; i++)
                        {
                            try
                            {
                                if (letts[i].getLetter() == Char.Parse(e.KeyCode.ToString()))
                                {
                                    if (flowLayoutPanel2.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel5.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel2, ref flowLayoutPanel5);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel2, ref flowLayoutPanel6);
                                        }
                                    else if (flowLayoutPanel3.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel5.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel3, ref flowLayoutPanel5);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel3, ref flowLayoutPanel6);
                                        }
                                    else if (flowLayoutPanel4.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel5.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel4, ref flowLayoutPanel5);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel4, ref flowLayoutPanel6);
                                        }
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
                else {
                    /*flag = false;
                    for (int i = 0; i < words.Length; i++) {
                        try {
                            if (words[i].getLetter() == FromEtoH(Char.Parse(e.KeyCode.ToString()))) {
                                words[i].setLet();
                            }
                        }
                        catch(Exception){

                        }
                    }
                    */
                    flag = false;
                    temp = null;
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (e.KeyCode.ToString().Length == 1)
                        {
                            try
                            {
                                if (words[i].getLetter() == FromEtoH(Char.Parse(e.KeyCode.ToString())))
                                {
                                    flag = true;
                                    words[i].setLet();
                                    temp = words[i];
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        else {

                        }
                    }
                    if (flag == true)
                    {
                        bool flag2 = false;
                        for (int i = 0; i < flowLayoutPanel2.Controls.Count; i++)
                        {
                            if (temp.getLetter() == ((Letter)flowLayoutPanel2.Controls[i]).getLetter())
                            {
                                ((Letter)flowLayoutPanel2.Controls[i]).animateDisAppear(ref flowLayoutPanel2);
                                flag2 = true;
                            }
                        }
                        if (flag2 == false)
                        {
                            for (int i = 0; i < flowLayoutPanel3.Controls.Count; i++)
                            {
                                if (temp.getLetter() == ((Letter)flowLayoutPanel3.Controls[i]).getLetter())
                                {
                                    ((Letter)flowLayoutPanel3.Controls[i]).animateDisAppear(ref flowLayoutPanel3);
                                    flag2 = true;
                                }
                            }
                            if (flag2 == false)
                            {
                                for (int i = 0; i < flowLayoutPanel4.Controls.Count; i++)
                                {
                                    if (temp.getLetter() == ((Letter)flowLayoutPanel4.Controls[i]).getLetter())
                                    {
                                        ((Letter)flowLayoutPanel4.Controls[i]).animateDisAppear(ref flowLayoutPanel4);
                                        flag2 = true;
                                    }
                                }
                            }
                        }
                    }
                    else {
                        label3.Text = int.Parse(label3.Text) - 1 + "";
                        WrongChoices++;
                        if (state == stickManState.idle)
                        {
                            state = stickManState.randomize;
                            setStickState(2);
                        }
                        else if (state == stickManState.randomize)
                        {
                            if (WrongChoices > word.Length)
                            {
                                state = stickManState.mortal;
                            }
                            rnd = new Random();
                            setStickState(rnd.Next(3, 7));
                        }
                        else {
                            if (WrongChoices == word.Length + 2)
                            {
                                setStickState(7);
                            }
                            else {
                                setStickState(8);
                                flagForWin = false;
                            }
                        }

                        for (int i = 0; i < letts.Length; i++)
                        {
                            try
                            {
                                if (letts[i].getLetter() == FromEtoH(Char.Parse(e.KeyCode.ToString())))
                                {
                                    if (flowLayoutPanel2.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel6.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel2, ref flowLayoutPanel6);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel2, ref flowLayoutPanel5);
                                        }
                                    else if (flowLayoutPanel3.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel6.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel3, ref flowLayoutPanel6);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel3, ref flowLayoutPanel5);
                                        }
                                    else if (flowLayoutPanel4.Controls.Contains(letts[i]))
                                        if (flowLayoutPanel6.Controls.Count < 6)
                                        {
                                            letts[i].animateDisAppear(ref flowLayoutPanel4, ref flowLayoutPanel6);
                                        }
                                        else {
                                            letts[i].animateDisAppear(ref flowLayoutPanel4, ref flowLayoutPanel5);
                                        }
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
                if (flagForWin)
                {
                    flagForWin = false;
                    for (int i = 0; i < words.Length; i++)
                    {
                        if (!words[i].allreadyShown)
                        {
                            flagForWin = true;
                        }
                    }
                    if (!flagForWin)
                    {
                        MessageBox.Show("You won!");
                        //Won
                        Control tempControl;
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            tempControl = this.Controls[i];
                            if (!tempControl.Equals(label2) && !tempControl.Equals(pictureBox7))
                            {
                                this.Controls.Remove(tempControl);
                            }
                        }
                        label2.Visible = true;
                        pictureBox7.Visible = true;
                        label2.BringToFront();
                    }
                }
            }
        }
        public static char FromEtoH(char E) {
            String h = "אבגדהוזחטיכלמנסעפצקרשתךםןף";
            String e = "TCDSVUZJYHFKNBXGPMERA,LOI;";
            return(h[e.IndexOf(E)]);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
