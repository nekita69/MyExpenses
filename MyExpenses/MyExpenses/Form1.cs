namespace MyExpenses
{
    public partial class Form1 : Form
    {
        App app; // ������ ����������;

        public Form1()
        {
            InitializeComponent();
            app = new App();
        }

        private void Form1_Load(object sender, EventArgs e) //����� ��� �������� �����;
        {
            DateTime localDate = DateTime.Now;
            string[] datetime = Convert.ToString(localDate).Split(' ');

            textBox1.Text += app.lastDate + "\r\n";
            app.GetHistory(textBox1);
            app.GetMonths(textBox1);

            label2.Text = "������: " + app.CheckBalance() + " RUB\r\n";
        }

        private void button2_Click(object sender, EventArgs e) // �����;
        {
            string s = textBox2.Text.Trim(' '); //�������� ������ �� ���� �����;
            if (int.TryParse(s, out _))
            {
                textBox1.Text = ""; //������� ��������� ��� ������;
                textBox2.Text = ""; //������� ���� ����� �����;
                decimal d = Convert.ToDecimal(s);
                if (d < 0)
                    d *= -1;

                app.AddHis(d);
                app.GetHistory(textBox1); //������� ����� ������� �� textbox;
                app.AppSave();

                label2.Text = "������: " + app.CheckBalance() + " RUB\r\n";
            }
            else MessageBox.Show("�� �������� ������");
        }

        private void button1_Click(object sender, EventArgs e) // ������;
        {
            string s = textBox2.Text.Trim(' '); //�������� ������ �� ���� �����;
            if (int.TryParse(s, out _))
            {
                textBox1.Text = ""; //������� ��������� ��� ������;
                textBox2.Text = ""; //������� ���� ����� �����;
                decimal d = Convert.ToDecimal(s);
                if (d > 0)
                    d *= -1;

                if (!app.AddHis(d))
                    MessageBox.Show("������������ �������");

                app.GetHistory(textBox1); //������� ����� ������� �� textbox;
                app.AppSave();
                label2.Text = "������: " + app.CheckBalance() + " RUB\r\n";

            }
            else MessageBox.Show("�� �������� ������");
        }

    }

}