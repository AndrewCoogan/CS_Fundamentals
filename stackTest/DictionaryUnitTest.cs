namespace utilities
{
    public class DictionaryUnitTest
    {
        private readonly Dictionary<string, int> myDictStrInt = new();

        [SetUp]
        public void Setup() {

        }

        [Test]
        public void CanAddValues() {
            try {
                myDictStrInt["quick"] = 1;
                myDictStrInt["brown"] = 2;
                myDictStrInt["fox"] = 3;
            }
            catch(Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void CanCount()
        {
            Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
        }

        [Test]
        public void CanRetrieveValues() {
            int brown = myDictStrInt["brown"];
            Assert.That(brown, Is.EqualTo(2));
        }

        [Test]
        public void CanNotFailWhenKeyNotFound() {
            int? jumped = myDictStrInt["jumped"];
            Assert.That(jumped, Is.EqualTo(default(int)));
        }
    }
}