import java.util.Arrays;
import java.util.List;
import java.util.Collections;

public class MinimizingLateness {

    public static void minimizeLateness(List<Job> jobs) {
        // Sắp xếp theo deadline tăng dần
        Collections.sort(jobs);

        int currentTime = 0;

        for (Job job : jobs) {
            job.start = currentTime;
            job.finish = currentTime + job.duration;
            currentTime = job.finish;
        }

        // In kết quả và độ trễ tối đa
        int maxLateness = 0;
        for (Job job : jobs) {
            System.out.println(job);
            maxLateness = Math.max(maxLateness, job.finish - job.deadline);
        }

        System.out.println("Maximum Lateness = " + maxLateness);
    }

    public static void main(String[] args) {
        List<Job> jobs = Arrays.asList(
            // Ví dụ không có độ trễ
            //new Job(1, 4, 10),  // Job 1: t = 4, deadline = 10
            //new Job(2, 3, 8),   // Job 2: t = 3, deadline = 8
            //new Job(3, 2, 15),  // Job 3: t = 2, deadline = 15
            //new Job(4, 1, 5)    // Job 4: t = 1, deadline = 5
            
            // Ví dụ có độ trễ
            new Job(1, 3, 4),   // JobID = 1, t = 3, d = 4
            new Job(2, 2, 5),
            new Job(3, 4, 8),
            new Job(4, 1, 6)
        );

        minimizeLateness(jobs);
    }
}
