namespace utilities;

//minimum requirements -- add, remove, find

public class LinkedList<T>
{
    // Default value is null, test if null for first value, then set.
    
    // I need to make the head public b/c its needed for the DrewDict
    // Not sure if this is kosher.
    private Node<T>? head;

    public void Add(T item) {
        Node<T> newNode = new(item);
        if (head == null) {
            head = newNode;
        } else {
            Node<T> current = head;
            while(current.Next != null) {
                current = current.Next;
            }
            current.Next = newNode;
        }
    }

    public T? Pop(){
        if(head == null) {
            /*
            Default returns whatever is defined in the class definition (T? in this case)
            If the object is nullable, then it will return null.
            Else, depends on the type. Int = 0, Bool = False.
            */
            return default;
        }

        if(head.Next == null) {
            // One element.
            T? value = head.Value;
            head = null;
            return value;
        }

        // More than one element. Know this is not null.
        Node<T> current = head;

        // This was initially ?, and has a similar effect as !
        // ? will only access the following member if it is non-null.
        // ! is a bit more explicit.
        while(current!.Next!.Next != null) {
            current = current.Next;  
        }

        // ! means that I am telling the compiler that I, the creator, am confident
        // that there are no null values being accessed.
        T? finalValue = current!.Next!.Value;
        current.Next = null;

        return finalValue;
    }

    public T? PopFront() {
        if(head == null) { return default; }

        T? value = head.Value;

        // VSCode rec, access head.Next if available, if not returns null?
        // null-coalescing operator
        // I took this bit off actually:  ?? null;
        // Since the default value of Next is null, we dont need to catch it.
        head = head.Next;

        return value;
    }

    public int Length() {
        return head == null ? 0 : LengthRecursive(head);
    }

    private int LengthRecursive(Node<T> node) {
        return node.Next == null ? 1 : LengthRecursive(node.Next) + 1;
    }

    // This and its recursive function dont have much of a use.
    // Bue here they are incase I change my mind.
    public int? Find(T ItemToFind) {
        // This will only find the first (lowest index value), if there are multiple.
        return FindRecursive(ItemToFind, head, 0);
    }

    private int? FindRecursive(T? ItemToFind, Node<T>? node, int index) {
        if(node == null) { return null; }

        // This is how you can compare two variables of generic types.
        if(EqualityComparer<T>.Default.Equals(ItemToFind, node.Value)) {
            return index;
        }

        // If not found, go to next index.
        return FindRecursive(ItemToFind, node.Next, index + 1);
    }

    // VS Code was getting me to try and use this notation: 
    // https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/#
    // That makes zero sense to me, so not going to implement.
    private T? GetAt(int index) {
        ValidateIndex(index);
        Node<T>? current = head;
        for(int i = 0; i < index; i++) {
            current = current!.Next;
        }
        return current!.Value;
    }

    private void SetAt(int index, T? value) {
        ValidateIndex(index);
        Node<T>? current = head;
        for(int i = 0; i < index; i++) {
            current = current!.Next;
        }
        current!.Value = value;
    }
    private void ValidateIndex(int index) {
        if(index < 0 || index > Length()) {
            throw new IndexOutOfRangeException();
        }
    }

    public T? this[int index] {
        // https://www.educative.io/answers/how-to-use-indexers-in-c-sharp
        // ^^^ following this guide
        get => GetAt(index);
        set => SetAt(index, value);
    }
}