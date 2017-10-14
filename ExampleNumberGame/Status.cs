using MVCEventSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleNumberGame
{
    public enum State { Start, HaveMN, Win, Lose };
    public class Status: IEventReturn
    {
        private State _state;
        public State State{
            get { return _state; }
            set { _state = value; }
        }

        private int _chosenNumber;
        public int ChosenNumber
        {
            get { return _chosenNumber; }
        }


        private static IEventReturn _default = new Status(); 
        public IEventReturn Default
        {
            get { return _default; }
        }

        public Status(State s, int chosen)
        {
            _state = s;
            _chosenNumber = chosen;
        }

        public Status()
        {
            _state = State.Start;
            _chosenNumber = 0;
        }
    }
}
