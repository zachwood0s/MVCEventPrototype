# MVCEventSystem

This is a simple library that makes MVC event handling and event broadcasting easy. 

## Features
- An EventHandler object holding all the event handlers
- Different event types
- Event listeners that can respond to all of one type of event
- Event listeners that can respond to specific events
- Broadcasters allowing for... well... *broadcasting*
- Routing allowing for... you guessed it... *routing*

## Usage

### EventListeners

Suppose you wanted to make a class called Example and wanted EventListeners that return an Error (an example IEventReturn provided int MVCEventSystem).
We'll first create a simple event that prints "Hello World" when triggered.

```C#

class Example: EventHandler<Error>
{
    public Example(){

        AddEventListener("helloWorld", (e)=>{
            Console.WriteLine("Hello World");
            return Error.None;
        });
    }
}

```

Now whenever our EventHandle recieves an event named "helloWorld", our listener will respond with "Hello World". That's pretty neat, but not entirely useful unless we know how to broadcast an event. To do that we'll need to create something like an InputView. We'll also need something of an event class. The system only requires that the event class implement IEvent, other than that it can be whatever we want. Heres a sample of an event class:

```C#

class ExampleEvent: IEvent
{
    private string _type;
    public string Type
    {
        get{ return _type; }   
    }
    public ExampleEvent(string type)
    {
        _type = type;
    }
}
```

So now we have a super simple event class and we can use it to broadcast events from our InputView or wherever like so:

```C#

Example ex = new Example();
EventListener<Error> listener = ex.EventHandle;
listener(new ExampleEvent("helloWorld"));

```

This works pretty nicely, but what if we wanted to send more data along with our event like a number the user inputed? Well we can do that as well. Let's change our ExampleEvent a little to have a number stored in it.

```C#

class ExampleEvent: IEvent
{
    private string _type;
    public string Type
    {
        get{ return _type; }
    }

    private int _number;
    public int Number{
        get{ return _number; }
    }

    public ExampleEvent(string type, int n)
    {
        _type = type;
        _number = n;
    }
}

```

Now that we have our new event, let's modify our EventListener to make use of the new data.

```C#

AddEventListener("helloWorld", (e) =>{
    Console.WriteLine(e.Number);
    return Error.None;
});

```

So this would be great if it worked, but by default our event listeners only take IEvents as parameters and IEvents don't have a Number property so we'll have to cast.

```C#

AddEventListener("helloWorld", (e)=>{
    ExampleEvent evt = e as ExampleEvent;
    Console.WriteLine(e.Number);
    return Error.None;
});

```

Now it works, but in my option it looks pretty terrible. Everytime we create a new event we'd have to cast to our specific event type and then check to make sure the cast succeeded. That's no fun. Fortunately the system allows us to specify the event type in the parameter like so:

```C#

AddEventListener("helloWorld", (ExampleEvent e) =>{
    Console.WriteLine(e.Number);
    return Error.None;
});

```

Now it will work great. We'll just need to change our listener call now to deal with our new event type and we'll be good to go.

```C#

listener(new ExampleEvent("helloWorld", 4));

```

So what would happen if we tried to call the "helloWorld" event with the wrong type of event. Well you would recieve a debug warning resembling this: **--WARNING--: Event was called with incorrect event type. {0} used but {1} needed**. 

Now what if we wanted to respond to all events of the type ExampleEvent with one listener. Well, instead of specifing a name in the AddEventListener, we can specify a type like so:

```C#

AddEventListener(typeof(ExampleEvent), (ExampleEvent e) =>{
    Console.WriteLine("I'm responding to all ExampleEvents");
    return Error.None;
});

```

Now "I'm responding to all ExampleEvents" will be printed any time an ExampleEvent is broadcast, no matter what the type of the event is.

Defining events this way is okay I guess, but there's still a lot to be desired from it. Writing AddEventListener every time can be annoying and the whole lambda syntax can look a little messy sometimes. Also it's not super clear what is being returned always especially if you had larger EventListeners. So now we're going to look at a different way to declare events: Attributes.

### Attributes

Attributes in C# are really cool and you should totally look them up. I'm not gonna explain them right now, I'm just gonna show you how to use it in the MVCEventSystem. Here's our "helloWorld" event listener defined with attributes.

```C#

[EventListenerAttr("helloWorld")]
private Error HelloWorldEvent(ExampleEvent e){
    Console.WriteLine(e.Number);
    return Error.None;
}

```

Well that's pretty nice. Now we can clearly see what our return type is and it's less cluttered. Also, now that it's not an anonymous function, we can call it from other places in our Example class which is neat. We can also respond to all events of a specific type with this method like so:

```C#

[EventListenerAttr(typeof(ExampleEvent))]
private Error AllOfTypeEvent(ExampleEvent e){
    Console.WriteLine("All of this type");
    return Error.None;
}

```
Neato.