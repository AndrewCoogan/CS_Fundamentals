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
            T value = head.Value;
            head = null;
            return value;
        }

        // Mode than one element.
        Node<T> current = head;

        while(current.Next.Next != null) {
            current = current.Next;
        }

        T finalValue = current.Next.Value;
        current.Next = null;

        return finalValue;
    }
}