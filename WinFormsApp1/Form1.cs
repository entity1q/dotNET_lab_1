using ATMSystem;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Bank bank;
        private Account? currentAccount;
        private AutomatedTellerMachine atm;

        public Form1()
        {
            InitializeComponent();
            bank = new Bank();
            atm = new AutomatedTellerMachine("ATM001", "Kyiv, Shevchenka 10", 10000);

            
            Account fixedAccount1 = new Account("4441111131990099", "1234");
            Account fixedAccount2 = new Account("4441111131880088", "1234");
            Account fixedAccount3 = new Account("4441111131450045", "1234");
            bank.AddAccount(fixedAccount1);
            bank.AddAccount(fixedAccount2);
            bank.AddAccount(fixedAccount3);

            
            bank.ShowMessage += ShowMessage;          
            atm.OperationPerformed += ShowMessage;    
        }

        
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // �����������
            string cardNumber = textBox3.Text;
            string pinCode = textBox4.Text;

            currentAccount = bank.FindAccount(cardNumber);
            if (currentAccount != null && currentAccount.Authenticate(pinCode))
            {
                MessageBox.Show("����������� ������.");
            }
            else
            {
                MessageBox.Show("������� ����� ������ ��� ��-���.");
                currentAccount = null;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // ���������� �������
            if (currentAccount != null)
            {
                decimal amount;
                if (decimal.TryParse(textBox1.Text, out amount) && amount > 0)
                {
                    bank.Deposit(currentAccount, amount);  
                }
                else
                {
                    MessageBox.Show("������ ��������� ���� ��� ����������.");
                }
            }
            else
            {
                MessageBox.Show("���� �����, �������� �������������.");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            // ������ �����
            if (currentAccount != null)
            {
                if (decimal.TryParse(textBox1.Text, out decimal amount) && amount > 0)
                {
                    atm.Withdraw(currentAccount, amount);  
                }
                else
                {
                    MessageBox.Show("������ ��������� ���� ��� ������.");
                }
            }
            else
            {
                MessageBox.Show("���� �����, �������� �������������.");
            }
        }


        private void button4_Click_1(object sender, EventArgs e)
        {
            // �������� �������
            if (currentAccount != null)
            {
                MessageBox.Show($"��� ������: {currentAccount.Balance} ���.");
            }
            else
            {
                MessageBox.Show("���� �����, �������� �������������.");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            // ������� �����
            if (currentAccount != null)
            {
                string recipientCardNumber = textBox2.Text;
                decimal amount;
                if (decimal.TryParse(textBox1.Text, out amount) && amount > 0)
                {
                    bank.Transfer(currentAccount, recipientCardNumber, amount);  
                }
                else
                {
                    MessageBox.Show("������ ��������� ���� ��� ��������.");
                }
            }
            else
            {
                MessageBox.Show("���� �����, �������� �������������.");
            }
        }
    }
}
