namespace utilities
{
    public class DictionaryUnitTest
    {
        private readonly Dictionary<string, int> myDictStrInt = new();

        [SetUp]
        public void Setup()
        {
            myDictStrInt["quick"] = 1;
            myDictStrInt["brown"] = 1;
            myDictStrInt["fox"] = 1;
        }

        [Test]
        public void ListTest()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
            });
        }
    }
}