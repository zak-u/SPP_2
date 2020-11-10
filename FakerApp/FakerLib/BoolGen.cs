using System;
namespace FakerLib
{
    public class BoolGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }

        public object Generate(GeneratorContext context)
        {
            return context.Random.Next(19) >= 10;
        }
    }
}
