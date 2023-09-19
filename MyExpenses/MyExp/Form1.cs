using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace MyExp
{
    public partial class Form1 : Form
    {
        App app; // Объект приложения;
        Dictionary<int, string> mnt = new Dictionary<int, string>(){
            { 1, "January" },
            { 2, "February" },
            { 3, "March" },
            { 4, "April" },
            { 5, "May" },
            { 6, "June" },
            { 7, "July" },
            { 8, "August" },
            { 9, "September" },
            { 10, "October" },
            { 11, "November" },
            { 12, "December" }
        };

        public Form1()
        {
            InitializeComponent();
            app = new App();
            app.SplitForMonth();


            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "MMMM";

            //chart1.Series[0].XValueType = ChartValueType.DateTime;

            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Maximum = 12;

            //chart1.ChartAreas[0].AxisY.Minimum = -10;
            //chart1.ChartAreas[0].AxisY.Maximum = 10;

            chart1.Series[0].Points.Clear();

            
            for(int i = 0; i < app.monthsXp.Count(); i++)
            {
                // Расходы;
                DataPoint p1 = new DataPoint();
                p1.AxisLabel = mnt[app.monthsMon[i]];

                p1.XValue = i;
                p1.YValues = new double[] { Convert.ToDouble(Math.Abs(app.monthsXp[i])) };

                chart1.Series[0].Points.Add(p1);

                // Доходы;
                p1 = new DataPoint();
                p1.AxisLabel = mnt[app.monthsMon[i]];

                p1.XValue = i; 
                p1.YValues = new double[] { Convert.ToDouble(Math.Abs(app.monthsInc[i])) };

                chart1.Series[1].Points.Add(p1);
            }

        }

        private void button1_Click(object sender, EventArgs e) // Расход
        {
            string s = textBox2.Text.Trim(' '); //Получаем данные из поля суммы;
            if (int.TryParse(s, out _))
            {
                textBox1.Text = ""; //Очищаем текстбокс для вывода;
                textBox2.Text = ""; //Очищаем поле вводы суммы;
                decimal d = Convert.ToDecimal(s);
                if (d > 0)
                    d *= -1;

                if (!app.AddHis(d))
                    MessageBox.Show("Недостаточно средств");

                app.GetHistory(textBox1); //Выводим новую историю та textbox;
                app.AppSave();
                label2.Text = "Баланс: " + app.CheckBalance() + " RUB\r\n";

            }
            else MessageBox.Show("Не является числом");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            string[] datetime = Convert.ToString(localDate).Split(' ');

            textBox1.Text += "Последнее обновление: " + app.lastDate + "\r\n\r\n";
            app.GetHistory(textBox1);
            //app.GetMonths(textBox1);

            label2.Text = "Баланс: " + app.CheckBalance() + " RUB\r\n";
        }

        private void button2_Click(object sender, EventArgs e) // Доход
        {
            string s = textBox2.Text.Trim(' '); //Получаем данные из поля суммы;
            if (int.TryParse(s, out _))
            {
                textBox1.Text = ""; //Очищаем текстбокс для вывода;
                textBox2.Text = ""; //Очищаем поле вводы суммы;
                decimal d = Convert.ToDecimal(s);
                if (d < 0)
                    d *= -1;

                app.AddHis(d);
                app.GetHistory(textBox1); //Выводим новую историю та textbox;
                app.AppSave();

                label2.Text = "Баланс: " + app.CheckBalance() + " RUB\r\n";
            }
            else MessageBox.Show("Не является числом");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            chart1.Visible = !chart1.Visible;

            if (chart1.Visible)
            { 
                button3.Text = "История";
            }
            else
            {
                button3.Text = "Графики";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            app.AppSave();
            Application.Exit();
        }
    }
}
