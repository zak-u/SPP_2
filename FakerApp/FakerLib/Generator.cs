﻿using System;
using System.Collections.Generic;

namespace FakerLib
{
    public class Generator
    {
        public static List<IGenerator> generators = new List<IGenerator>();
        public static object Generate(GeneratorContext context)
        {
            foreach (var gen in generators)
            {
                if (gen.CanGenerate(context.TargetType))
                {
                    return gen.Generate(context);
                }
            }
            return null;
        }
    }
}
