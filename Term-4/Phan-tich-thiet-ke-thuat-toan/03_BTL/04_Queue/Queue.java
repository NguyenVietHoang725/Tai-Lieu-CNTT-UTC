import java.io.*;
import java.util.Iterator;
import java.util.NoSuchElementException;

/**
 * Lớp {@code Queue} mô phỏng hàng đợi (FIFO - First In First Out)
 * với các thao tác thêm phần tử vào cuối và loại bỏ phần tử ở đầu.
 * 
 * <p>
 * Triển khai dựa trên danh sách liên kết đơn, với lớp Node lồng tĩnh.
 * Các thao tác như enqueue, dequeue, peek, size và isEmpty đều có độ phức tạp O(1).
 * </p>
 *
 * @param <Item> Kiểu dữ liệu phần tử trong hàng đợi
 */
public class Queue<Item> implements Iterable<Item> {
    private Node<Item> first; // con trỏ tới phần tử đầu hàng đợi
    private Node<Item> last;  // con trỏ tới phần tử cuối hàng đợi
    private int n;            // số lượng phần tử trong hàng đợi

    // Lớp con Node (tĩnh) mô tả một nút trong danh sách liên kết
    private static class Node<Item> {
        private Item item;
        private Node<Item> next;
    }

    /** Khởi tạo một hàng đợi rỗng */
    public Queue() {
        first = null;
        last  = null;
        n = 0;
    }

    /**
     * Kiểm tra hàng đợi có rỗng không
     *
     * @return true nếu rỗng, ngược lại false
     */
    public boolean isEmpty() {
        return first == null;
    }

    /**
     * Trả về số lượng phần tử hiện có trong hàng đợi
     *
     * @return số phần tử
     */
    public int size() {
        return n;
    }

    /**
     * Trả về phần tử ở đầu hàng đợi mà không loại bỏ nó
     *
     * @return phần tử đầu tiên trong hàng đợi
     * @throws NoSuchElementException nếu hàng đợi rỗng
     */
    public Item peek() {
        if (isEmpty()) throw new NoSuchElementException("Queue underflow");
        return first.item;
    }

    /**
     * Thêm một phần tử vào cuối hàng đợi
     *
     * @param item phần tử cần thêm
     */
    public void enqueue(Item item) {
        Node<Item> oldlast = last;
        last = new Node<>();
        last.item = item;
        last.next = null;
        if (isEmpty()) {
            first = last;
        } else {
            oldlast.next = last;
        }
        n++;
    }

    /**
     * Loại bỏ và trả về phần tử ở đầu hàng đợi
     *
     * @return phần tử đầu tiên đã bị loại bỏ
     * @throws NoSuchElementException nếu hàng đợi rỗng
     */
    public Item dequeue() {
        if (isEmpty()) throw new NoSuchElementException("Queue underflow");
        Item item = first.item;
        first = first.next;
        n--;
        if (isEmpty()) last = null; // tránh "loitering"
        return item;
    }

    /**
     * Trả về chuỗi biểu diễn các phần tử trong hàng đợi theo thứ tự FIFO
     *
     * @return chuỗi biểu diễn hàng đợi
     */
    public String toString() {
        StringBuilder s = new StringBuilder();
        for (Item item : this) {
            s.append(item).append(' ');
        }
        return s.toString();
    }

    /**
     * Trả về một iterator để duyệt hàng đợi theo thứ tự FIFO
     *
     * @return iterator
     */
    public Iterator<Item> iterator() {
        return new ListIterator(first);
    }

    // Lớp iterator để duyệt qua hàng đợi
    private class ListIterator implements Iterator<Item> {
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
     * Hàm main dùng để kiểm thử đơn vị cho lớp Queue
     * Đọc chuỗi từ file `tobe.txt`, nếu chuỗi là "-", thực hiện dequeue,
     * nếu không thì enqueue chuỗi đó.
     */
    public static void main(String[] args) throws IOException {
        System.setIn(new FileInputStream(new File("tobe.txt")));
        Queue<String> queue = new Queue<>();
        while (!StdIn.isEmpty()) {
            String item = StdIn.readString();
            if (!item.equals("-")) {
                queue.enqueue(item);
            } else if (!queue.isEmpty()) {
                StdOut.print(queue.dequeue() + " ");
            }
        }
        StdOut.println("(" + queue.size() + " left on queue)");
    }
}