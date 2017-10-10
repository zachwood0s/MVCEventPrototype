using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    class Utils
    {
        public static EventListener<U> WrapGenericEventListener<T, U>(EventListener<T, U> e) where T : IEvent where U:IEventReturn, new()
        {
            return (IEvent evt) =>
            {
                if (evt is T)
                {
                    return e((T)evt);
                }
                else
                {
                    Debug.WriteLine("--WARNING--: Event was called with incorrect event type. {0} used but {1} needed", evt.GetType(), typeof(T));
                    return (U) new U().Default;
                }
            };
        }
    }
    public class EventListenerAttr : System.Attribute
    {
        private string _name;
        private Type _type;
        public string Name
        {
            get { return _name; }
        }
        public Type Type
        {
            get { return _type; }
        }

        public EventListenerAttr(string name)
        {
            _name = name;
        }
        public EventListenerAttr(Type type)
        {
            _type = type;
        }
    }
}
