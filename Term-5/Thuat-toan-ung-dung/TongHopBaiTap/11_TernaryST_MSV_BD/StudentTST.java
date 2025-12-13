/**
 * Lớp StudentTST quản lý bảng điểm sinh viên sử dụng cấu trúc TST lồng nhau.
 * - Key cấp 1: Mã sinh viên (String)
 * - Value cấp 1: Bảng điểm (VietnameseTST<Double>)
 * - Key cấp 2: Tên môn học (String)
 * - Value cấp 2: Điểm số (Double)
 */
public class StudentTST extends VietnameseTST<VietnameseTST<Double>> {

    /**
     * Thêm điểm một môn học cho sinh viên.
     * Nếu sinh viên chưa tồn tại, tự động tạo mới bảng điểm cho sinh viên đó.
     * * @param studentId Mã sinh viên
     * @param subject Tên môn học
     * @param grade Điểm số
     */
    public void putStudent(String studentId, String subject, Double grade) {
        // 1. Tìm bảng điểm của sinh viên trong TST lớp ngoài
        VietnameseTST<Double> transcript = get(studentId);
        
        // 2. Nếu chưa có bảng điểm (SV mới), khởi tạo bảng điểm mới
        if (transcript == null) {
            transcript = new VietnameseTST<>();
            put(studentId, transcript);
        }
        
        // 3. Thêm điểm môn học vào TST lớp trong (Bảng điểm)
        transcript.put(subject, grade);
    }

    /**
     * Lấy điểm của một môn cụ thể của sinh viên.
     * * @param studentId Mã sinh viên
     * @param subject Tên môn học
     * @return Điểm số (hoặc null nếu không tìm thấy)
     */
    public Double getGrade(String studentId, String subject) {
        // Lấy bảng điểm
        VietnameseTST<Double> transcript = get(studentId);
        
        // Nếu SV không tồn tại hoặc chưa học môn này
        if (transcript == null) return null;
        
        return transcript.get(subject);
    }

    /**
     * Lấy danh sách tất cả các môn học mà sinh viên này có điểm.
     * * @param studentId Mã sinh viên
     * @return Danh sách tên các môn học
     */
    public Iterable<String> getSubjects(String studentId) {
        VietnameseTST<Double> transcript = get(studentId);
        if (transcript == null) return new Queue<String>(); // Trả về rỗng nếu SV không tồn tại
        return transcript.keys();
    }
}