namespace utilities;

public class LinkedList<T> {
    // Default value is null, test if null for first value, then set.
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
            // No elements.
            Console.WriteLine("List is empty.");
            // It returns the default value when the object is not initialized.
            return default;
        }

        if(head.Next == null) {
            // One element.
            T? value = head.Value;
            head = null;
            return value;
        }

        // Mode than one element.
        // Know this is not null.
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

        return finalValue == null ? default : finalValue;
    }

    public int Length() {
        return head == null ? 0 : LengthRecursive(head);
    }

    private int LengthRecursive(Node<T> node) {
        return node.Next == null ? 0 : LengthRecursive(node.Next) + 1;
    }
}