public class DirectedEdge_T {
    private final Vertex from;
    private final Vertex to;
    private final double weight;
    private final DirectedEdge_T originalEdge; // lưu cạnh gốc nếu là cạnh trong đồ thị co cụm

    public DirectedEdge_T(Vertex from, Vertex to, double weight) {
        this(from, to, weight, null);
    }

    public DirectedEdge_T(Vertex from, Vertex to, double weight, DirectedEdge_T originalEdge) {
        if (from == null || to == null) throw new IllegalArgumentException("Vertex cannot be null");
        if (Double.isNaN(weight)) throw new IllegalArgumentException("Weight is NaN");
        this.from = from;
        this.to = to;
        this.weight = weight;
        this.originalEdge = originalEdge;
    }

    public Vertex from() {
        return from;
    }

    public Vertex to() {
        return to;
    }

    public double weight() {
        return weight;
    }

    public DirectedEdge_T originalEdge() {
        return originalEdge;
    }

    @Override
    public String toString() {
        return from.getName() + "->" + to.getName() + " " + String.format("%5.2f", weight);
    }
}
