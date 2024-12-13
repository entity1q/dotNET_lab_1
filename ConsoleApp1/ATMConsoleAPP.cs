using System;
using ATMSystem;

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();
        bool running = true;
        Account? currentAccount = null;

        while (running)
        {
            Console.WriteLine("\nОберіть опцію:");
            Console.WriteLine("1. Створити рахунок");
            Console.WriteLine("2. Авторизація");
            Console.WriteLine("3. Поповнити рахунок");
            Console.WriteLine("4. Зняти кошти");
            Console.WriteLine("5. Перерахувати кошти");
            Console.WriteLine("6. Перевірити баланс");
            Console.WriteLine("7. Вихід");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    // Створення профілю
                    Console.WriteLine("Введіть номер картки (16 цифр):");
                    string cardNumber = Console.ReadLine();
                    if (cardNumber.Length != 16 || !cardNumber.All(char.IsDigit))
                    {
                        Console.WriteLine("Невірний номер картки.");
                        break;
                    }
                    Console.WriteLine("Введіть пін-код (4 цифри):");
                    string pinCode = Console.ReadLine();
                    if (pinCode.Length != 4 || !pinCode.All(char.IsDigit))
                    {
                        Console.WriteLine("Невірний пін-код.");
                        break;
                    }
                    bank.CreateAccount(cardNumber, pinCode);
                    break;

                case "2":
                    // Аутентифікація
                    Console.WriteLine("Введіть номер картки:");
                    cardNumber = Console.ReadLine();
                    Console.WriteLine("Введіть пін-код:");
                    pinCode = Console.ReadLine();
                    currentAccount = bank.FindAccount(cardNumber);

                    if (currentAccount != null && currentAccount.Authenticate(pinCode))
                    {
                        Console.WriteLine("Авторизація успішна.");
                    }
                    else
                    {
                        Console.WriteLine("Невірний номер картки або пін-код.");
                        currentAccount = null;
                    }
                    break;

                case "3":
                    // Поповнення рахунку
                    if (currentAccount != null)
                    {
                        Console.WriteLine("Введіть суму для поповнення:");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        bank.Deposit(currentAccount, amount);
                    }
                    else
                    {
                        Console.WriteLine("Будь ласка, спочатку пройдіть авторизацію.");
                    }
                    break;

                case "4":
                    // Зняття коштів
                    if (currentAccount != null)
                    {
                        Console.WriteLine("Введіть суму для зняття:");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        bank.Withdraw(currentAccount, amount);
                    }
                    else
                    {
                        Console.WriteLine("Будь ласка, спочатку пройдіть авторизацію.");
                    }
                    break;

                case "5":
                    // Перерахування коштів
                    if (currentAccount != null)
                    {
                        Console.WriteLine("Введіть номер картки отримувача:");
                        string recipientCardNumber = Console.ReadLine();
                        Console.WriteLine("Введіть суму для переказу:");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());
                        bank.Transfer(currentAccount, recipientCardNumber, amount);
                    }
                    else
                    {
                        Console.WriteLine("Будь ласка, спочатку пройдіть авторизацію.");
                    }
                    break;

                case "6":
                    // Перевірка балансу
                    if (currentAccount != null)
                    {
                        Console.WriteLine($"Ваш баланс: {currentAccount.Balance} грн.");
                    }
                    else
                    {
                        Console.WriteLine("Будь ласка, спочатку пройдіть авторизацію.");
                    }
                    break;

                case "7":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Невірна опція.");
                    break;
            }
        }
    }
}
