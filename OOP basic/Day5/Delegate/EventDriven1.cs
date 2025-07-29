/*
Delegates: A delegate is a type that represents references to methods with a specific parameter list and return type. 
It is similar to a function pointer in C or C++. Delegates are used to encapsulate method references, allowing methods to be passed as parameters,
stored in variables, or returned from other methods. 

use-case:
-> Delegates are commonly used in event handling, callback methods, and asynchronous programming.
-> They allow methods to be passed as parameters, enabling flexible and reusable code structures.
-> Delegates can be used to define custom event handlers, allowing for a clean separation of concerns in event-driven programming.
-> They can also be used to implement the observer pattern, where multiple subscribers can listen to events and respond accordingly.
-> Delegates can be used to create higher-order functions, enabling functional programming paradigms in C#.

 */

/*Example: An event driven notification system using delegates.
 ToDo: when a user places an order, the system should notify the user and the admin.
 */


//using Events and Delegates in C#
using System;
using System.Collections.Generic;

class OrderService
{
    //define a custom delegate type
    public delegate void OrderPlacedEventHandler(string orderDetails);
    /*
   EXPLANATION:
   - 'delegate' keyword creates a new type that can hold method references
   - 'void' means methods assigned to this delegate must return nothing
   - 'OrderPlacedEventHandler' is the name of this delegate type
   - '(string orderDetails)' means methods must accept one string parameter
   - Think of this as creating a "contract" that methods must follow
   */

    //Declare an event using the delegate
    public event OrderPlacedEventHandler OrderPlaced; //event declaration
    /*
    EXPLANATION:
    - 'event' keyword creates a special delegate with restrictions
    - 'OrderPlacedEventHandler' is the delegate type we defined above
    - 'OrderPlaced' is the name of this specific event instance
    - Events can only be triggered from inside the class that declares them
    - Outside classes can only subscribe (+=) or unsubscribe (-=) to events
    */


    //Event publisher method
    public void PlaceOrder(string orderDetails)
    {
        Console.WriteLine($"Order placed: {orderDetails}");

        //trigger and notify all the subscribers (multicast)
        OrderPlaced?.Invoke(orderDetails);
        /*
       EXPLANATION:
       - '?.' is null-conditional operator (safe navigation)
       - Checks if OrderPlaced is not null before calling Invoke
       - 'Invoke(orderDetails)' calls ALL methods subscribed to this event
       - Passes 'orderDetails' as parameter to each subscribed method
       - If no one is subscribed, nothing happens (no error)
       */
    }
}

//service class to handle notifications
class NotificationService
{     //an event handler - receive orderdetails from the event publisher and send Email
    public void OnOrderPlacedEmail(string orderDetails)
    {
        Console.WriteLine($"Email notification sent for order: {orderDetails}");
    }

    //an event handler - receive orderdetails from the event publisher and send SMS
    public void OnOrderPlacedSMS(string orderDetails)
    {
        Console.WriteLine($"SMS notification sent for order: {orderDetails}");
    }
}


class Program
{
    static void Main(string[] args)
    {
        //create instances of services
        OrderService orderService = new OrderService();
        NotificationService notificationService = new NotificationService();
        /*
       EXPLANATION:
       - Creates an instance of OrderService (the event publisher)
       - Creates an instance of NotificationService (contains event handlers)
       - At this point, no events are connected yet
       */


        //subscribe to the event
        orderService.OrderPlaced += notificationService.OnOrderPlacedEmail;
        orderService.OrderPlaced += notificationService.OnOrderPlacedSMS;
        /*
        EXPLANATION:
        - '+=' operator subscribes methods to the event
        - First line: "When OrderPlaced event fires, call OnOrderPlacedEmail method"
        - Second line: "When OrderPlaced event fires, ALSO call OnOrderPlacedSMS method"
        - Now we have TWO methods listening to the same event (multicast)
        */


        //place an order
        orderService.PlaceOrder("Order #1234: 2x Widget A, 1x Widget B");

        orderService.OrderPlaced -= notificationService.OnOrderPlacedSMS; // Unsubscribe from SMS notifications

        //place another order
        orderService.PlaceOrder("Order #5678: 1x Widget C, 3x Widget D");

    }
}


/*

FIRST ORDER PLACEMENT:
OrderService.PlaceOrder() called
    ↓
OrderPlaced.Invoke() executed
    ↓
┌─────────────────────────────────────┐
│  Event has 2 subscribers:           │
│  1.OnOrderPlacedEmail  ←── Called  │
│  2. OnOrderPlacedSMS    ←── Called  │
└─────────────────────────────────────┘

AFTER UNSUBSCRIBING EMAIL:
orderService.OrderPlaced -= notificationService.OnOrderPlacedEmail;
    ↓
┌─────────────────────────────────────┐
│  Event now has 1 subscriber:        │
│  1.OnOrderPlacedSMS    ←── Only    │
└─────────────────────────────────────┘

SECOND ORDER PLACEMENT:
OrderService.PlaceOrder() called
    ↓
OrderPlaced.Invoke() executed
    ↓
┌─────────────────────────────────────┐
│  Event has 1 subscriber:            │
│  1. OnOrderPlacedSMS    ←── Called  │
│  (Email handler is gone)            │
└─────────────────────────────────────┘

 
 */