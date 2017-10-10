using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventPrototype
{
    class InputView
    {
        private EventListener<Error> _eventHandler;
        public InputView(EventListener<Error> e)
        {
            _eventHandler = e;
        }
        public void Start()
        {
          //  _eventHandler(new DisplayEvent("test1"));
           // _eventHandler(new TestEvent("this is a message", "test2"));
            _eventHandler(new TestEvent("this is an awesome message", "test4"));
            _eventHandler(new TestEvent("second event to test typeEvents", "test6"));
            _eventHandler(new DisplayEvent("routingTest"));
            
            _eventHandler(new TestEvent("third event to test typeEvents", "test9"));
        }
    }
}
