namespace ATMSystem
{
    public class Account
    {
        public string CardNumber { get; set; }
        public string PinCode { get; set; }
        public decimal Balance { get; set; }

        public Account(string cardNumber, string pinCode)
        {
            CardNumber = cardNumber;
            PinCode = pinCode;
            Balance = 5000;  
        }

        public bool Authenticate(string pin)
        {
            return PinCode == pin;
        }

        
        public decimal GetBalance()
        {
            return Balance;
        }
    }
}
