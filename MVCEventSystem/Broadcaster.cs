using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public class Broadcaster: EventHandler
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
            _registry.Add(Utils.WrapGenericEventListener(e));
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
}
