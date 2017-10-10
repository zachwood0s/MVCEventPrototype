using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCEventSystem
{
    public class Error
    {
        private string _message;
        private bool _errorOccured;
        public static Error None = new Error();

        public string Message
        {
            get { return _message; }
        }

        public Error()
        {
            _errorOccured = false;
        }
        public Error(string m)
        {
            _errorOccured = true;
            _message = m;
        }
        public void Catch(Action<Error> e)
        {
            if (_errorOccured)
            {
                e(this);
            }
        }
    }
}
