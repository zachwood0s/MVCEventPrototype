using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller c = new Controller();
            InputView i = new InputView(c.EventHandle);
            OutputView o = new OutputView();

            c.RegisterObserver(o.EventHandle);
            i.Start();

            Console.ReadKey();
        }
    }
}
