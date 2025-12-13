import java.util.Iterator;
import java.util.NoSuchElementException;
import java.io.*;

/**
 * Lớp Stack mô phỏng cấu trúc dữ liệu ngăn xếp (LIFO - Last In First Out) sử dụng danh sách liên kết đơn.
 * Hỗ trợ các thao tác cơ bản: push, pop, peek, kiểm tra rỗng, lấy kích thước, và duyệt phần tử theo thứ tự LIFO.
 *
 * @param <Item> Kiểu dữ liệu của phần tử trong ngăn xếp
 */
public class Stack<Item> implements Iterable<Item> {
    private Node<Item> first;  // Phần tử đầu ngăn xếp (đỉnh stack)
    private int n;             // Số lượng phần tử trong ngăn xếp

    // Lớp Node đại diện cho một nút trong danh sách liên kết đơn
    private static class Node<Item> {
        private Item item;
        private Node<Item> next;
    }

    /**
     * Khởi tạo một ngăn xếp rỗng.
     */
    public Stack() {
        first = null;
        n = 0;
    }

    /**
     * Kiểm tra ngăn xếp có rỗng hay không.
     *
     * @return true nếu ngăn xếp rỗng, ngược lại false
     */
    public boolean isEmpty() {
        return first == null;
    }

    /**
     * Trả về số lượng phần tử hiện có trong ngăn xếp.
     *
     * @return số phần tử trong ngăn xếp
     */
    public int size() {
        return n;
    }

    /**
     * Thêm một phần tử vào đầu ngăn xếp.
     *
     * @param item phần tử cần thêm
     */
    public void push(Item item) {
        Node<Item> oldfirst = first;
        first = new Node<Item>();
        first.item = item;
        first.next = oldfirst;
        n++;
    }

    /**
     * Xóa và trả về phần tử ở đầu ngăn xếp (phần tử được thêm gần nhất).
     *
     * @return phần tử bị loại khỏi ngăn xếp
     * @throws NoSuchElementException nếu ngăn xếp rỗng
     */
    public Item pop() {
        if (isEmpty()) throw new NoSuchElementException("Stack underflow");
        Item item = first.item;
        first = first.next;
        n--;
        return item;
    }

    /**
     * Trả về (nhưng không loại bỏ) phần tử đầu ngăn xếp.
     *
     * @return phần tử ở đỉnh ngăn xếp
     * @throws NoSuchElementException nếu ngăn xếp rỗng
     */
    public Item peek() {
        if (isEmpty()) throw new NoSuchElementException("Stack underflow");
        return first.item;
    }

    /**
     * Trả về chuỗi đại diện cho ngăn xếp, liệt kê các phần tử theo thứ tự LIFO (từ đỉnh xuống).
     *
     * @return chuỗi biểu diễn ngăn xếp
     */
    public String toString() {
        StringBuilder s = new StringBuilder();
        for (Item item : this) {
            s.append(item);
            s.append(' ');
        }
        return s.toString();
    }

    /**
     * Trả về một iterator duyệt các phần tử trong ngăn xếp theo thứ tự LIFO.
     *
     * @return đối tượng iterator
     */
    public Iterator<Item> iterator() {
        return new ListIterator<Item>(first);
    }

    // Lớp iterator để duyệt ngăn xếp
    private class ListIterator<Item> implements Iterator<Item> {
        private Node<Item> current;

        public ListIterator(Node<Item> first) {
            current = first;
        }

        public boolean hasNext() {
            return current != null;
        }

        public void remove() {
            throw new UnsupportedOperationException();
        }

        public Item next() {
            if (!hasNext()) throw new NoSuchElementException();
            Item item = current.item;
            current = current.next;
            return item;
        }
    }

    /**
     * Hàm main để kiểm thử Stack bằng cách đọc dữ liệu từ file "tobe.txt".
     * Mỗi từ được thêm vào stack trừ khi là dấu "-", khi đó sẽ pop phần tử ra và in ra màn hình.
     *
     * @param args không sử dụng
     * @throws IOException nếu lỗi đọc file
     */
    public static void main(String[] args) throws IOException {
        System.setIn(new FileInputStream(new File("tobe.txt")));
        Stack<String> stack = new Stack<String>();
        while (!StdIn.isEmpty()) {
            String item = StdIn.readString();
            if (!item.equals("-"))
                stack.push(item);
            else if (!stack.isEmpty())
                StdOut.print(stack.pop() + " ");
        }
        StdOut.println("(" + stack.size() + " left on stack)");
    }
}