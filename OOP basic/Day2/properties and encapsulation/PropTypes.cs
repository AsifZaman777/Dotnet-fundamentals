class Account
{
    private string accountNumber;
    private decimal accountBalance;
    private string creationDate;

    //types of properties

    //reead-write property
    public string AccountNumber { get; set; }

    //read-only property
    public string AccountBalance
    {
        get { return accountBalance.ToString(); }
    }
    //write-only property
    public string CreationDate
    {
        set { creationDate = value; }
    }
    //auto-implemented property - no backing field required
    public string AccountType { get; set; }

    //computed property- no backing field required
    public string AccountAge
    {
        get { DateTime creation = DateTime.Parse(creationDate); 
              return (DateTime.Now - creation).TotalDays.ToString("F0") + " days old";
        }
    }

    //property with validation
    public decimal AccountBalanceValidation
    {
        get { return accountBalance; }
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Account balance cannot be negative.");
            }
            accountBalance = value;
        }
    }

    //we can use private setter in the sames class
    public decimal UpdateAccountBalance
    {
        set
        {
          if(value == accountBalance)
            {
                throw new ArgumentException("New balance cannot be the same as current balance.");
            }
          else if(value < 0)
            {
                throw new ArgumentException("Account balance cannot be negative.");
            }
          else
            {
                AccountBalanceValidation = value;
            }
    }

}

    class Program
    {
        static void Main(string[] args)
        {
            //object initializer
            Account account = new Account
            {
                AccountNumber = "123456789",
                AccountType = "Savings"
            };
            account.CreationDate = "2023-01-01";
            account.UpdateAccountBalance = 1000.00m; // so now we are using udpateAccountBalance property to use the private setter
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Account Type: {account.AccountType}");
            //Console.WriteLine($"Creation Date: {account.CreationDate}"); // we cannot access this property directly as it is write-only
            Console.WriteLine($"Account Balance: {account.AccountBalance}");
            Console.WriteLine($"Account Age: {account.AccountAge}");
        }
    }
}