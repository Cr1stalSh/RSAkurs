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
using System.Diagnostics;
using System.Numerics;

namespace kurs3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        long ee, n, d;
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length > 0) && (textBox2.Text.Length > 0))
            {
                long p = Convert.ToInt64(textBox1.Text);
                long q = Convert.ToInt64(textBox2.Text);

                if (IsSimple(p) == true && IsSimple(q) == true)
                {
                    n = p * q;
                    long fn = (p - 1) * (q - 1);
                    ee = Calculate_e(fn);
                    d = Calculate_d(ee, fn);

                    listBox1.Items.Add("e = " + ee);
                    listBox1.Items.Add("n = " + n);
                    listBox1.Items.Add("d = " + d);

                }
                else
                    MessageBox.Show("p или q - не простые числа!");
            }
            else
                MessageBox.Show("Введите p и q!");
        }
        static bool IsSimple(long p)
        {
            if (p < 2)
                return false;
            if (p == 2)
                return true;
            for (long i = 2; i < p; i++)
                if (p % i == 0)
                    return false;
            return true;
        }
        static long Calculate_d(long e, long fn)
        {
            long d = 10;

            while (true)
            {
                if ((e * d) % fn == 1)
                    break;
                else
                    d++;
            }

            return d;
        }
        static long Calculate_e(long m)
        {
            Random rand = new Random();
            long e = rand.Next(1, (int)m);
            for (long i = 2; i <= m; i++)

                if ((m % i == 0) && (e % i == 0))
                {
                    e++;
                    i = 1;
                }

            return e;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox3.Text.Length > 0) && (textBox4.Text.Length > 0))
                {
                    long h = Convert.ToInt64(textBox4.Text);
                    long z = Convert.ToInt64(textBox3.Text);
                    string s = "";
                    StreamReader sr = new StreamReader("in.txt");
                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }
                    sr.Close();
                    s = s.ToUpper();
                    List<string> result = RSA_Endoce(s, h, z);
                    StreamWriter sw = new StreamWriter("out.txt");
                    foreach (string item in result)
                        sw.WriteLine(item);
                    sw.Close();
                    Process.Start("out.txt");
                }
                else
                    MessageBox.Show("Введите открытый ключ!");
            }
            catch
            {
                MessageBox.Show("Вы ввели неверные числа");
            }


        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox5.Text.Length > 0) && (textBox6.Text.Length > 0))
                {
                    long d = Convert.ToInt64(textBox5.Text);
                    long n = Convert.ToInt64(textBox6.Text);

                    List<string> input = new List<string>();

                    StreamReader sr = new StreamReader("out.txt");

                    while (!sr.EndOfStream)
                    {
                        input.Add(sr.ReadLine());
                    }

                    sr.Close();

                    string result = RSA_Dedoce(input, d, n);

                    StreamWriter sw = new StreamWriter("outt.txt");
                    sw.WriteLine(result);
                    sw.Close();

                    Process.Start("outt.txt");
                }
                else
                    MessageBox.Show("Введите секретный ключ!");
            }
            catch
            {
                MessageBox.Show("Вы ввели неверные числа");
            }
        }
        static List<string> RSA_Endoce(string s, long e, long n)
        {
            char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };

            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }
        static string RSA_Dedoce(List<string> input, long d, long n)
        {
            char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
                result = result.ToLower();
            }

            return result;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;
                StreamWriter file = new StreamWriter(File.Create(Path.Combine(path, "key.txt")));
                file.WriteLine("e = " + ee);
                file.WriteLine("n = " + n);
                file.WriteLine("d = " + d);
                file.Close();
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Process.Start("in.txt");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Process.Start("out.txt");
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Process.Start("outt.txt");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
