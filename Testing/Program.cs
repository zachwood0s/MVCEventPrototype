using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            Example ex = new Example();
            EventListener<Error> listener = ex.EventHandle;
            listener(new ExampleEvent("helloWorld", 3));

            Console.ReadKey();
        }
    }


    class Example: MVCEventSystem.EventHandler<Error>
    {
        public Example()
        {

        }

        [EventListenerAttr("helloWorld")]
        private Error HelloWorldEvent(ExampleEvent e)
        {
            Console.WriteLine(e.Number);
            return Error.None;
        }

        [EventListenerAttr(typeof(ExampleEvent))]
        private Error AllOfTypeEvent(ExampleEvent e)
        {
            Console.WriteLine("All of this type");
            return Error.None;
        }
    }
    class ExampleEvent : IEvent
    {
        private string _type;
        public string Type
        {
            get { return _type; }
        }

        private int _number;
        public int Number
        {
            get { return _number; }
        }
        public ExampleEvent(string type, int n)
        {
            _type = type;
            _number = n;
        }
    }
}
