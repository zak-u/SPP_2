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
        public Generator gen;
        private Stack<Type> types = new Stack<Type>();
        
        public Faker(Generator Gen)
        {
            gen = Gen;
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        private object Create(Type t)
        {
            types.Push(t);
            var rand = new Random();
            GeneratorContext context = new GeneratorContext(rand, t, gen);
            
            if (gen.CanGenerate(t) == true)
            {
                types.Pop();
                return gen.Generate(context);
            }

            var properties = t.GetProperties();//свойства
            var constructors = t.GetConstructors();
            var fields = t.GetFields();//поля
            constructors = constructors.OrderByDescending(x => x.GetParameters().Count()).ToArray();
            var paramList = new List<object>();
            object obj = new object();

            if (constructors.Length != 0)
            {
                foreach (var constructor in constructors)
                {
                    foreach (var param in constructor.GetParameters())
                    {
                        context = new GeneratorContext(rand, param.ParameterType, gen);
                        if (gen.CanGenerate(param.ParameterType) == false)
                        {
                            if (types.Contains(param.ParameterType))
                            {
                                continue;
                            }
                            paramList.Add(Create(param.ParameterType));
                            continue;
                        }
                        paramList.Add(gen.Generate(context));
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
            else//Эта проверка ветка нужна, т.к. у структур(в частности C) нет конструкторов по умолчанию, и нам нужно создать объект, чтобы заполнить в нем поля
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
                context = new GeneratorContext(rand, property.PropertyType, gen);
                if (gen.CanGenerate(property.PropertyType) == false)
                {
                    if (types.Contains(property.PropertyType))
                    {
                        continue;
                    }
                    //Эта проверка нужна, если возникнет исключение (класс С, поле Dictionary)
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
                property.SetValue(obj, gen.Generate(context));
               
            }
            foreach (var field in fields)
            {
                context = new GeneratorContext(rand, field.FieldType, gen);
                
                if (gen.CanGenerate(field.FieldType) == false)
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
                field.SetValue(obj, gen.Generate(context));
                
            }
            types.Pop();
            return obj;
        }
    }
}
