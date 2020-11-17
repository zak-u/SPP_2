using System;
using System.Collections;
using System.Collections.Generic;

namespace FakerLib
{
    public class ListGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
        }

        public object Generate(GeneratorContext context)
        {
            var result = (IList)Activator.CreateInstance(context.TargetType);
            int size = context.Random.Next(1, 6);
            Type elementsType = context.TargetType.GetGenericArguments()[0];
            
            foreach (var generator in context.gen.GetGeneratorsList())
            {
                if (generator.CanGenerate(elementsType))
                {
                    context = new GeneratorContext(context.Random, elementsType, context.gen);
                    for (var i = 0; i < size; i++)
                    {
                        result.Add( generator.Generate(context) );
                    }
                    break;
                }
            }
            return result;
        }
    }
}
