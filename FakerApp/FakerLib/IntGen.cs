using System;
namespace FakerLib
{
    public class IntGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.Next(Int32.MinValue, Int32.MaxValue);
        }
    }
}
