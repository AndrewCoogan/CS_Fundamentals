using System.ComponentModel;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Xml;

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
/// Creating my own dictionay class, using my list class.
/// </summary>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TValue"></typeparam>
public class Dictionary<TKey, TValue> {
    // VSCode was telling me to make this readonly, going with it for now.

    /*
    This is going to be list of lists. The lists are going to be used to avoid
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
    so once you get the index of the array you run through the list to find the unhashed key.

    This is a really infomative link:
    https://dotnetos.org/blog/2022-03-28-dictionary-implementation/

    Readonly means you can change it you cant make a new thing in its place.
    */
    protected const int DefaultCapacity = 16;
    private const double MaxCapacityRatio = 0.75;
    private List<List<KeyValuePair<TKey, TValue>>> buckets = new(DefaultCapacity);
    private List<List<KeyValuePair<TKey, TValue>>> newBuckets = new(DefaultCapacity);
    private int _count = 0;

    public int Count() => _count;

    public void Print() {
        for(int i = 0; i < Math.Ceiling((double)_count / DefaultCapacity) * DefaultCapacity; i++) {
            if(buckets[i] is not null) {
                for(int j = 0; j < buckets[i]!.Length(); j++) {
                    Console.WriteLine("{0} : {1}", buckets![i]![j]?.Key?.ToString(), buckets![i]![j]?.Value?.ToString());
                }
            }
        }
    }

    private void Resize() {
        // Make a new one longer.
        newBuckets = new List<List<KeyValuePair<TKey, TValue>>>(buckets.Capacity() + DefaultCapacity);

        _count = 0;
        for(int i = 0; i < buckets.Capacity(); i++) {
            for(int j = 0; j < buckets[i]?.Length(); j++) {
                AddToDict(buckets[i]![j]!, true);
            }
        }
        buckets = newBuckets;
    }

    private int GetBucketIndex(TKey key, int capacity = 0) {
        if(capacity == 0) capacity = buckets.Capacity();
        return Math.Abs(key!.GetHashCode() % capacity);
    }

    /// <summary>
    /// 
    /// This is a method that adds an element to the sub list, a list within a bucket.
    /// It makes the hash of the key, and gets the bucket location.
    /// If its empty then we make a new list of kvp's.
    /// If its a new list, then the loop immediately skips and adds the kvp to the list.
    /// If the list is populated, we need to overwrite the value.
    /// 
    /// </summary>
    /// <param name="item"></param>
    private void AddToDict(KeyValuePair<TKey, TValue> item, bool assignInNewBuckets = false)
    {
        // Console.WriteLine("Adding kvp: {0} - {1}", item.Key!.ToString(), item.Value?.ToString());
        // Find out what bucket has the key, if it exists.
        int bucketIndex = GetBucketIndex(item.Key, assignInNewBuckets ? newBuckets.Capacity() : 0);
        bool resize = ((double)_count / buckets.Capacity() ) > MaxCapacityRatio;
        if(resize & !assignInNewBuckets) Resize();

        // This can be null, if we have not accessed it yet.
        List<KeyValuePair<TKey, TValue>> current = assignInNewBuckets ?
            newBuckets[bucketIndex] ?? new List<KeyValuePair<TKey, TValue>>() : 
            buckets[bucketIndex] ?? new List<KeyValuePair<TKey, TValue>>();

        bool newValue = true;
        int position = 0;

        // Here we are in the sub list, ideally of length 1.
        while (position < current.Length() & newValue) {
            // The key already exists. Set it to the new value.
            if(EqualityComparer<TKey>.Default.Equals(current[position]!.Key, item.Key)) {
                current[position]!.Value = item.Value;
                newValue = false;
                }
            position++;
        }

        // I am attamting to add the element to the end of the bucket in the buckets list.
        // I think this should add by reference.
        if(newValue) {
            //Console.WriteLine("Key: {0}, Value: {1} is being added.", item.Key.ToString(), item.Value?.ToString());
            current.Add(item);
            _count++;
        }

        List<List<KeyValuePair<TKey, TValue>>> targetBuckets = assignInNewBuckets ? newBuckets : buckets;
        targetBuckets[bucketIndex] = current;
    }

    // It is forgiving and will return a null if key is not present.
    public TValue? Get(TKey key)
    {
        // Find out what bucket has the key, if it exists.
        int bucketIndex = GetBucketIndex(key);

        List<KeyValuePair<TKey, TValue>> current = buckets[bucketIndex]!;

        if (current is not null) {
            // Loop through the list. If the list is empty, the length will be 0.
            for (int i = 0; i < current.Length(); i++) {
                // This will return the value of the node.
                KeyValuePair<TKey, TValue> kvp = current[i]!;

                if (EqualityComparer<TKey>.Default.Equals(key, kvp.Key)) {
                    // If the key is present, return the value.
                    return kvp.Value;
                }
            }
        }

        return default;
    }

    // Took the lead from my List code on this one.
    // Will allow for DrewDict[obj] = obj2
    // or xxx = DrewDict[obj]
    private TValue? GetAt(TKey key) => Get(key);

    public void Add(TKey key, TValue value) => 
        AddToDict(new KeyValuePair<TKey, TValue>(key, value));

    public TValue? this[TKey index] {
        get => GetAt(index);
        set => AddToDict(new KeyValuePair<TKey, TValue>(index, value!));
    }

    public int GetRawBucket(TKey key) => GetBucketIndex(key);
}