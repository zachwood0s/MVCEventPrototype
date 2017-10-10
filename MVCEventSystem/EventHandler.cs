using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public class EventHandler<U> where U:IEventReturn, new()
    {
        private Dictionary<string, EventListener<U>> _handlers;
        private Dictionary<Type, EventListener<U>> _typeHandlers;

        public EventHandler()
        {
            _handlers = new Dictionary<string, EventListener<U>>();
            _typeHandlers = new Dictionary<Type, EventListener<U>>();
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
                var pars = method.GetParameters();
                if (pars[0] != null)
                {
                    Type typeArgument = pars[0].ParameterType;
                    if (attr.Name != null)
                    {
                        Action<MethodInfo, string> genMethod = CreateAttributedListener<IEvent>;
                        MethodInfo callMethod = GetType().GetMethod(
                            genMethod.Method.Name, 
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, 
                            null, 
                            new[] { typeof(MethodInfo), typeof(string) },
                            null
                        );
                        MethodInfo generic = callMethod.MakeGenericMethod(typeArgument);

                        generic.Invoke(this, new object[] { method, attr.Name });
                    }
                    else
                    {
                        Action<MethodInfo, Type> genMethod = CreateAttributedListener<IEvent>;
                        MethodInfo callMethod = GetType().GetMethod(
                            genMethod.Method.Name, 
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                            null, 
                            new[] { typeof(MethodInfo), typeof(Type) },
                            null
                        );
                        MethodInfo generic = callMethod.MakeGenericMethod(typeArgument);

                        generic.Invoke(this, new object[] { method, attr.Type });
                    }
                }
                
            }
            Debug.WriteLine(methods); 
        }
        protected void CreateAttributedListener<T>(MethodInfo method, string eventName) where T: IEvent
        {
            Type genericListener = typeof(EventListener<,>);
            Type constructedListener = genericListener.MakeGenericType(typeof(T), typeof(U));

            var newListener = (EventListener<T, U> )Delegate.CreateDelegate(constructedListener, this, method);
            AddEventListener(eventName, newListener);
        }
        protected void CreateAttributedListener<T>(MethodInfo method, Type eventType) where T: IEvent
        {
            Type genericListener = typeof(EventListener<,>);
            Type constructedListener = genericListener.MakeGenericType(typeof(T), typeof(U));

            var newListener = (EventListener<T, U> )Delegate.CreateDelegate(constructedListener, this, method);
            AddEventListener(eventType, newListener);
        }

        /********************************/
        /*          END MAGIC           */
        /********************************/

        protected void AddEventListener(string name, EventListener<U> e)
        { 
            _handlers.Add(name,  e); 
        }
        protected void AddEventListener<T>(string name, EventListener<T, U> listener) where T : IEvent
        {
            _handlers.Add(name, Utils.WrapGenericEventListener(listener));
        } 

        protected void AddEventListener<T>(Type type, EventListener<T, U> listener) where T: IEvent
        {
            _typeHandlers.Add(type, Utils.WrapGenericEventListener(listener));
        }
        public virtual U EventHandle<T>(T e) where T: IEvent
        {
            if (_handlers.ContainsKey(e.Type))
            {
                IEventReturn result = _handlers[e.Type](e);
                if (result != result.Default) return (U)result;
            }
            if (_typeHandlers.ContainsKey(e.GetType()))
            {
                return _typeHandlers[e.GetType()](e);
            }
            return (U) new U().Default;
        }

    }
}
