import java.util.Arrays;
import java.util.List;
import java.util.Collections;

public class IntervalPartitioning {

    // Hàm trả về số phòng tối thiểu cần dùng
    public static int minRoomsRequired(List<Lecture> lectures) {
        // Bước 1: Sắp xếp bài giảng theo thời gian bắt đầu
        Collections.sort(lectures);

        // Bước 2: MinPQ để lưu thời gian kết thúc các phòng đang dùng
        MinPQ<Integer> pq = new MinPQ<>();

        int d = 0;  // số phòng đã cấp phát

        for (Lecture lec : lectures) {
            // Nếu có phòng kết thúc trước hoặc đúng lúc bài giảng bắt đầu
            if (!pq.isEmpty() && pq.min() <= lec.start) {
                pq.delMin();  // Giải phóng phòng đó (dùng cho bài mới)
            } else {
                // Không có phòng rảnh, cấp thêm phòng mới
                d++;
            }
            // Gán bài giảng này cho phòng (mới hoặc đã rảnh)
            pq.insert(lec.finish);
        }

        // d chính là số phòng tối thiểu
        return d;
    }

    public static void main(String[] args) {
        List<Lecture> lectures = Arrays.asList(
            new Lecture(0, 30),
            new Lecture(5, 10),
            new Lecture(15, 20),
            new Lecture(25, 35),
            new Lecture(30, 40),
            new Lecture(32, 50),
            new Lecture(45, 60),
            new Lecture(55, 70),
            new Lecture(65, 80),
            new Lecture(75, 90),
            new Lecture(85, 100)
        );

        int result = minRoomsRequired(lectures);
        System.out.println("Minimum number of rooms required = " + result);
    }
}