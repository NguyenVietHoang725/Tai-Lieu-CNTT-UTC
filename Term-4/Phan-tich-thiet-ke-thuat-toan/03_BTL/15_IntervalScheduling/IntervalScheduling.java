import java.util.ArrayList;
import java.util.List;

public class IntervalScheduling {
    public static Job[] schedule(Job[] jobs) {
        // Dùng MinPQ để sắp xếp theo thời gian kết thúc tăng dần
        MinPQ<Job> pq = new MinPQ<>(jobs.length, (a, b) -> Integer.compare(a.finish, b.finish));
        for (Job job : jobs) {
            pq.insert(job);
        }

        List<Job> selected = new ArrayList<>();
        int lastFinishTime = -1;

        while (!pq.isEmpty()) {
            Job current = pq.delMin();
            if (current.start >= lastFinishTime) {
                selected.add(current);
                lastFinishTime = current.finish;
            }
        }

        return selected.toArray(new Job[0]);
    }

    // Hàm main để test đơn giản
    public static void main(String[] args) {
        Job[] jobs = {
            new Job( 0, 6, 60 ),
            new Job( 1, 4, 30 ),
            new Job( 3, 5, 10 ),
            new Job( 5, 7, 30 ),
            new Job( 5, 9, 50 ),
            new Job( 7, 8, 10 )
        };

        Job[] result = schedule(jobs);
        System.out.println("Selected jobs:");
        for (Job job : result) {
            System.out.println(job);
        }
    }
}
