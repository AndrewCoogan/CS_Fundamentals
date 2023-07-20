namespace utilities
{
    public class DictionaryUnitTest
    {
        private readonly Dictionary<string, int> myDictStrInt = new();

        [SetUp]
        public void Setup()
        {
            string key1 = "quick";
            string key2 = "brown";
            string key3 = "fox";

            myDictStrInt[key1] = 1;
            myDictStrInt[key2] = 1;
            myDictStrInt[key3] = 1;

            Console.WriteLine("Key1 : " + myDictStrInt.GetRawBucket(key1));
            Console.WriteLine("Key2 : " + myDictStrInt.GetRawBucket(key2));
            Console.WriteLine("Key3 : " + myDictStrInt.GetRawBucket(key3));
        }

        [Test]
        public void DictionaryTest()
        {
            Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
            var brown = myDictStrInt["brown"];
            Assert.That(brown, Is.EqualTo(1));
            int? jumped = myDictStrInt["jumped"];
            Assert.That(jumped, Is.EqualTo(default(int)));
        }
    }
}