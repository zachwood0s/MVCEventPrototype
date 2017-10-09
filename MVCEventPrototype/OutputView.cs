using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventPrototype
{
    class OutputView:EventHandler
    {
        public OutputView()
        {

            AddEventListener("output", (e) =>
            {
                Console.WriteLine("Output event was triggered");
                return Error.None;
            });
            AddEventListener("routingTest", (DisplayEvent e) =>
            {
                Console.WriteLine("routingWorked");
                return Error.None;
            });
        }
    }
}
