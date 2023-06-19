namespace utilities;

public class KeyValuePair<TKey, TValue>
{
    public required TKey Key { get; set; }
    public TValue? Value { get; set; }
}