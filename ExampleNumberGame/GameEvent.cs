using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleNumberGame
{
    public class GameEvent : IEvent
    {
        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
        }
        public readonly string Value;

        public GameEvent(string s, string type)
        {
            Value = s;
            _type = type;
        }
    }
}
