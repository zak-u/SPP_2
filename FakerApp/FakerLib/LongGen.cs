using System;
namespace FakerLib
{
    public class LongGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }

        public object Generate(GeneratorContext context)
        {
            return (long)context.Random.Next(Int32.MinValue, Int32.MaxValue);
        }
    }
}
