// =============================================================================
// Analaysis: TIGHTLY COUPLED vs LOOSELY COUPLED
// =============================================================================

/*
SCENARIO: A simple notification system that can send messages
We'll show how SWAPPING implementations works in both approaches
*/

// =============================================================================
// 1. TIGHTLY COUPLED EXAMPLE (BAD - HARD TO SWAP)
// =============================================================================

// ❌ TIGHTLY COUPLED - Concrete classes
public class EmailNotifier
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📧 EMAIL: {message}");
    }
}

public class SmsNotifier
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📱 SMS: {message}");
    }
}

// ❌ TIGHTLY COUPLED SERVICE - Hard to change
public class TightlyoupledAlertService
{
    private EmailNotifier emailNotifier; // Direct dependency!

    public TightlyoupledAlertService()
    {
        emailNotifier = new EmailNotifier(); // Hard-coded!
    }

    public void SendAlert(string message)
    {
        emailNotifier.SendMessage(message);
    }

    // ❌ PROBLEM: To use SMS, we must MODIFY this class!
    // We'd need to:
    // 1. Add SmsNotifier field
    // 2. Change constructor
    // 3. Modify SendAlert method
    // 4. Recompile and redeploy
}

// =============================================================================
// 2. LOOSELY COUPLED EXAMPLE (GOOD - EASY TO SWAP)
// =============================================================================

// ✅ INTERFACE - Contract for any notifier
public interface INotifier
{
    void SendMessage(string message); //contract
}

// ✅ IMPLEMENTATIONS - Different ways to notify
public class EmailNotifierLoose : INotifier
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📧 EMAIL: {message}");
    }
}

public class SmsNotifierLoose : INotifier
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"📱 SMS: {message}");
    }
}

public class SlackNotifier : INotifier
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"💬 SLACK: {message}");
    }
}

// ✅ LOOSELY COUPLED SERVICE - Easy to change
public class LooselyoupledAlertService
{
    private readonly INotifier notifier; // Interface dependency!

    public LooselyoupledAlertService(INotifier notifier)
    {
        this.notifier = notifier; //will be injected during the runtime
    }

    public void SendAlert(string message)
    {
        notifier.SendMessage(message);
    }

    // ✅ BENEFIT: Works with ANY INotifier implementation!
    // No changes needed to this class!
}

// =============================================================================
// 3. DEMONSTRATION - SWAPPING IMPLEMENTATIONS
// =============================================================================

public class SwappingDemo
{
    public static void RunDemo()
    {
        Console.WriteLine("=== TIGHTLY COUPLED - HARD TO SWAP ===");
        DemonstrateTightCoupling();

        Console.WriteLine("\n=== LOOSELY COUPLED - EASY TO SWAP ===");
        DemonstrateLooseCoupling();
    }

    private static void DemonstrateTightCoupling()
    {
        // ❌ TIGHTLY COUPLED - Can only use Email
        var tightService = new TightlyoupledAlertService();
        tightService.SendAlert("System is down!");

        Console.WriteLine("❌ To use SMS, we must MODIFY the AlertService class!");
        Console.WriteLine("❌ This means changing source code, recompiling, and redeploying!");

        // We CAN'T easily switch to SMS without code changes!
    }

    private static void DemonstrateLooseCoupling()
    {
        string alertMessage = "System is down!";

        Console.WriteLine("✅ EASY SWAPPING - Same service, different notifiers:");

        // ✅ Use Email notifier
        var emailNotifier = new EmailNotifierLoose();
        var emailService = new LooselyoupledAlertService(emailNotifier);
        emailService.SendAlert(alertMessage);

        // ✅ Swap to SMS notifier - NO CODE CHANGES needed!
        var smsNotifier = new SmsNotifierLoose();
        var smsService = new LooselyoupledAlertService(smsNotifier);
        smsService.SendAlert(alertMessage);

        // ✅ Swap to Slack notifier - STILL no code changes!
        var slackNotifier = new SlackNotifier();
        var slackService = new LooselyoupledAlertService(slackNotifier);
        slackService.SendAlert(alertMessage);

        Console.WriteLine("✅ Same AlertService class works with ALL implementations!");
    }
}

// =============================================================================
// 4. VISUAL COMPARISON
// =============================================================================

/*
TIGHTLY COUPLED - HARD TO SWAP:
═══════════════════════════════

┌─────────────────────────┐
│  AlertService           │
│─────────────────────────│
│ - EmailNotifier email   │ ◄──── DIRECT DEPENDENCY
│ + SendAlert()           │       (Hard-coded!)
└─────────────────────────┘
            │
            ▼
┌─────────────────────────┐
│  EmailNotifier          │
│─────────────────────────│
│ + SendMessage()         │
└─────────────────────────┘

Problems:
❌ To use SMS, must modify AlertService
❌ Can't use multiple notifiers easily
❌ Hard to test (always sends real emails)
❌ Violates Open/Closed Principle


LOOSELY COUPLED - EASY TO SWAP:
══════════════════════════════

┌─────────────────────────┐
│  AlertService           │
│─────────────────────────│
│ - INotifier notifier    │ ◄──── INTERFACE DEPENDENCY
│ + SendAlert()           │       (Flexible!)
└─────────────────────────┘
            │
            ▼
┌─────────────────────────┐
│  INotifier              │ ◄──── CONTRACT
│─────────────────────────│
│ + SendMessage()         │
└─────────┬───────────────┘
          │
    IMPLEMENTED BY
          │
   ┌──────┼──────┬──────────┐
   │      │      │          │
   ▼      ▼      ▼          ▼
┌─────┐ ┌───┐ ┌──────┐ ┌─────────┐
│Email│ │SMS│ │Slack │ │ Future  │
│     │ │   │ │      │ │ Types   │
└─────┘ └───┘ └──────┘ └─────────┘

Benefits:
✅ Easy to swap - just change constructor parameter
✅ Can use any notifier without code changes
✅ Easy to test (use fake notifier)
✅ Follows Open/Closed Principle
*/

/*
KEY TAKEAWAYS:
═════════════

1. 🔧 TIGHTLY COUPLED:
   • Hard-coded dependencies
   • Must modify source code to change behavior
   • One implementation only
   • Difficult to test

2. 🔄 LOOSELY COUPLED:
   • Interface dependencies
   • Change behavior by swapping implementations
   • Multiple implementations possible
   • Easy to test with fakes

3. 🎯 SWAPPING BENEFITS:
   • Runtime flexibility
   • Configuration-driven behavior
   • Easy A/B testing
   • Gradual migration between systems

4. 💡 REAL-WORLD EXAMPLE:
   • Development: Use console notifier
   • Testing: Use fake notifier  
   • Production: Use email/SMS notifier
   • Same code, different behavior!
*/








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