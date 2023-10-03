using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace MyExps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FileStorage fs = new FileStorage();
            Storage st = new Storage();
            User user = new User("Pidrosovich","AdolfHitler");
            fs.AddFile(user);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Access access = new Access();
            access.Registration(textBox1.Text, textBox2.Text);
            if (!access.acc)
                MessageBox.Show("Пользователь с таким логином уже опеределен");
            else
            {
                MessageBox.Show("Вы зарегистрированы");
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Access access = new Access();
            access.LogIn(textBox1.Text, textBox2.Text);
            if (!access.acc)
                MessageBox.Show("Неправильно набран логин или пароль");
            else
            {
                MessageBox.Show("Вы авторизованы");

            }
        }
    }
}
