using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MVCEventSystem;

namespace MVCEventPrototype
{
    class Controller: Broadcaster<Error>
    {
        public Controller()
        {
            AddRouting(typeof(DisplayEvent));
        }

        [EventListenerAttr(typeof(TestEvent))]
        private Error TypeTestingEvent(TestEvent e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("type events with magic work?");
            return Error.None;
        }

        [EventListenerAttr("test4")]
        private Error TestEvent(TestEvent e)
        {

            Console.WriteLine("this worked?");
            Console.WriteLine(e.Message);
            return Error.None;
        }

        [EventListenerAttr("routingTest")]
        private Error Routing(DisplayEvent e)
        {
            Console.WriteLine("routing failed");
            return Error.None;
        }
    }
}
