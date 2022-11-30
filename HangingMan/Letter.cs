using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HangingMan
{
    class Letter : PictureBox
    {
        static string path = "";
        static int Count = 0;
        char ch = ' ';
        public bool allreadyShown = false;
        Timer animater = new Timer();
        Timer disAppearer = new Timer();
        int capa = -10;
        int fullWidth = 0;
        static string temp = "";
        bool alreadyPassed = false;
        System.Drawing.Image curr = null;
        Timer an = new Timer();
        public Letter() {
            an.Tick += new EventHandler(an_Tick);
            this.ch = '_';
            an.Interval = 400;
            Count++;
            temp = Directory.GetCurrentDirectory();
            path = temp.Substring(0, temp.Length - 9);
            temp1 = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(path + @"Resources\LettCont.png")));
            temp2 = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(path + @"Resources\LettCont_.png")));
            this.Image = temp1;
            this.Location = new System.Drawing.Point(51, 8);
            this.Name = "pictureBox" + Count;
            this.Size = new System.Drawing.Size(100, 100);
            this.fullWidth = 100;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TabIndex = 2;
            this.TabStop = false;
            an.Start();
        }
        bool isFull = false;
        void an_Tick(object sender, EventArgs e)
        {
            if (isFull == false)
            {
                this.Image = temp2;
                isFull = true;
            }
            else {
                this.Image = temp1;
                isFull = false;
            }
        }
        System.Drawing.Image temp1;
        System.Drawing.Image temp2;
        public Letter(char letter) {
            animater.Interval = 1;
            animater.Tick += new EventHandler(animater_Tick);
            disAppearer.Interval = 1;
            disAppearer.Tick += new EventHandler(disAppearer_Tick);
            this.ch = letter;
            Count++;
            this.Image = System.Drawing.Image.FromStream(new MemoryStream(File.ReadAllBytes(path + @"Resources\LettCont.png")));
            this.Location = new System.Drawing.Point(51, 8);
            this.Name = "pictureBox"+Count;
            this.Size = new System.Drawing.Size(100, 100);
            this.fullWidth = 100;
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TabIndex = 2;
            this.TabStop = false;
        }
        bool reverse = false;
        void disAppearer_Tick(object sender, EventArgs e){
            if (this.reverse == false)
            {
                this.setLocalWidth(this.Size.Width - 20);
            }
            else {
                this.setLocalWidth(this.Size.Width + 20);
            }
            if (reverse == false)
            {
                if (targerPanel != null)
                {
                    if (this.Size.Width == 0)
                    {
                        currPanel.Controls.Remove(this);
                        reverse = true;
                        targerPanel.Controls.Add(this);
                    }
                }
                else {
                    if (this.Size.Width == 0)
                    {
                        currPanel.Controls.Remove(this);
                        disAppearer.Stop();
                    }
                }
            }
            else {
                if (this.Size.Width >= this.fullWidth) {
                    disAppearer.Stop();
                    this.setLocalWidth(this.fullWidth);
                    reverse = false;
                }
            }
        }
        FlowLayoutPanel currPanel = null;
        FlowLayoutPanel targerPanel = null;
        public void animateDisAppear(ref FlowLayoutPanel f,ref FlowLayoutPanel tar) {
            currPanel = f;
            targerPanel = tar;
            disAppearer.Start();
        }
        public void animateDisAppear(ref FlowLayoutPanel f)
        {
            currPanel = f;
            disAppearer.Start();
        }
        public Char getLetter() {
            return this.ch;
        }
        public void setSize(System.Drawing.Size s) {
            this.Size = s;
            this.fullWidth = s.Width;
        }
        private void setLocalWidth(int w) {
            this.Size = new System.Drawing.Size(w, this.Size.Height);
        }
        
        //Animates the setLet()
        void animater_Tick(object sender, EventArgs e)
        {
            this.setLocalWidth(this.Size.Width+capa);
            if (this.Size.Width == 0) {
                this.Image = curr;
                capa = 10;
                alreadyPassed = true;
            }
            if (alreadyPassed) {
                if (this.Size.Width >= this.fullWidth) {
                    this.setLocalWidth(fullWidth);
                    alreadyPassed = false;
                    animater.Stop();
                }
            }
        }
        //Showing the letter in the image
        public void setLet() {
            if (allreadyShown == false)
            {
                allreadyShown = true;
                MemoryStream ms = null;
                if ((ch >= 'A' && ch <= 'Z') || ch == '-')
                {
                    ms = new MemoryStream(File.ReadAllBytes(path+@"Resources\LettCont" + ch + ".png"));
                }
                else
                {
                    ms = new MemoryStream(File.ReadAllBytes(path+@"Resources\LettContH" + ch + ".png"));
                }
                this.curr = System.Drawing.Image.FromStream(ms);
                animater.Start();
            }
        }
    }
}
