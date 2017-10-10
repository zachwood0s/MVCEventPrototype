using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public class Broadcaster<U>: EventHandler<U> where U: IEventReturn, new()
    {
        List<EventListener<U>> _registry;
        List<Type> _routings;
        public Broadcaster()
        {
            _routings = new List<Type>();
            _registry = new List<EventListener<U>>();
        }
        public void RegisterListener(EventListener<U> e)
        {
            _registry.Add(e);
        }
        public void RegisterListener<T>(EventListener<T, U> e) where T :IEvent
        {
            _registry.Add(Utils.WrapGenericEventListener(e));
        }
        public void Broadcast(IEvent e)
        {
            foreach(EventListener<U> listener in _registry)
            {
                listener(e);
            }
        }

        public void AddRouting(Type t)
        {
            _routings.Add(t);
        }

        public override U EventHandle<T>(T e)
        {
            if (_routings.Contains(e.GetType()))
            {
                Broadcast(e);
                return (U) new U().Default;
            }
            else
            {
                return base.EventHandle<T>(e);
            }
        }
    }
}
