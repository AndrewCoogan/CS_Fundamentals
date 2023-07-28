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
        public void CanAddValuesAsExpected() {
            myDictStrInt["quick"] = 1;
            myDictStrInt["brown"] = 2;
            myDictStrInt["fox"] = 3;
            myDictStrIntAdd.Add("quick", 1);
            myDictStrIntAdd.Add("brown", 2);
            myDictStrIntAdd.Add("fox", 3);
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
        public void CanGetCorrectValues() {
            Assert.That(myDictStrIntBig["63"], Is.EqualTo(63));
        }

        [Test, Order(2)]
        public void CanGetCorrectValuesViaGet() {
            Assert.That(myDictStrIntBig.Get("63"), Is.EqualTo(63));
        }

        [Test]
        public void CanReplaceValuesAccurately() {
            myDictStrIntBig["13"] = 26;
            Assert.That(myDictStrIntBig["13"], Is.EqualTo(26));
        }

        [Test]
        public void CanCount()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrInt.Count(), Is.EqualTo(3));
                Assert.That(myDictStrIntAdd.Count(), Is.EqualTo(3));
            });
        }

        [Test]
        public void CanRetrieveValues()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrInt["brown"], Is.EqualTo(2));
                Assert.That(myDictStrInt.Get("brown"), Is.EqualTo(2));
            });
        }

        [Test]
        public void CanRetrieveValuesAfterAdd()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrIntAdd["brown"], Is.EqualTo(2));
                Assert.That(myDictStrIntAdd.Get("brown"), Is.EqualTo(2));
            });
        }

        [Test]
        public void CanNotFailWhenKeyNotFound()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrInt["jumped"], Is.EqualTo(default(int)));
                Assert.That(myDictStrInt.Get("jumped"), Is.EqualTo(default(int)));
            });
        }

        [Test]
        public void CanNotFailWhenKeyNotFoundAfterAdd()
        {
            Assert.Multiple(() =>
            {
                Assert.That(myDictStrIntAdd["jumped"], Is.EqualTo(default(int)));
                Assert.That(myDictStrIntAdd.Get("jumped"), Is.EqualTo(default(int)));
            });
        }
    }
}