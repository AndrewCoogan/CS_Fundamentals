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

            myDictStrInt["quick"] = 1;
            myDictStrInt["brown"] = 1;
            myDictStrInt["fox"] = 1;

            // This is just to maeke sure i dont have a hash collision off the bat.
            Console.WriteLine("Key1 : " + myDictStrInt.GetRawBucket(key1));
            Console.WriteLine("Key2 : " + myDictStrInt.GetRawBucket(key2));
            Console.WriteLine("Key3 : " + myDictStrInt.GetRawBucket(key3));
        }

        [Test]
        public void DictionaryTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
                var brown = myDictStrInt["brown"];
                Assert.That(brown, Is.EqualTo(1));
                int? jumped = myDictStrInt["jumped"];
                Assert.That(jumped, Is.Null);
            });
        }
    }
}