using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kurs3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            if (textBox1.Text == "yaroslav" && textBox2.Text == "12345")
            {
                f.Show();
                Hide();
            }
            else
            {
                if (textBox1.Text == "pi" && textBox2.Text == "12")
                {
                    f.Show();
                    Hide();
                }
                else

                {
                    if (textBox1.Text == "y" && textBox2.Text == "1")
                    {
                        f.Show();
                        Hide();
                    }
                    else MessageBox.Show("Вы ввели неверный логин или пароль");
                }
            }
            
        }
       
        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
