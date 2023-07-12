namespace utilities;

public class KeyValuePair<TKey, TValue>
{
    public KeyValuePair(TKey key, TValue? value, int version = 0)
    {
        Key = key;
        Value = value;
        Version = version;
    }

    public TKey Key { get; }
    public TValue? Value { get; set; }
    public int Version { get; set; }
}