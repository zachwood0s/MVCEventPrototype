using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCEventSystem;
namespace MVCEventPrototype
{
    class OutputView:MVCEventSystem.EventHandler<Error>
    {
        [EventListenerAttr("output")]
        private Error OutputListener(IEvent e)
        {
            Console.WriteLine("out put was triggered");
            return Error.None;
        }

        [EventListenerAttr("routingTest")]
        private Error RoutingTest(DisplayEvent e)
        {
            Console.WriteLine("routing worked");
            AddEventListener("helloWorld", (evt) =>
            {
                return Error.None;
            });
            return Error.None;
        }
    }
}
