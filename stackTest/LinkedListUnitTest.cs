namespace utilities
{
    public class LinkedListUnitTest
    {
        private readonly LinkedList<int> LinkedListInt = new();
        private readonly LinkedList<string> LinkedListString = new();

        [SetUp]
        public void Setup()
        {   

        }

        [Test]
        public void LinkedListTest()
        {
            Assert.Multiple(() =>
            {
                // Add
                LinkedListInt.Add(1);
                LinkedListInt.Add(2);
                LinkedListInt.Add(3);
                LinkedListString.Add("Quick");
                LinkedListString.Add("Brown");
                LinkedListString.Add("Fox");

                // Length
                Assert.That(LinkedListInt.Length(), Is.EqualTo(3));
                Assert.That(LinkedListString.Length(), Is.EqualTo(3));

                // Pop
                int frontPop = LinkedListInt.PopFront();
                Assert.That(frontPop, Is.EqualTo(1));
                Assert.That(LinkedListInt.Length(), Is.EqualTo(2));

                // Clear
                LinkedListInt.Clear();
                Assert.That(LinkedListInt.Length(), Is.EqualTo(0));

                // By index get and set
                LinkedListString[2] = "Dog";
                string? backPop = LinkedListString.Pop();
                Assert.That(backPop, Is.EqualTo("Dog"));

                // Find
                Assert.That(LinkedListString.Find("Quick"), Is.EqualTo(0));
                Assert.That(LinkedListString.Find("NotInList"), Is.Null);
            });
        }
    }
}