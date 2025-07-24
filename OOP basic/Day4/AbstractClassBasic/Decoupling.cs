// =============================================================================
// DECOUPLING ANALYSIS: Abstract Classes vs Interfaces
// =============================================================================

/*
DECOUPLING SCORECARD:
═══════════════════

Mechanism                │ Interfaces │ Abstract Classes │ Concrete Classes
═════════════════════════┼════════════┼══════════════════┼═════════════════
Dependency Inversion     │     ⭐⭐⭐    │        ⭐⭐        │        ✗
Loose Coupling          │     ⭐⭐⭐    │        ⭐⭐        │        ✗
Testability             │     ⭐⭐⭐    │        ⭐         │        ✗
Multiple Implementations │     ⭐⭐⭐    │        ⭐         │        ✗
Code Reusability        │     ⭐      │        ⭐⭐⭐       │        ⭐⭐
Flexibility             │     ⭐⭐⭐    │        ⭐⭐        │        ✗

WINNER FOR DECOUPLING: INTERFACES 🏆
*/

// =============================================================================
// DEMONSTRATION: TIGHT vs LOOSE COUPLING
// =============================================================================

// ❌ TIGHTLY COUPLED - BAD EXAMPLE
public class TightlyCoupledNotificationService
{
    private EmailSender emailSender;      // Direct dependency on concrete class
    private SmsSender smsSender;          // Another concrete dependency

    public TightlyCoupledNotificationService()
    {
        emailSender = new EmailSender();  // Hard-coded creation
        smsSender = new SmsSender();      // Hard-coded creation
    }

    public void SendNotification(string message, string type)
    {
        if (type == "email")
            emailSender.SendEmail(message);     // Knows about specific implementation
        else if (type == "sms")
            smsSender.SendSms(message);         // Knows about specific implementation

        // Problem: To add new notification types, we must modify this class!
    }
}

// ✅ LOOSELY COUPLED - GOOD EXAMPLE USING INTERFACES
public interface INotificationSender
{
    void Send(string message, string recipient);
    bool IsAvailable { get; }
}

// Concrete implementations
public class EmailSender : INotificationSender
{
    public void Send(string message, string recipient)
        => Console.WriteLine($"Email sent to {recipient}: {message}");
    public bool IsAvailable => true;
}

public class SmsSender : INotificationSender
{
    public void Send(string message, string recipient)
        => Console.WriteLine($"SMS sent to {recipient}: {message}");
    public bool IsAvailable => true;
}

public class PushNotificationSender : INotificationSender
{
    public void Send(string message, string recipient)
        => Console.WriteLine($"Push notification to {recipient}: {message}");
    public bool IsAvailable => true;
}

// ✅ DECOUPLED SERVICE - Depends on abstraction, not concretions
public class DecoupledNotificationService
{
    private readonly IEnumerable<INotificationSender> _senders;

    // Dependency Injection - receives dependencies from outside
    public DecoupledNotificationService(IEnumerable<INotificationSender> senders)
    {
        _senders = senders;
    }

    public void SendNotification(string message, string recipient)
    {
        // Can work with ANY implementation of INotificationSender
        foreach (var sender in _senders.Where(s => s.IsAvailable))
        {
            sender.Send(message, recipient);
        }

        // Benefits:
        // 1. No modification needed for new notification types
        // 2. Easy to test with mock implementations
        // 3. Runtime flexibility - can change implementations
        // 4. Single Responsibility - only orchestrates, doesn't create
    }
}

// =============================================================================
// ABSTRACT CLASSES FOR PARTIAL DECOUPLING
// =============================================================================

// Abstract class provides some decoupling but less than interfaces
public abstract class NotificationServiceBase
{
    protected readonly string serviceName;

    protected NotificationServiceBase(string serviceName)
    {
        this.serviceName = serviceName;
    }

    // Template method pattern - defines algorithm structure
    public void ProcessNotification(string message, string recipient)
    {
        if (ValidateMessage(message))
        {
            LogNotification(message, recipient);
            SendMessage(message, recipient);  // Abstract - must be implemented
            UpdateDeliveryStatus(recipient);
        }
    }

