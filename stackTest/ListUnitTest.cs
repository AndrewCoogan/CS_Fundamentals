using System.Numerics;
using System.Reflection.Metadata;

namespace utilities
{
    public class LinkedUnitTest
    {
        private const int SMALL = 16; // This should be DefaultScaler in the list.
        private const int LARGE = 1000;

        private readonly List<int> ListInt = new();
        private readonly List<string> ListString = new();
        private readonly List<int> ListIntLengthFive = new(5);
        private readonly List<string> AnotherList = new();
        private readonly List<string> AShortList = new(SMALL);
        private readonly List<int> ALongList = new();

        [SetUp]
        public void Setup() {   

        }

        [Test]
        public void CanAddValues() {
            try {
                ListInt.Add(1);
                ListInt.Add(2);
                ListInt.Add(3);
                ListString.Add("Quick");
                ListString.Add("Brown");
                ListString.Add("Fox");
                AnotherList.Add("One sentence.");
                AnotherList.Add("Another sentence.");
                AnotherList.Add("Wow, now youre getting greedy with sentences.");

                for(int i = 0; i < 1000; i++) {
                    ALongList.Add(i);
                }
            }
            catch(Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void CanTellLength() {
            Assert.That(ListInt.Length(), Is.EqualTo(3));
        }

        [Test]
        public void CanTellLenthOfEmptyList() {
            Assert.That(ListIntLengthFive.Length(), Is.EqualTo(0));
        }

        [Test]
        public void CanFlagContainers() {
            Assert.Multiple(() => {
                Assert.That(ListInt.Contains(1), Is.True);
                Assert.That(ListInt.Contains(-1), Is.False);
                Assert.That(ListString.Contains("Quick"), Is.True);
                Assert.That(ListString.Contains("quick"), Is.False);
            });
        }

        [Test]
        public void CanFilter() {
            Assert.Multiple(() => {
                Assert.That(ListInt.Filter(x => x % 2 == 0).Length(), Is.EqualTo(1));
                Assert.That(ListInt.Filter(x => x % 1 == 0).Length(), Is.EqualTo(3));
                Assert.That(ListInt.Filter(x => x % 10 == 0).Length(), Is.EqualTo(0));
                Assert.That(ListString.Filter(x => x == "Quick").Length(), Is.EqualTo(1));
            });
        }

        [Test]
        public void CanRemove() {
            Assert.Multiple(() => {

                var newListInt = ListInt.Remove(1);
                Assert.That(newListInt!.Length(), Is.EqualTo(2));
                Assert.That(ListString.Remove("Fox")?.Length(), Is.EqualTo(2));
                newListInt.Remove(3, true);
                Assert.That(newListInt.Length(), Is.EqualTo(1));
            });
        }

        [Test]
        public void CanUseBracketsForAssignment() {
            Assert.That(AnotherList[0], Is.EqualTo("One sentence."));
        }

        [Test]
        public void CanCalculateCapacityCorrectly()
        {
            Assert.Multiple(() =>
            {
                Assert.That(AShortList.Capacity(), Is.EqualTo(16));
                Assert.That(ALongList.Capacity(), Is.EqualTo(SMALL * ((LARGE / SMALL) + 1)));
                // This looks weird, but / of two ints rounds down, so cant simplify easily.
            });
        }
    }
}