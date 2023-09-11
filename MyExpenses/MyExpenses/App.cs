using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace MyExpenses
{
    public class App
    {
        string lastDate;        //Дата последнего обновления приложения (вход, пополнение, расход и т.д.);
        string currDate;        //Текущая дата;
        List<string> history;   //История операция, представляют из себя строки (без минуса: пополнение: 300, с минусом: расход: -300);
                                //Такие строки легко приводить к числу и работать с ним;
        List<string> months;    //Строки с месяцами, название, пополнение, расход;
        decimal balance;

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
                this.lastDate = allstr[0].Split(":")[1].Trim();
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
                    }
                }
            }
        }

        public void GetHistory(TextBox t) //Метод для получения истории; В переданный textBox все выведет;
        {
            t.Text += this.lastDate + "\r\n";
            t.Text += "История: " + "\r\n";
            for (int i = 0; i < this.history.Count; i++)
                t.Text += history[i] + "\r\n";
            t.Text += "\r\n";
        }

        public void GetMonths(TextBox t) //Метод для получения истории по месяцам; В переданный textBox все выведет;
        {
            t.Text += "История по месяцам: " + "\r\n";
            for (int i = 0; i < this.months.Count; i++)
                t.Text += months[i] + "\r\n";
            t.Text += "\r\n";
        }

        public void AddHis(decimal sum) //Метод для добавления операции в историю
        {
            //Тип decimal - с ним удобно работать когда речь идет о деньгах, если я конечно не ошибаюсь ;D
            // Для пополнения - передавать число без знака, пример: 3000.00
            // Для расхода - с минусом: -200.50
            this.history.Add(Convert.ToString(sum));
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

        public void CheckBalance() // Подсчет текущего баланса
        {
            for (int i = 0; i < this.history.Count; i++)
                balance += Convert.ToDecimal(this.history[i]);     
        }

    }
}
