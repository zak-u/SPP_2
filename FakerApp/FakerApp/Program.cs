using System;
using Newtonsoft.Json;


namespace FakerApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Random random = new Random();
            Faker.Faker faker = new Faker.Faker();
            
          
            A AClass = faker.Create<A>();
            B BClass = faker.Create<B>();
            C CClass = faker.Create<C>();
           ;
            //Console.WriteLine(JsonConvert.SerializeObject(AClass, Formatting.Indented));
            
             
        }
    }
}
