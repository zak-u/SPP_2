using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FakerLib;
namespace Faker
{
    public class Faker : IFaker
    {
        private Stack<Type> types = new Stack<Type>();
        private readonly string pathToPlugins = "/Users/admin/Projects/FakerApp/Plugins";

        public Faker()
        {
            FillGenerator();
        }

        public void FillGenerator()
        {
            Generator.generators.Add(new IntGen());
            Generator.generators.Add(new LongGen());
            Generator.generators.Add(new DoubleGen());
            Generator.generators.Add(new FloatGen());
            Generator.generators.Add(new ListGen());
            Generator.generators.Add(new BoolGen());
           

            // Добавляем генераторы из плагинов
            DirectoryInfo pluginDirectory = new DirectoryInfo(pathToPlugins);


            var pluginFiles = Directory.GetFiles(pathToPlugins, "*.dll");
            foreach (var file in pluginFiles)
            {
                Assembly assembly = Assembly.LoadFrom(file);

                var types = assembly.GetTypes().
                    Where(t => t.GetInterfaces().Where(i => i.FullName == typeof(IGenerator).FullName).Any());

                foreach (var type in types)
                {
                    var plugin = (IGenerator)assembly.CreateInstance(type.FullName);
                    Generator.generators.Add(plugin); 
                }
            }
            Console.WriteLine(Generator.generators.Count);
            
        }
       
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            types.Push(t);
            var rand = new Random();
            GeneratorContext context;

            context = new GeneratorContext(rand, t);
            if (Generator.Generate(context) != null)
            {
                types.Pop();
                return Generator.Generate(context);
            }

            var properties = t.GetProperties();
            var constructors = t.GetConstructors();
            var fields = t.GetFields();
            constructors = constructors.OrderByDescending(x => x.GetParameters().Count()).ToArray();
            var paramList = new List<object>();
            object obj = new object();

            if (constructors.Length != 0)
            {
                foreach (var constructor in constructors)
                {
                    foreach (var param in constructor.GetParameters())
                    {
                        context = new GeneratorContext(rand, param.ParameterType);
                        if (Generator.Generate(context) == null)
                        {
                            if (types.Contains(param.ParameterType))
                            {
                                continue;
                            }
                            paramList.Add(Create(param.ParameterType));
                            continue;
                        }
                        paramList.Add(Generator.Generate(context));
                    }
                    try
                    {
                        obj = Activator.CreateInstance(t, paramList.ToArray());
                        break;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            else
            {
                try
                {
                    obj = Activator.CreateInstance(t);
                }
                catch
                {
                    types.Pop();
                    return null;
                }
            }

            foreach (var property in properties)
            {
                context = new GeneratorContext(rand, property.PropertyType);
                if (Generator.Generate(context) == null)
                {
                    if (types.Contains(property.PropertyType))
                    {
                        continue;
                    }

                    try
                    {
                        property.SetValue(obj, Create(property.PropertyType));
                    }
                    catch
                    {
                        
                        types.Pop();
                        return null;
                    }
                    continue;
                }

                try
                {
                    property.SetValue(obj, Generator.Generate(context));
                }
                catch
                {
                    types.Pop();
                    return null;
                }
            }
            foreach (var field in fields)
            {
                context = new GeneratorContext(rand, field.FieldType);
                if (Generator.Generate(context) == null)
                {
                    if (types.Contains(field.FieldType))
                    {
                        continue;
                    }
                    try
                    {
                        field.SetValue(obj, Create(field.FieldType));
                    }
                    catch
                    {
                        types.Pop();
                        return null;
                    }
                    continue;
                }
                try
                {
                    field.SetValue(obj, Generator.Generate(context));
                }
                catch
                {
                    types.Pop();
                    return null;
                }
            }
            types.Pop();
            return obj;
        }
    }
}
