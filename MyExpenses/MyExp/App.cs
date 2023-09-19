using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;

namespace MyExp
{
    public class App
    {
        public string lastDate { get; } //Дата последнего обновления приложения (вход, пополнение, расход и т.д.);
        string currDate;        //Текущая дата;
        List<string> history;   //История операция, представляют из себя строки (без минуса: пополнение: 300, с минусом: расход: -300);
                                //Такие строки легко приводить к числу и работать с ним;
        List<string> months;    //Строки с месяцами, название, пополнение, расход;
       
        public List<int> monthsMon { get; private set; }    // Номер месяца (1,2,3...);
        public List<decimal> monthsInc { get; private set; }   // Доходы;
        public List<decimal> monthsXp { get; private set; }   // Расходы;

        public App() 
        {
            //При инициализации будет происходить открытие или создание файла в первый раз с данными;
            //Считываться данные в соответствующие поля;

            if (!File.Exists("data.txt"))   //Проверка на существование файла;
            {
                                            //Файл не существует, первый старт приложения;
                StreamWriter sw = new StreamWriter("data.txt"); //Создаем файл;
                sw.WriteLine("-----"); //По этим "отметкам" будем делить файл на части;
                sw.WriteLine("-----");
                sw.Close();

                DateTime dt = DateTime.Now; //Получаем текущее время;
                string[] date = dt.ToString().Split(' ');

                this.lastDate = date[0];
                this.currDate = date[0];
                this.history = new List<string>();
                this.months = new List<string>();
            }
            else
            {
                //Файл существует;
                StreamReader sr = new StreamReader("data.txt");
                List<string> allstr = new List<string>();
                while (!sr.EndOfStream)
                {
                    allstr.Add(sr.ReadLine());
                }
                sr.Close();

                DateTime dt = DateTime.Now; //Получаем текущее время;
                string[] date = dt.ToString().Split(' ');

                this.history = new List<string>();
                this.months = new List<string>();

                this.lastDate = allstr[0].Split(':')[1].Trim();
                this.currDate = date[0];

                if (allstr.Count > 3) //Если есть история какая-то;
                {
                    List<int> indexs = new List<int>();
                    for (int i = 0; i < allstr.Count; i++) //Находим индексы разделителей;
                        if (allstr[i] == "-----")
                            indexs.Add(i);

                    if(indexs[1] < allstr.Count()-1) //Заполняем массивы;
                    {
                        for (int i = indexs[0] + 1; i < indexs[1]; i++)
                            this.months.Add(allstr[i]);
                                
                        for (int i = indexs[1] + 1; i < allstr.Count() ; i++)
                            this.history.Add(allstr[i]);

                        SplitForMonth();
                    }
                }
            }
        }

        public void SplitForMonth() // Вот метод который делит;
        {
            this.monthsMon = new List<int>();
            this.monthsInc = new List<decimal>();
            this.monthsXp = new List<decimal>();

            for (int i = 0; i < this.months.Count; i++)
                if (this.months[i] != "" && this.months[i] != " ")
                {
                    string[] temp = this.months[i].Split(':');
                    this.monthsMon.Add(Convert.ToInt32(temp[0]));
                    string[] summs = temp[1].Split(';') ;
                    this.monthsInc.Add(Convert.ToDecimal(summs[0]));
                    this.monthsXp.Add(Convert.ToDecimal(summs[1]));
                }
        }

        public void DrowGraphic(PictureBox picture1, bool Inc)
        {
            Graphics graphics = picture1.CreateGraphics();
            Pen pen = new Pen(Color.Black, 3f);

            Point[] points = new Point[1000];
            if (Inc)
            {
                for (int i = 0; i < monthsInc.Count; i++)
                {
                    points[i] = new Point(monthsMon[i], (int)monthsInc[i]);
                }
            }
            else
            {
                for (int i = 0; i < monthsXp.Count; i++)
                {
                    points[i] = new Point(monthsMon[i], (int)monthsXp[i]);
                }
            }
            graphics.DrawLines(pen, points);
        }

        public void GetHistory(TextBox t) //Метод для получения истории; В переданный textBox все выведет;
        {
            string rez = "История:\r\n";
            for (int i = 0; i < this.history.Count; i++)
                rez += this.history[i].ToString() + "\r\n";
            t.Text += rez;
            t.AppendText(" ");
        }

        public void GetMonths(TextBox t) //Метод для получения истории по месяцам; В переданный textBox все выведет;
        {
            string rez = "История по месяцам:\r\n";
            for (int i = 0; i < this.months.Count; i++)
                rez += this.months[i].ToString() + "\r\n";
            t.Text += rez;
            t.AppendText(" ");
        }

        public bool AddHis(decimal sum) //Метод для добавления операции в историю
        {
            //Тип decimal - с ним удобно работать когда речь идет о деньгах, если я конечно не ошибаюсь ;D
            // Для пополнения - передавать число без знака, пример: 3000.00
            // Для расхода - с минусом: -200.50
            if(CheckBalance() + sum >= 0)
            {
                this.history.Add(Convert.ToString(sum));
                return true;
            }
            return false;
        }

        public void AppSave() //Сохранение всей информации из полей класса в файл;
        {
            StreamWriter sw = new StreamWriter("data.txt");

            sw.WriteLine("lastupdate: " + this.currDate);
            sw.WriteLine("-----");
            for (int i = 0; i < this.months.Count; i++)
                sw.WriteLine(this.months[i]);

            sw.WriteLine("-----");
            for (int i = 0; i < this.history.Count; i++)
                sw.WriteLine(this.history[i]);

            sw.Close();
        }

        public decimal CheckBalance() // Подсчет текущего баланса
        {
            if(this.history.Count != 0)
            {
                decimal balance = 0;
                for (int i = 0; i < this.history.Count; i++)
                    if (int.TryParse(this.history[i], out _)) // Проверка на то, что именно число;
                        balance += Convert.ToDecimal(this.history[i]);
                return balance;
            }  
            return 0;
        }

    }
}