    // Concrete methods - shared implementation
    protected virtual bool ValidateMessage(string message) => !string.IsNullOrEmpty(message);
    protected virtual void LogNotification(string message, string recipient)
        => Console.WriteLine($"[{serviceName}] Logging: {message} to {recipient}");
    protected virtual void UpdateDeliveryStatus(string recipient)
        => Console.WriteLine($"[{serviceName}] Status updated for {recipient}");

    // Abstract method - forces implementation
    protected abstract void SendMessage(string message, string recipient);
}

public class EmailNotificationService : NotificationServiceBase
{
    public EmailNotificationService() : base("Email") { }

    protected override void SendMessage(string message, string recipient)
    {
        Console.WriteLine($"📧 Email sent to {recipient}: {message}");
    }
}

public class SmsNotificationService : NotificationServiceBase
{
    public SmsNotificationService() : base("SMS") { }

    protected override void SendMessage(string message, string recipient)
    {
        Console.WriteLine($"📱 SMS sent to {recipient}: {message}");
    }

    // Can override shared behavior if needed
    protected override bool ValidateMessage(string message)
    {
        return base.ValidateMessage(message) && message.Length <= 160;
    }
}

// =============================================================================
// DECOUPLING VISUALIZATION
// =============================================================================

/*
TIGHT COUPLING DIAGRAM:
═══════════════════════

┌─────────────────────────┐
│  NotificationService    │ ──┐
│─────────────────────────│   │ DIRECTLY DEPENDS ON
│ - EmailSender           │ ◄─┘ (knows concrete types)
│ - SmsSender             │ ◄─┐
│ + SendNotification()    │   │ CREATES INSTANCES
└─────────────────────────┘   │ (hard-coded dependencies)
             │                │
             ▼                │
┌─────────────────┐     ┌─────────────────┐
│   EmailSender   │     │    SmsSender    │
│─────────────────│     │─────────────────│
│ + SendEmail()   │     │ + SendSms()     │
└─────────────────┘     └─────────────────┘

Problems:
• Hard to test (can't mock dependencies)
• Hard to extend (must modify service for new types)
• Violation of Open/Closed Principle
• High coupling, low cohesion


LOOSE COUPLING DIAGRAM:
══════════════════════

┌─────────────────────────┐
│  NotificationService    │ ──┐
│─────────────────────────│   │ DEPENDS ON ABSTRACTION
│ - INotificationSender[] │ ◄─┘ (interface contract)
│ + SendNotification()    │   
└─────────────────────────┘   
             │                
             ▼                
┌─────────────────────────┐
│  INotificationSender    │ ◄── INTERFACE CONTRACT
│─────────────────────────│
│ + Send()                │
│ + IsAvailable           │
└──────────┬──────────────┘
           │ IMPLEMENTED BY
     ┌─────┼─────┬─────────────┐
     │     │     │             │
┌──────┐ ┌───┐ ┌─────┐ ┌──────────┐
│Email │ │SMS│ │Push │ │ Future   │
│Sender│ │   │ │     │ │ Senders  │
└──────┘ └───┘ └─────┘ └──────────┘

Benefits:
• Easy to test (can inject mocks)
• Easy to extend (just add new implementations)
• Follows SOLID principles
• Low coupling, high cohesion
*/

// =============================================================================
// SIMPLE DECOUPLING EXAMPLES
// =============================================================================

// Example 1: Simple Payment Processing
public interface IPaymentProcessor
{
    bool ProcessPayment(decimal amount);
    string GetPaymentMethod();
}

public class CreditCardProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via Credit Card");
        return true;
    }

    public string GetPaymentMethod() => "Credit Card";
}

public class PayPalProcessor : IPaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing ${amount} via PayPal");
        return true;
    }

    public string GetPaymentMethod() => "PayPal";
}

// Example 2: Simple Logger Interface
public interface ILogger
{
    void Log(string message);
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[FILE] {DateTime.Now}: {message}");
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[CONSOLE] {DateTime.Now}: {message}");
    }
}

// Example 3: Simple Order Service (Decoupled)
public class OrderService
{
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly ILogger _logger;

    // Constructor injection - receives dependencies
    public OrderService(IPaymentProcessor paymentProcessor, ILogger logger)
    {
        _paymentProcessor = paymentProcessor;
        _logger = logger;
    }

