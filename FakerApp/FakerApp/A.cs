using System;
namespace FakerApp
{
    public class A
    {
        public int IntegerValue { get;  set; }
        private long LongValue;
        private double DoubleValue;
        public string StingValue { get; private set; }
        public float FloatValue;
        
        public B BValue;


        public A(int intValue, string strValue)
        {
            IntegerValue = intValue;
            StingValue = strValue;
        }
        
        public A(int intValue)
        {
            IntegerValue = intValue;
        }
        public A()
        {
        }
        public long longValue
        {
            get
            {
                return LongValue;
            }
            set
            {
                LongValue = value;
            }
        }
        public double doubleValue
        {
            get
            {
                return DoubleValue;
            }
            set
            {
                DoubleValue = value;
            }
        }
    }
}
