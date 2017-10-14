using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCEventSystem;

namespace ExampleNumberGame
{
    class Controller:MVCEventSystem.EventHandler<Status>
    {
        private int m;
        private int n = -1;
        private Status _status = new Status();

        [EventListenerAttr("buttonPressed")]
        private Status MainHandle(GameEvent evt)
        {
            switch (_status.State)
            {
                case State.Start:
                    {
                        bool intOK = int.TryParse(evt.Value, out m);
                        if(intOK && m>=0 && m <= 10)
                        {
                            n = (new Random()).Next(0, 10 - m);
                            _status.State = State.HaveMN;
                        }
                        else { _status.State = State.Lose; }
                        break;
                    }
                case State.HaveMN:
                    {
                        int p;
                        bool intOK = int.TryParse(evt.Value, out p);
                        if (m+n+p == 10)
                        {
                            _status.State = State.Win;
                        }
                        else
                        {
                            _status.State = State.Lose;
                        }
                        break;
                    }
                case State.Win: case State.Lose:
                    {
                        _status.State = State.Start;
                        break;
                    }
                default:
                    {
                        break;
                    }                        
            }
            return new Status(_status.State, n);
        } 
    }
}
