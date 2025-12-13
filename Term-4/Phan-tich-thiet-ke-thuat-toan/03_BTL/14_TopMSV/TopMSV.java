import java.io.*;

public class TopMSV {

    // Không cho khởi tạo đối tượng
    private TopMSV() { }

    public static void main(String[] args) throws IOException {
        System.setIn(new FileInputStream(new File("students.txt")));

        int m = Integer.parseInt(args[0]);
        MinPQ<Student> pq = new MinPQ<>(m + 1);

        while (StdIn.hasNextLine()) {
            String line = StdIn.readLine();
            Student student = new Student(line); // Constructor phân tích dòng thành Student
            pq.insert(student);

            if (pq.size() > m) 
                pq.delMin();  // loại bỏ sinh viên có điểm thấp nhất trong top m
        }

        Stack<Student> stack = new Stack<>();
        for (Student s : pq)
            stack.push(s);

        while (!stack.isEmpty())
            StdOut.println(stack.pop());
    }
}
