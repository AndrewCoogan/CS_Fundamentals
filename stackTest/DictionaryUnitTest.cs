namespace utilities
{
    public class DictionaryUnitTest
    {
        private readonly Dictionary<string, int> myDictStrInt = new();
        private readonly Dictionary<string, int> myDictStrIntAdd = new();
        private readonly Dictionary<string, int> myDictStrIntBig = new();
        private const int bigDictMeasurement = 100;

        [SetUp]
        public void Setup() {

        }
        
        [Test, Order(1)]
        public void CanAddLotsOfValues() {
            for(int i = 0; i < bigDictMeasurement; i++) {
                myDictStrIntBig.Add(i.ToString(), i);
            }
        }

        [Test]
        public void CanBigDictsHaveGoodStats() {
            Assert.That(myDictStrIntBig.Count, Is.EqualTo(bigDictMeasurement));
        }

        [Test, Order(2)]
        public void CanStoreCorrectValues() {
            Assert.That(myDictStrIntBig["63"], Is.EqualTo(63));
        }

        [Test, Order(3)]
        public void CanGetCorrectValuesViaGet() {
            Assert.That(myDictStrIntBig.Get("63"), Is.EqualTo(63));
        }

        [Test]
        public void CanReplaceValuesAccurately() {
            myDictStrIntBig["13"] = 26;
            Assert.That(myDictStrIntBig["13"], Is.EqualTo(26));
        }

        [Test]
        public void CanAddValuesViaBrackets() {
            myDictStrInt["quick"] = 1;
            myDictStrInt["brown"] = 2;
            myDictStrInt["fox"] = 3;
        }

        [Test]
        public void CanAddValuesViaAddMethod() {
            myDictStrIntAdd.Add("quick", 1);
            myDictStrIntAdd.Add("brown", 2);
            myDictStrIntAdd.Add("fox", 3);
        }

        [Test]
        public void CanCount()
        {
            Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
        }

        [Test]
        public void CanCountAfterAdd()
        {
            Assert.That(myDictStrIntAdd.Count(), Is.EqualTo(3));
        }
        
        [Test]
        public void CanRetrieveValues() {
            int brown = myDictStrInt["brown"];
            Assert.That(brown, Is.EqualTo(2));
        }

        [Test]
        public void CanRetrieveValuesAfterAdd() {
            int brown = myDictStrIntAdd["brown"];
            Assert.That(brown, Is.EqualTo(2));
        }

        [Test]
        public void CanNotFailWhenKeyNotFound() {
            int? jumped = myDictStrInt["jumped"];
            Assert.That(jumped, Is.EqualTo(default(int)));
        }

        [Test]
        public void CanNotFailWhenKeyNotFoundAfterAdd() {
            int? jumped = myDictStrIntAdd["jumped"];
            Assert.That(jumped, Is.EqualTo(default(int)));
        }
    }
}