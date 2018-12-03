using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text += "1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";

        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
        }
        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
        }
        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "^";
        }
        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "×";
        }
        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "÷";
        }

        //清零
        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        //backspace
        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text += "(";

        }

        private void button19_Click(object sender, EventArgs e)
        {
            textBox1.Text += ")";
        }


        //等于号"="，按下计算结果
        private void button20_Click(object sender, EventArgs e)
        {
            Calculate calculate = new Calculate();
            //console.WriteLine("textBox1.Text" + textBox1.Text);
            textBox2.Text = calculate.getResult(textBox1.Text) + "";
        }
    }
}
