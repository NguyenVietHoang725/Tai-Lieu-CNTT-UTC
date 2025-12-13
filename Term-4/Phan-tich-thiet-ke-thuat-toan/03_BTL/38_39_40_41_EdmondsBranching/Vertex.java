public interface Vertex extends Comparable<Vertex> {
    boolean contains(Vertex v); // dùng để kiểm tra đỉnh có thuộc supernode không
    String getName();           // trả về tên của đỉnh (dễ debug và truy vết)

    @Override
    default int compareTo(Vertex other) {
        // Mặc định so sánh theo tên
        return this.getName().compareTo(other.getName());
    }
}
