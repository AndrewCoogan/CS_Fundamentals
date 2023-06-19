namespace utilities;

/*
https://thepythoncorner.com/posts/2020-08-21-hash-tables-understanding-dictionaries/
This is crazy:
A DOS attack (where DOS stands for Denial Of Service) is an attack where the resources of a 
computer system are deliberately exhausted by the attacker so that the system is no longer 
able to provide service to the clients. In this specific case of the attack demonstrated by 
Scott Crosby, the attack was possible flooding the target system with a lot of data whose 
hash collide, making the target system use a lot more of computing power to resolve the collisions.
*/

/// <summary>
/// Creating my own dictionay class, using my linked list class.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class DrewDictionary<TKey, TValue> {
    // VSCode was telling me to make this readonly, going with it for now.

    /*
    This is going to be an array of linked lists. The linked lists are going to be used to avoid
    and collisions that come up. The index of the array is going to be function on the hash of the key.

    To calculate the index within the buckets var I do the following: hash(key) % len(buckets)
    The modulo of the length of the buckets array is to ensure the range is a valid index.
    
    The way I am writing this as of 2:07 PM on Monday, it will be staticly sized, as in will not resize.
    
    I think I would like to do that, it sounds neat.

    Effectively, you can do this by creating a threshold, like 75%. This will require maintaining a number of used 
    buckets, checked before allocating new entry. When 75% of the buckets are used, a new buckets array is created 
    with 2*current capacity, and all elements of the current has table is re-hashed.
    
    This could be done easily with a pop first function, so it would be O(n) rather than O(n*log(n)).

    I am going to set the initalization size as 16.

    Q : Say you get a collision and you append, how do you tell the difference? Do you store the unhashed key too?
    A : That is correct, the index of the array is the hash of the key. The value in the node is the key value pair,
    so once you get the index of the array you run through the linked list to find the unhashed key.
    */
    private LinkedList<KeyValuePair<TKey, TValue>>[] buckets;
    private const int DefaultCapacity = 16;

    public DrewDictionary() {
        buckets = new LinkedList<KeyValuePair<TKey, TValue>>[DefaultCapacity];
    }

    private int GetBucketIndex(TKey key) {
        // Its asking me to establish key as non null, ill do that for now, but see post restart
        int hashCode = key!.GetHashCode();
        int bucketIndex = hashCode % buckets.Length;
        return bucketIndex;
    }

    public void Add(KeyValuePair<TKey, TValue> item) {
        // Find out what bucket has the key, if it exists.
        int bucketIndex = GetBucketIndex(item.Key);
        // TODO: This is where I will need to do resizing.

        LinkedList<KeyValuePair<TKey, TValue>> current = buckets[bucketIndex];
        bool newValue = true;
        int position = 0;

        while(position < current.Length() & newValue) {
            // The key already exists. Set it to the new value.
            if(EqualityComparer<TKey>.Default.Equals(current[position]!.Key, item.Key)) {
                current[position]!.Value = item.Value;
                newValue = false;
            }
        }

        if(newValue) {
            current.Add(item); // I am like 99% sure this changes he variable in place.
        }
    }

    public TValue? Get(TKey key) {
        // Find out what bucket has the key, if it exists.
        int bucketIndex = GetBucketIndex(key);

        // This will return the LinkedList for us to see if the specific key exits.
        LinkedList<KeyValuePair<TKey,TValue>> current = buckets[bucketIndex];

        // Loop through the linked list. If the linked list is empty, the length will be 0.
        for(int i = 0; i < current.Length(); i++){
            // This will return the value of the node.
            KeyValuePair<TKey, TValue>? keyValuePair = current[i];

            if(EqualityComparer<TKey>.Default.Equals(key, keyValuePair!.Key)) {
                // If the key is present, return the key.
                return keyValuePair.Value;
            }
        }
        return default;
    }
}