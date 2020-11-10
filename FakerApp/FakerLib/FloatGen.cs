using System;
namespace FakerLib
{
    public class FloatGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

        public object Generate(GeneratorContext context)
        {
            double mantissa = context.Random.NextDouble();
            double exponent = Math.Pow(2.0, context.Random.Next(-128, 127));
            return (float)(mantissa * exponent);
        }
    }
}
