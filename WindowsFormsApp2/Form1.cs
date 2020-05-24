using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1_Click(null, null);
            timer1.Interval = 2000;
            timer1.Enabled = true;
        }

        int num_one = 0;
        int num_two = 0;
        string operation;
        int result = 0;
        Random rnd = new Random();

        private void label1_Click(object sender, EventArgs e)
        {
            // label1.Text = "HelloWorld";
            return;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label2.Text = "";
            return;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            num_one = rnd.Next(9) + 1;
            num_two = rnd.Next(9) + 1;
            int t = rnd.Next(4);
            switch (t)
            {
                case 0: operation = "+"; result = num_one + num_two; break;
                case 1: operation = "-"; result = num_one - num_two; break;
                case 2: operation = "*"; result = num_one * num_two; break;
                case 3: operation = "/"; result = num_one / num_two; break;
            }
            label1.Text = num_one.ToString() + operation + num_two.ToString() + "=";
        }
        public void button2_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            double user_result = 0;
            if(input=="")
            {
                label2.Text = "Wrong!";
                return;
            }
            else
            {
                try
                {
                    user_result = double.Parse(input);
                }
                catch(Exception ee)
                {
                    label2.Text = ee.ToString();
                    return;
                }
            }
            if(user_result == result)
            {
                label2.Text = "Right!";
            }
            else
            {
                label2.Text = "Wrong!";
            }
            return;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            button1_Click(null, null);
            timer1.Enabled = true;
        }
    }
}
