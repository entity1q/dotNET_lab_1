using ATMSystem;

public class AutomatedTellerMachine
{
    public string ATMId { get; set; }
    public string Address { get; set; }
    public decimal CashAmount { get; set; }

    public event Action<string>? OperationPerformed;

    public AutomatedTellerMachine(string atmId, string address, decimal cashAmount)
    {
        ATMId = atmId;
        Address = address;
        CashAmount = cashAmount;
    }

    public void Withdraw(Account account, decimal amount)
    {
        if (account.Balance < amount)
        {
            OperationPerformed?.Invoke("Недостатньо коштів на рахунку.");
            return;
        }

        if (CashAmount < amount)
        {
            OperationPerformed?.Invoke("У банкоматі недостатньо грошей.");
            return;
        }

        account.Balance -= amount;
        CashAmount -= amount;
        OperationPerformed?.Invoke($"Успішно знято {amount} грн. Баланс: {account.Balance} грн.");
    }

    public void Deposit(Account account, decimal amount)
    {
        account.Balance += amount;
        CashAmount += amount;
        OperationPerformed?.Invoke($"Успішно зараховано {amount} грн. Баланс: {account.Balance} грн.");
    }
}
