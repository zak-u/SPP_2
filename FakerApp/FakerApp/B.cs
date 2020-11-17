using System;
using System.Collections.Generic;

namespace FakerApp
{
    public class B
    {
        public int integerValue;
        public DateTime DateTimeValue;
        public B du;
        public List<string> ListValue;
        public C CValue;
        public B()
        {

        }

        public B(int intValue)
        {
            integerValue = intValue;
        }
    }
}
