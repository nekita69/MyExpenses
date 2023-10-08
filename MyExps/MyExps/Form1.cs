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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyExps
{
    public partial class Form1 : Form
    {
        App app;
        public Form1()
        {
            InitializeComponent();
        }

        // All Objects;
        Form2 form2;
        User user;
        Storage st;
        Access access;

        public void SendDataForm1(User user)
        {
            this.user = user;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            FileStorage fs = new FileStorage();
            Storage st = new Storage();
            User user = new User("Pidrosovich","AdolfHitler");
            fs.AddFile(user);
            
=======
            form2 = new Form2();
            st = new Storage();
            access = new Access(st);
>>>>>>> nikita
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != ""){ //тут проверку, что не пустые строки переданы;

                if (radioButton1.Checked)
                { //Авторизация;

                    user = access.LogIn(textBox1.Text, textBox2.Text);
                    if (user == null)
                    {
                        MessageBox.Show("Неправильный логин или пароль");
                    }
                    else
                    {
                        //Передаем на другую форму; 
                        form2.SendDataForm2(user, this, st);
                        form2.Show();
                        this.Hide();

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }

                }
                else if (radioButton2.Checked)
                { //Регистрация;
                    user = new User(textBox1.Text, textBox2.Text);
                    if (access.Registration(user)) //Регистрация прошла
                    {
                        //Передаем на другую форму; 
                        form2.SendDataForm2(user, this, st);
                        form2.Show();
                        this.Hide();

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else //Не прошла
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Действие?");
                }

            }
            else { MessageBox.Show("Заполните все поля"); }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Access access = new Access();
            access.LogIn(textBox1.Text, textBox2.Text);
            if (!access.acc)
                MessageBox.Show("Неправильно набран логин или пароль");
            else
            {
                MessageBox.Show("Вы авторизованы");

            }
=======
            app = new App();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Reg";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Log";
>>>>>>> f5b10fb60a8ae6edc4debd44962cbc1cee417456
=======
            textBox2.UseSystemPasswordChar = !textBox2.UseSystemPasswordChar;
        }
         
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) //При закрытии формы все сохраняем и все закрываем;
        {
            Application.Exit();
>>>>>>> nikita
        }
    }
}
