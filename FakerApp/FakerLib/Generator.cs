using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerLib
{
    public class Generator
    {
        public static List<IGenerator> generators = new List<IGenerator>() ;

        public List<IGenerator> GetGeneratorsList()
        {
            return generators;
        }
        public bool CanGenerate(Type t)
        {
            foreach (var gen in generators)
            {
                if (gen.CanGenerate(t))
                {
                    return true;
                }
            }
            return false;
        }
        public object Generate(GeneratorContext context)
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
        public void AddGenerator(IGenerator newGenerator)
        {
            generators.Add(newGenerator);
        }
        public void LoadGenerators(string path)
        {
            // Добавляем генераторы из плагинов
            DirectoryInfo pluginDirectory = new DirectoryInfo(path);
            var pluginFiles = Directory.GetFiles(path, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                var types = assembly.GetTypes().
                    Where(t => t.GetInterfaces().Where(i => i.FullName == typeof(IGenerator).FullName).Any());

                foreach (var type in types)
                {
                    var plugin = (IGenerator)assembly.CreateInstance(type.FullName);
                    generators.Add(plugin);
                }
            }
        }
    }
}
