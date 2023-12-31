using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualBasic;

namespace utilities;

//minimum requirements -- add, remove, find

public class List<T>
{
    // emptyArray will never change, static readonly will make that set.
    private static readonly T[] _emptyList = new T[0];
    private int _length = 0;
    private const int DefaultScaler = 16;

    // I need to initialize my actual list.
    private T[] _list;
    
    /*
    I am going to make a couple of initializations here.
    Overall, this is going to be similar to the actual list implementation, there is not a ton getting 
    around that, but I am going to make sure that I understand why these are implemented as they are.

    List() -> Empty initialization. Returns a empty array with length zero.

    List(int length) -> Returns an empty array with a defined length. 
    Valid indicies will go from 0 -> length-1?

    List(IEnumerable<T> collection) -> This one is a bit trickier.
    The idea behind this one is to allow for the common initialization: List() {1,2,3}
    {} is how you denote an array in this language. 
    The data types for this one are pretty unfamiliar, so I think I am going to move on for now.
    */
    public List() {
        _list = new T[DefaultScaler];    
    }
    public List(int length) {
        if(length > 0) {
            _list = new T[length];
        } else if(length == 0) {
            _list = _emptyList;
        } else {
            throw new ArgumentOutOfRangeException(nameof(length));
        }
    }

    public int Length() => _length;
    public int Capacity() => _list.Length;

    public List(int length, T? defaultValue) {
        List<T> newList = new(length);
        for(int i = 0; i < length; i++) {
            newList[i] = defaultValue; 
        }
        _list = newList.ToArray();
    }

    // This is going to add a singular item to the list.
    public void Add(T item) {        
        /*
        Copy creates a new list and copies it element by element. O(n)
        Resize is going to make the list longer.
        "REF" is passing in _list by referene, meaning its changing the array in place.
        */

        //Console.WriteLine("Length: {0}", _length.ToString());

        if(_length >= _list.Length) Resize(_list.Length + DefaultScaler);

        _list[_length] = item;
        _length++;

        /*
        Here's a breakdown of the syntax:
            ^ indicates the start of the index from the end syntax.
            1 represents the offset from the end of the collection. 
                
            So ^1 returns the last element.
            _list[^1] = item;
        */
    }

    public void Resize(int length) {
        try {
            Array.Resize(ref _list, length);
        }
        catch (Exception e) {
            throw new ArgumentOutOfRangeException(e.ToString());
        }
    }

    // This is a filter method that takes in a predicate (x => x % 2 == 0), and returns a list.
    // Predicates use lambda functions or ananomous functions, so by doing match(value), what were
    // really doing is func(value) => value % 2 == 0 => true/false
    // If its true, we want it in the result, if not, we dont.
    // This is not an inplace operation and returns a new instance of a list.
    private List<T> Filter(Predicate<T> match, bool just_first) {
        // This is making a new instance of a list, no need to worry about count.
        List<T> outputList = new(); 
        for(int i = 0 ; _list != null && i < _length; i++) {
            // && ensures the LHS is true before doing the RHS
            if(_list[i] != null && match(_list[i])) {
                outputList.Add(_list[i]);
                if(just_first) { return outputList; }
            }
        }
        return outputList;
    }

    public bool Contains(T item) {
        bool match(T X) => EqualityComparer<T>.Default.Equals(item, X);
        return Filter(match, true).Length() > 0;
    }

    public List<T> Filter(Predicate<T> match) => Filter(match, false);

    public List<T>? Remove(T item, bool inplace = false)  {
        bool retrurnNull = inplace;
        List<T> filteredData = Filter(X => !EqualityComparer<T>.Default.Equals(item, X), false);
        if(inplace) {
            // It is so awkward to have these exposed.
            _length = filteredData.Length();
            _list = filteredData.ToArray();
        }
        return retrurnNull ? null : filteredData;
    }

    public T[] ToArray() {
        T[] array = new T[Length()];
        Array.Copy(_list, 0, array, 0, array.Length);
        return array;
    }

    // This is kinda copied from the source, it would just make some of what comes later a little easier.
    public void ForEach(Action<T> action) {
        if (action is null) throw new ArgumentNullException(nameof(action));

        for (int i = 0; i < _length; i++) {
            action(_list[i]);
        }
    }

    private T? GetAt(int index) {
        if(index < 0 || index >= _list.Length) {
            throw new ArgumentOutOfRangeException(nameof(index));
        } 
        return _list[index];
    }

    private void SetAt(int index, T value) {
        if(value is null) {
            throw new ArgumentNullException(nameof(index));
        } else if(index < 0) {
            throw new ArgumentOutOfRangeException(nameof(index));
        } else if(index < _list.Length) {
            _length = index + 1;
        } else {
            Resize(index);
        }
        _list[index] = value;
    }

    public T? this[int index] {
        get => GetAt(index);
        set => SetAt(index, value!);
    }
}