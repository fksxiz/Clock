using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            watches1.StartClock();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            watches1.StopClock();
        }

        private void watches1_DigitChange(object sender, EventArgs e)
        {
            listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add("\r\n Смена разряда"); });
        }

        private void watches1_Alarm(object sender, EventArgs e)
        {
            MessageBox.Show("Будильник","Внимание");
            listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add("\r\n Будильник"); });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int hh = int.Parse(textBox1.Text);
            int mm = int.Parse(textBox2.Text);
            watches1.mm = mm;
            watches1.hh = hh;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int hh = int.Parse(textBox1.Text);
            int mm = int.Parse(textBox2.Text);
            watches1.Hours = hh;
            watches1.Minutes = mm;
            watches1.ALARMMM();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            watches1.SuccessivelyDigitChange();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            watches1.State();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
