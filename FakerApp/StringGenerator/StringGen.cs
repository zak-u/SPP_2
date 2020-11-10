using System;
using System.Text;
using FakerLib;
namespace StringGenerator
{
    public class StringGen : IGenerator
    {
        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

        public object Generate(GeneratorContext context)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            int stringLength = context.Random.Next(10, 50);

            StringBuilder resultString = new StringBuilder(stringLength);
            for (int i = 0; i < stringLength; i++)
            {
                resultString.Append(chars[context.Random.Next(chars.Length)]);
            }
            return resultString.ToString();
        }
    }
}
