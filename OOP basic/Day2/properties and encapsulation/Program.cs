class Client
{
    private string client_name;
    private int client_age;
    private string client_email;

    //properties for control access to the fields
    public string ClientName
    {
        get { return client_name; }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                client_name = value;
            }
        }
    }

    public int ClientAge
    {
        get { return client_age; }
        set
        {
            if (value < 0 && value >= 150)
            {
                throw new ArgumentException(
                    "Please insert a valid user");
            }
            else
            {
                client_age = value;
            }
        }
    }

    public string ClientEmail
    {
        get { return client_email; }
        set
        {
            if (IsValidEmailId(value))
            {
                client_email = value;
            }
            else
            {
                throw new ArgumentException("Please insert a valid email id");
            }
        }
    }

    public bool IsValidEmailId(string clientEmail)
    {

        return clientEmail.Contains("@") && clientEmail.Contains(".");


    }
}

class Program
{
    static void Main(string[] args)
    {
        Client client = new Client();
        client.ClientName = "John Doe";
        client.ClientAge = 30;
        client.ClientEmail = "asif@gmail.com";

        Console.WriteLine($"Client Name: {client.ClientName}");
        Console.WriteLine($"Client Age: {client.ClientAge}");
        Console.WriteLine($"Client Email: {client.ClientEmail}");
    }
}