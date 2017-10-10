using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public delegate U EventListener<T, U>(T e) where T : IEvent;
    public delegate U EventListener<U>(IEvent e) where U :IEventReturn;
    public interface IEvent
    {
        string Type
        {
            get;
        }
    }
    public interface IEventReturn
    {
        IEventReturn Default
        {
            get;
        }
    }
}
