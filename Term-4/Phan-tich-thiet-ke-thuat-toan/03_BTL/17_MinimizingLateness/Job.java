import java.util.*;

// Lưu trữ thông tin một Job
class Job implements Comparable<Job> {
    int id;         // ID công việc (tùy chọn, dùng để theo dõi)
    int duration;   // Thời gian thực hiện
    int deadline;   // Deadline

    int start;      // Thời gian bắt đầu (sẽ được tính sau)
    int finish;     // Thời gian kết thúc (sẽ được tính sau)

    public Job(int id, int duration, int deadline) {
        this.id = id;
        this.duration = duration;
        this.deadline = deadline;
    }

    // Sắp xếp theo deadline tăng dần
    @Override
    public int compareTo(Job other) {
        return Integer.compare(this.deadline, other.deadline);
    }

    @Override
    public String toString() {
        return String.format("Job %d: Start=%d, Finish=%d, Deadline=%d, Lateness=%d",
                id, start, finish, deadline, Math.max(0, finish - deadline));
    }
}
