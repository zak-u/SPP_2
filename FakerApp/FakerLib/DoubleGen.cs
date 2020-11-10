using System;
namespace FakerLib
{
    public class DoubleGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.NextDouble(); ;
        }
    }
}
