using System;
namespace FakerLib
{
    public class GeneratorContext
    {
        public Random Random { get; }

        public Type TargetType { get; }

        public Generator gen { get; }

        public GeneratorContext(Random random, Type targetType, Generator Gen)
        {
            Random = random;
            TargetType = targetType;
            gen = Gen;
        }
    }
}
