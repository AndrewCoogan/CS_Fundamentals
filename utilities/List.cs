using System.Diagnostics.CodeAnalysis;

namespace utilities;

//minimum requirements -- add, remove, find

public class List<T>
{
    // emptyArray will never change, static readonly will make that set.
    private static readonly T[] _emptyList = new T[0];

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
        _list = _emptyList;
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

    // This is going to add a singular item to the list.
    public void Add(T item) {
        // Arrays are fixed length, so need to add to the size
        // I need this to be a new array for the array copy to work.
        T[] newList = new T[_list.Length + 1];
        
        // We can use Array.Copy to, well, copy the list, just make it one longer.
        Array.Copy(_list, newList, newList.Length);

        // Add the item.
        newList[_list.Length] = item;

        // Set the internal list as the new, one longer, list.
        _list = newList;
    }

    // This is a filter method that takes in a predicate (x => x % 2 == 0), and returns a list.
    // Predicates use lambda functions or ananomous functions, so by doing match(value), what were
    // really doing is func(value) => value % 2 == 0 => true/false
    // If its true, we want it in the result, if not, we dont.
    // This is not an inplace operation and returns a new instance of a list.
     public List<T> Filter([NotNull]Predicate<T> match) {
        List<T> outputList = new(); 
        for(int i = 0 ; _list != null && i < _list.Length; i++) {
            // && ensures the LHS is true before doing the RHS
            if(_list[i] != null && match(_list[i])) {
                outputList.Add(_list[i]);
            }
        }
        return outputList;
    }

    private T? GetAt(int index) => 
        (index < 0 || index >= _list.Length) ? 
        throw new ArgumentOutOfRangeException(nameof(index)) : _list[index];

    private void SetAt(int index, T? value) {
        if(index < 0) {
            throw new ArgumentOutOfRangeException(nameof(index));
        } else if(index < _list.Length) {
            _list[index] = value; // I dont see why we cant add a possible null value.
        } else {
            T[] newList = new T[index + 1];
            Array.Copy(_list, newList, newList.Length);
            if(value != null) { newList[index] = value; }
            _list = newList;
        }
    }

    // I can add the NotNull attribute to throw a null exception error if a null is passes.
    public T? this[[NotNull]int index] {
        get => GetAt(index);
        set => SetAt(index, value);
    }
}