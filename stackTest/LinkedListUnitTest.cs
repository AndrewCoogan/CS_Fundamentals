namespace utilities
{
    public class LinkedListUnitTest
    {
        private readonly LinkedList<int> LinkedListInt = new();
        private readonly LinkedList<string> LinkedListString = new();

        [SetUp]
        public void Setup() {

        }

        [Test, Order(1)]
        public void CanAddValues() {
            try {
                LinkedListInt.Add(1);
                LinkedListInt.Add(2);
                LinkedListInt.Add(3);
                LinkedListString.Add("Quick");
                LinkedListString.Add("Brown");
                LinkedListString.Add("Fox");
            }
            catch(Exception ex) {
                Assert.Fail(ex.Message);
            }

        }

        [Test, Order(2)]
        public void CanTellLength() {
            Assert.Multiple(() => {
                Assert.That(LinkedListInt.Length(), Is.EqualTo(3));
                Assert.That(LinkedListString.Length(), Is.EqualTo(3));
            });
        }

        [Test, Order(3)]  
        public void CanPopFrontValues() {
            Assert.Multiple(()=> {
                int frontPop = LinkedListInt.PopFront();
                Assert.That(frontPop, Is.EqualTo(1));
                Assert.That(LinkedListInt.Length(), Is.EqualTo(2));
            });
        }

        [Test, Order(4)]
        public void CanClearList() {
            LinkedListInt.Clear();
            Assert.That(LinkedListInt.Length(), Is.EqualTo(0));
        }

        [Test, Order(5)]
        public void CanGetSetOnIndex() {
            LinkedListString[2] = "Dog";
            string? backPop = LinkedListString.Pop();
            Assert.That(backPop, Is.EqualTo("Dog"));
        }

        [Test]
        public void CanFind() {
            Assert.Multiple(() =>
            {
                Assert.That(LinkedListString.Find("Quick"), Is.EqualTo(0));
                Assert.That(LinkedListString.Find("NotInList"), Is.Null);
            });
        }
    }
}