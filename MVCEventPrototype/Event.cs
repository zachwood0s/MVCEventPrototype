using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventPrototype
{
    delegate Error EventListener<T>(T e) where T:IEvent;
    delegate Error EventListener(IEvent e);
    interface IEvent
    {
        string Type
        {
            get; 
        }
    }
    
    class DisplayEvent: IEvent
    {
        private string _type;
        public string Type
        {
            get { return _type; }
        }
        public DisplayEvent(string type)
        {
            _type = type;
        }
    }
    class TestEvent: IEvent
    {
        private string _message;

        private string _type;
        public string Type
        {
            get { return _type; }
        }
        public string Message
        {
            get { return _message; }
        }
        public TestEvent(string message, string type) 
        {
            _type = type;
            _message = message;
        }
    }

    class Error
    {
        private string _message;
        private bool _errorOccured;
        public static Error None = new Error();

        public string Message
        {
            get { return _message; }
        }

        public Error()
        {
            _errorOccured = false;
        }
        public Error(string m)
        {
            _errorOccured = true;
            _message = m;
        }
        public void Catch(Action<Error> e)
        {
            if (_errorOccured)
            {
                e(this);
            }
        }
    }

    class EventHandler
    {
        private Dictionary<string, EventListener> _handlers;
        private Dictionary<Type, EventListener> _typeHandlers;

        public EventHandler()
        {
            _handlers = new Dictionary<string, EventListener>();
            _typeHandlers = new Dictionary<Type, EventListener>();
            LoadAttributedListeners();
        }

        /********************************/
        /*          MAGIC STUFF         */
        /********************************/
        private void LoadAttributedListeners()
        {
            var methods = GetType()
                          .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                          .Where(m => m.GetCustomAttributes().OfType<EventListenerAttr>().Any())
                          .ToArray();

            foreach(var method in methods)
            {
                EventListenerAttr attr = method.GetCustomAttribute<EventListenerAttr>();
                string eventName = attr.Name;

                var pars = method.GetParameters();
                if (pars[0] != null)
                {
                    Type typeArgument = pars[0].ParameterType;

                    Action<MethodInfo, string> genMethod = CreateAttributedListener<IEvent>;
                    MethodInfo callMethod = GetType().GetMethod(genMethod.Method.Name, BindingFlags.Public | BindingFlags.NonPublic| BindingFlags.Instance);
                    MethodInfo generic = callMethod.MakeGenericMethod(typeArgument);
                    generic.Invoke(this, new object[] { method, eventName });
                }
            }
            Debug.WriteLine(methods); 
        }
        protected void CreateAttributedListener<T>(MethodInfo method, string eventName) where T: IEvent
        {
            Type genericListener = typeof(EventListener<>);
            Type constructedListener = genericListener.MakeGenericType(typeof(T));

            var newListener = (EventListener<T> )Delegate.CreateDelegate(constructedListener, this, method);
            AddEventListener(eventName, newListener);
        }

        /********************************/
        /*          END MAGIC           */
        /********************************/

        protected void AddEventListener(string name, EventListener e)
        { 
            _handlers.Add(name,  e); 
        }
        protected void AddEventListener<T>(string name, EventListener<T> listener) where T: IEvent
        {
            _handlers.Add(name, (IEvent evt) =>
            {
                if(evt is T)
                {
                    return listener((T)evt); 
                }
                else
                {
                    Debug.WriteLine("--WARNING--: Event was called with incorrect event type. {0} used but {1} needed",evt.GetType(), typeof(T));
                    return Error.None;
                }
            });
        }

        protected void AddEventListener<T>(Type type, EventListener<T> listener) where T: IEvent
        {
            _typeHandlers.Add(type, (IEvent evt) =>
            {
                if(evt is T)
                {
                    return listener((T)evt);
                }
                else
                {
                    Debug.WriteLine("--WARNING--: Event was called with incorrect event type. {0} used but {1} needed",evt.GetType(), typeof(T));
                    return Error.None;
                }
            });
        }
        public virtual Error EventHandle<T>(T e) where T: IEvent
        {
            if (_handlers.ContainsKey(e.Type))
            {
                Error result = _handlers[e.Type](e);
                if (result != Error.None) return result;
            }
            if (_typeHandlers.ContainsKey(e.GetType()))
            {
                return _typeHandlers[e.GetType()](e);
            }
            return Error.None;
        }

    }


    class Broadcaster: EventHandler
    {

        List<EventListener> _registry;
        List<Type> _routings;
        public Broadcaster()
        {
            _routings = new List<Type>();
            _registry = new List<EventListener>();
        }
        public void RegisterListener(EventListener e)
        {
            _registry.Add(e);
        }
        public void RegisterListener<T>(EventListener<T> e) where T :IEvent
        {
            _registry.Add((IEvent evt) =>
            {
                if(evt is T)
                {
                    return e((T)evt);
                }
                else
                {
                    Debug.WriteLine("--WARNING--: Event was called with incorrect event type. {0} used but {1} needed",evt.GetType(), typeof(T));
                    return Error.None;
                }
            });
        }
        public void Broadcast(IEvent e)
        {
            foreach(EventListener listener in _registry)
            {
                listener(e);
            }
        }

        public void AddRouting(Type t)
        {
            _routings.Add(t);
        }

        public override Error EventHandle<T>(T e)
        {
            if (_routings.Contains(e.GetType()))
            {
                Broadcast(e);
                return Error.None;
            }
            else
            {
                return base.EventHandle<T>(e);
            }
        }
    }
    

    public class EventListenerAttr : System.Attribute
    {
        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public EventListenerAttr(string name)
        {
            _name = name;
        }
    }
}

