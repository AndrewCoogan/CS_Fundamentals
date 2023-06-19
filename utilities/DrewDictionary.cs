namespace utilities;

public class DrewDictionary<TKey, TValue> {
    // VSCode was 
    private readonly LinkedList<KeyValuePair<TKey, TValue>>? dict;

    public void Add(KeyValuePair<TKey, TValue> item) {
        // Is this the right way to be doing this?
        // It feels weird having to constantly tell the compiler that this is not going to be null.
        
        //I need to see if the key exists already.

        dict!.Add(item);
    }

    private bool ContainsKey(string key) {
        if(dict == null) { return false; }

        // This operation is O(n), which we know is inefficient.
        // Need to think on better way of doing this.

    }

    public void RemoveByKey(string key) {
        // There can only be one key in the list.
    }
    

}