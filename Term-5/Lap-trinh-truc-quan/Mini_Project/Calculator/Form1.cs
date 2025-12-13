using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private Calculator calculator = new Calculator();
        private double lastFirstOperand;

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button; 
            if (btn != null)
            {
                textBox1.Text += btn.Text; 
            }
        }
        private void SetOperation(string op)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            double number = double.Parse(textBox1.Text);
            calculator.SetFirstOperand(number);
            calculator.SetOperation(op);

            lastFirstOperand = number;
            textBox2.Text = $"{number} {op}";
            textBox1.Clear();
        }

        private void btnPlus_Click(object sender, EventArgs e) => SetOperation("+");
        private void btnMinus_Click(object sender, EventArgs e) => SetOperation("-");
        private void btnMultiply_Click(object sender, EventArgs e) => SetOperation("*");
        private void btnDivide_Click(object sender, EventArgs e) => SetOperation("/");

        private void btnEquals_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text)) return;

            double secondInput = double.Parse(textBox1.Text);
            double result = calculator.Calculate(secondInput);

            textBox2.Text = $"{lastFirstOperand} {calculator.GetOperation()} {secondInput} = {result}";
            textBox1.Clear();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            calculator.Clear();
            textBox1.Clear();
            textBox2.Clear();
        }
    }
}
