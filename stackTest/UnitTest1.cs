namespace stackTest;

using utilities;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void LinkedListTest()
    {
        var linkedList = new utilities.LinkedList<int>();
        linkedList.Add(1);
        linkedList.Add(2);
        linkedList.Add(3);
        Assert.That(linkedList.Length(), Is.EqualTo(2));
    }
}