    public bool ProcessOrder(string item, decimal price)
    {
        _logger.Log($"Processing order for {item}");

        bool success = _paymentProcessor.ProcessPayment(price);

        if (success)
            _logger.Log($"Order completed for {item} using {_paymentProcessor.GetPaymentMethod()}");
        else
            _logger.Log($"Order failed for {item}");

        return success;
    }
}

// =============================================================================
// EASY-TO-UNDERSTAND USAGE EXAMPLES
// =============================================================================

public class SimpleDecouplingDemo
{
    public static void RunDemo()
    {
        Console.WriteLine("=== DECOUPLING DEMONSTRATION ===\n");

        // Example 1: Different payment methods
        var creditCardProcessor = new CreditCardProcessor();
        var paypalProcessor = new PayPalProcessor();
        var fileLogger = new FileLogger();

        // Same service, different payment method
        var orderService1 = new OrderService(creditCardProcessor, fileLogger);
        var orderService2 = new OrderService(paypalProcessor, fileLogger);

        orderService1.ProcessOrder("Laptop", 999.99m);
        orderService2.ProcessOrder("Mouse", 25.50m);

        Console.WriteLine("\n=== NOTIFICATION DEMONSTRATION ===\n");

        // Example 2: Different notification methods
        var notificationSenders = new List<INotificationSender>
        {
            new EmailSender(),
            new SmsSender(),
            new PushNotificationSender()
        };

        var notificationService = new DecoupledNotificationService(notificationSenders);
        notificationService.SendNotification("Hello World!", "user@example.com");
    }
}

// =============================================================================
// DECOUPLING BEST PRACTICES (SIMPLIFIED)
// =============================================================================

/*
SIMPLE DECOUPLING RULES:
═══════════════════════

1. 🎯 USE INTERFACES FOR CONTRACTS
   ✅ Define what something CAN DO, not how it does it
   
   // Good
   public interface IEmailSender { void SendEmail(string message); }
   
   // Bad  
   public class GmailSender { void SendViaGmail(string message); }

2. 🎯 INJECT DEPENDENCIES
   ✅ Pass dependencies to constructors
   ✅ Don't create dependencies inside classes
   
   // Good
   public class OrderService(IPaymentProcessor processor) { }
   
   // Bad
   public class OrderService() { var processor = new CreditCardProcessor(); }

3. 🎯 PROGRAM TO INTERFACES
   ✅ Use interface types for parameters and fields
   
   // Good
   private readonly ILogger _logger;
   
   // Bad
   private readonly FileLogger _logger;

4. 🎯 SINGLE RESPONSIBILITY
   ✅ Each class should have one reason to change
   ✅ Separate concerns using different interfaces
   
   // Good
   public interface IEmailSender { }
   public interface ILogger { }
   
   // Bad
   public interface IEmailAndLogService { void SendEmail(); void Log(); }
*/

// =============================================================================
// SUMMARY: WHY INTERFACES WIN FOR DECOUPLING
// =============================================================================

/*
ANSWER: INTERFACES CREATE MORE DECOUPLED CODE! 🏆

SIMPLE REASONS WHY:
═══════════════════

1. ✅ PURE CONTRACTS
   • Interfaces only define WHAT, not HOW
   • No implementation details to worry about
   • Easy to understand and implement

2. ✅ EASY TO SWAP
   • Change implementations without changing client code
   • Perfect for testing (use fake implementations)
   • Runtime flexibility

3. ✅ MULTIPLE CAPABILITIES
   • Classes can implement many interfaces
   • Mix and match features as needed
   • Build complex behavior from simple parts

4. ✅ FUTURE-PROOF
   • Add new implementations anytime
   • No need to modify existing code
   • Follows Open/Closed Principle

WHEN TO USE ABSTRACT CLASSES:
═══════════════════════════

✅ When you have shared code between related classes
✅ When you want to provide default behavior
✅ When classes are part of the same family (IS-A relationship)

BEST PRACTICE:
═════════════

Use BOTH together:
• Abstract class for shared implementation
• Interfaces for capabilities and contracts
• Maximum flexibility with minimal duplication
*/