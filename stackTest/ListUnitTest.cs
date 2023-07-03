namespace utilities
{
    public class LinkedUnitTest
    {
        private readonly List<int> ListInt = new();
        private readonly List<string> ListString = new();
        private readonly List<int> ListIntLengthFive = new(5);
        private readonly List<string> AnotherList = new();


        [SetUp]
        public void Setup()
        {   

        }

        [Test]
        public void ListTest()
        {
            Assert.Multiple(() =>
            {
                // Add
                ListInt.Add(1);
                ListInt.Add(2);
                ListInt.Add(3);
                ListString.Add("Quick");
                ListString.Add("Brown");
                ListString.Add("Fox");
                AnotherList.Add("One sentence.");
                AnotherList.Add("Another sentence.");
                AnotherList.Add("Wow, now youre getting greedy with sentences.");

                // Length
                Assert.That(ListInt.Length, Is.EqualTo(3));

                // Contains
                Assert.That(ListInt.Contains(1), Is.True);
                Assert.That(ListInt.Contains(-1), Is.False);
                Assert.That(ListString.Contains("Quick"), Is.True);
                Assert.That(ListString.Contains("quick"), Is.False);

                // Filter
                Assert.That(ListInt.Filter(x => x % 2 == 0).Length(), Is.EqualTo(1));
                Assert.That(ListInt.Filter(x => x % 1 == 0).Length(), Is.EqualTo(3));
                Assert.That(ListInt.Filter(x => x % 10 == 0).Length(), Is.EqualTo(0));
                Assert.That(ListString.Filter(x => x == "Quick").Length(), Is.EqualTo(1));

                // Remove
                var newListInt = ListInt.Remove(1);
                Assert.That(newListInt!.Length(), Is.EqualTo(2));
                Assert.That(ListString.Remove("Fox")?.Length(), Is.EqualTo(2));
                newListInt.Remove(3, true);
                Assert.That(newListInt.Length(), Is.EqualTo(1));

                // Initalization
                Assert.That(ListIntLengthFive.Length(), Is.EqualTo(5));

                // Bracket Tests
                string firstElement = AnotherList[0]!;
                Assert.That(AnotherList[0], Is.EqualTo("One sentence."));
            });
        }
    }
}