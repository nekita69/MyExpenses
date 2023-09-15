namespace MyExpenses
{
    public partial class Form1 : Form
    {
        App app; // Объект приложения;

        public Form1()
        {
            InitializeComponent();
            app = new App();
        }

        private void Form1_Load(object sender, EventArgs e) //Метод при загрузке формы;
        {
            DateTime localDate = DateTime.Now;
            string[] datetime = Convert.ToString(localDate).Split(' ');

            textBox1.Text += app.lastDate + "\r\n";
            app.GetHistory(textBox1);
            app.GetMonths(textBox1);

            label2.Text = "Баланс: " + app.CheckBalance() + " RUB\r\n";
        }

        private void button2_Click(object sender, EventArgs e) // Доход;
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

        private void button1_Click(object sender, EventArgs e) // Расход;
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

    }

}