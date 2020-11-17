using FakerLib;
using NUnit.Framework;

using System;
namespace FakerTest
{
    [TestFixture()]
    public class Test
    {
        public static Faker.Faker Setup()
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
            return faker;
        }
        
         Faker.Faker faker = Setup();
        
        public class A
        {
            public string StrVal { get; set; }
        }
        public class B
        {
            public A AVal;
        }

        class PrivateFieldsClass
        {
            private int IntValue;
            private bool flag = false;

            public PrivateFieldsClass(int data)
            {
                IntValue = data;
                flag = true;
            }
            public bool IsSetData()
            {
                return flag;
            }
        }

        class TwoConstuctors
        {
            private int Data;
            public bool constructor1 = false;
            private bool constructor2 = false;

            public TwoConstuctors()
            {
                constructor1 = true;
            }
            public TwoConstuctors(int data)
            {
                Data = data;
                constructor2 = true;
            }
            public bool IsSetData()
            {
                return constructor2;
            }
        }
        public class Rec1
        {
            public Rec2 A;
        }

        public class Rec2
        {
            public Rec1 B;
        }

        class PrivateConstructor
        {
            private PrivateConstructor()
            {

            }
        }
        [Test()]
        public void StringPropertyTest()
        {
            A A = faker.Create<A>();
            Assert.AreEqual(true, A.StrVal.Length > 1);
        }

        [Test()]
        public void ObjectFieldTest()
        {
            B BVal = faker.Create<B>();
            Assert.AreNotEqual(null, BVal.AVal);
        }

        [Test()]
        public void PrivateFieldTest()
        {
            PrivateFieldsClass PrFields = faker.Create<PrivateFieldsClass>();
            Assert.AreEqual(true, PrFields.IsSetData());
        }

        [Test()]
        public void TwoConstructorsTest()
        {
            TwoConstuctors twoconstructor = faker.Create<TwoConstuctors>();
            Assert.AreEqual(true, twoconstructor.IsSetData());
        }

        [Test()]
        public void ReccursionTest()
        {
            Rec1 rec = faker.Create<Rec1>();
            Assert.AreEqual(null, rec.A.B);
        }

        [Test()]
        public void PrivateConstructorTest()
        {
            PrivateConstructor obj = faker.Create<PrivateConstructor>();
            Assert.AreEqual(null, obj);
        }

        [Test()]
        public void SHouldGEnerateINt()
        {
            int i = faker.Create<int>();
            Assert.NotZero(i);
        }

       
    }
}
