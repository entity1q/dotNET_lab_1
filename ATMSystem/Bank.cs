namespace ATMSystem
{
    public class Bank
    {
        public event Action<string> ShowMessage;

        public Bank()
        {
          
            Account fixedAccount1 = new Account("4441111131990099", "1234");
            Account fixedAccount2 = new Account("4441111131880088", "1234");
            Account fixedAccount3 = new Account("4441111131450045", "1234");

            
            AddAccount(fixedAccount1);
            AddAccount(fixedAccount2);
            AddAccount(fixedAccount3);
        }

        private List<Account> accounts = new List<Account>();


        public void CreateAccount(string cardNumber, string pinCode)
        {
            var account = new Account(cardNumber, pinCode);
            accounts.Add(account);
            ShowMessage?.Invoke("Рахунок успішно створено.");
        }

        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }

      
        public Account? FindAccount(string cardNumber)
        {
            return accounts.FirstOrDefault(acc => acc.CardNumber == cardNumber);
        }

        // Поповнити рахунок
        public void Deposit(Account account, decimal amount)
        {
            if (amount > 0)
            {
                account.Balance += amount;
                ShowMessage?.Invoke($"Ваш рахунок поповнено на {amount} грн. Тепер ваш баланс: {account.Balance} грн.");
            }
            else
            {
                ShowMessage?.Invoke("Сума поповнення повинна бути більшою за 0.");
            }
        }

        // Зняти кошти
        public void Withdraw(Account account, decimal amount)
        {
            if (amount <= 0)
            {
                ShowMessage?.Invoke("Сума зняття повинна бути більшою за 0.");
            }
            else if (account.Balance >= amount)
            {
                account.Balance -= amount;
                ShowMessage?.Invoke($"Знято {amount} грн. Залишок на рахунку: {account.Balance} грн.");
            }
            else
            {
                ShowMessage?.Invoke("Недостатньо коштів на рахунку.");
            }
        }

        // Перерахувати кошти
        public void Transfer(Account sender, string recipientCardNumber, decimal amount)
        {
            var recipient = FindAccount(recipientCardNumber);

            if (recipient == null)
            {
                ShowMessage?.Invoke("Рахунок отримувача не знайдено.");
                return;
            }

            if (sender.Balance >= amount && amount > 0)
            {
                sender.Balance -= amount;
                recipient.Balance += amount;
                ShowMessage?.Invoke($"Перераховано {amount} грн. на картку {recipientCardNumber}. Ваш баланс: {sender.Balance} грн.");
            }
            else
            {
                ShowMessage?.Invoke("Недостатньо коштів або некоректна сума переказу.");
            }
        }
    }
}
