using System;
using FakerLib;
using Newtonsoft.Json;


namespace FakerApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Generator gen = new Generator();
            gen.AddGenerator(new IntGen());
            gen.AddGenerator(new LongGen());
            gen.AddGenerator(new DoubleGen());
            gen.AddGenerator(new FloatGen());
            gen.AddGenerator(new ListGen());
            gen.AddGenerator(new BoolGen());
            gen.LoadGenerators("/Users/admin/Projects/SPP_2/FakerApp/Plugins");
           
            Faker.Faker faker = new Faker.Faker(gen);
            A AClass = faker.Create<A>();
            B BClass = faker.Create<B>();
            C CClass = faker.Create<C>();
            
            Console.WriteLine(JsonConvert.SerializeObject(BClass, Formatting.Indented));
        }
    }
}
