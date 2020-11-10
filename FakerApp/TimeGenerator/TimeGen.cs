using System;
using FakerLib;
namespace TimeGenerator
{
    public class TimeGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime);
        }

        public object Generate(GeneratorContext context)
        {
            int year = context.Random.Next(1970, 2021);
            int month = context.Random.Next(1, 13);
            int day = context.Random.Next(1, 29);
            int hours = context.Random.Next(1, 13);
            int minutes = context.Random.Next(1, 60);
            int seconds = context.Random.Next(1, 60);
            DateTime dateTime = new DateTime(year, month, day, hours, minutes, seconds);
            return dateTime;
        }
    }
}
