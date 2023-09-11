namespace MyExpenses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime localDate = DateTime.Now;
            string[] datetime = Convert.ToString(localDate).Split(' ');

            App app = new App();

            app.AddHis(-30000.40m);
            app.GetHistory(textBox1);
            app.GetMonths(textBox1);
            app.AppSave();
        }
    }

}