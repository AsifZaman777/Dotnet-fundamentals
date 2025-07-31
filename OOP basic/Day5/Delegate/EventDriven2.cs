/*
 Notification send when order is being placed using pub sub pattern using event bus.
notification will sent to the client when order placed SMS Email

- This code is part of a larger system that handles event-driven architecture using C# and .NET.

 */


/*                  ┌─────────────────┐
                    │    EVENT BUS    │
                    │ (Message Broker)│
                    │                 │
   Publisher        │  ┌───────────┐  │        Subscribers
┌─────────────┐     │  │ Routing   │  │     ┌─────────────────┐
│OrderService │────→│  │ & Delivery│  │────→│EmailService     │
│             │     │  │ Engine    │  │     │SMSService       │
│- PlaceOrder │     │  └───────────┘  │     │                 │
│- Publish    │     │                 │     │                 │
└─────────────┘     └─────────────────┘     └─────────────────┘
      │                       │                 │
      │            LOOSE COUPLING               │
      │         ┌─────────────────────┐         │
      └─────────│   Only knows:       │─────────┘
                │   - Message format  │
                │   - EventBus        │
                │   - Nothing else!   │
                └─────────────────────┘

SUBSCRIPTION:
eventBus.Subscribe<OrderPlacedMessage>(HandleOrderPlaced);

BENEFITS:
✅ Complete decoupling
✅ Easy to add new services
✅ No circular dependencies
✅ Excellent testability
✅ Follows SOLID principles
✅ Centralized message routing
✅ Error isolation
✅ Runtime subscriber addition/removal*/


















