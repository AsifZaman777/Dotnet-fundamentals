//public class EmailNotifier
//{
//    public void SendEmail(string msg) => Console.WriteLine("Email message:" + msg);
//}

//public class SmsNotifier
//{
//    public void SendSMS(string msg) => Console.WriteLine("SMS message: " + msg);
//}


//public class NotificationService
//{
//    public SmsNotifier smsNotifier; 

//    public NotificationService()
//    {
//        smsNotifier = new SmsNotifier();  //hardcoded dependency - tightly coupled - declared inside the class
//    }

//    public void SendAlert()
//    {
//        smsNotifier.SendSMS("Test Email");
//    }
//}




//loosely coupled solution

public interface INotificationService
{
    public void SendMessage(string msg); //cotnract
}

public class EmailNotifier : INotificationService
{
    public void SendMessage(string msg) => Console.WriteLine("Email message:" + msg);
}

public class SmsNotifier: INotificationService
{
    public void SendMessage(string msg) => Console.WriteLine("SMS message: " + msg);
}


public class NotificationService
{
    private INotificationService _notificationService;

    //constructor injection during runtime
    public NotificationService(INotificationService notificationService) //loose coupling - dependency is injected from outside
    {
       _notificationService=notificationService; //can receive any implementation SMS, Email, etc.
    }

    public void sendGenericMessage()
    {
        _notificationService.SendMessage("Message sent");
    }
}


public class Practice()
{
    static void Main(string[] args)                 
    {
        //NotificationService service = new NotificationService();
        //service.SendAlert();

        INotificationService emailNotifier = new EmailNotifier();
        emailNotifier.SendMessage("Ola I am email");

        INotificationService smsService =  new SmsNotifier();
        smsService.SendMessage("Ola I am sms");

    }
}
