using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public delegate Error EventListener<T>(T e) where T : IEvent;
    public delegate Error EventListener(IEvent e);
    public interface IEvent
    {
        string Type
        {
            get;
        }
    }
}
