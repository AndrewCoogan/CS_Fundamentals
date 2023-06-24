namespace utilities
{
    public class LinkedUnitTest
    {
        private readonly List<int> ListInt = new();
        private readonly List<string> ListString = new();

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

                // Length
                //Assert.That(ListInt.Length, Is.EqualTo(3));

                // Contains
                //Assert.That(ListInt.Contains(1), Is.True);
            });
        }
    }
}