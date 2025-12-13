import java.util.ArrayList;
import java.util.List;

public class EdgeWeightedDigraph_T {
    private final ST<Vertex, Boolean> vertexMap;
    private final ST<Vertex, Bag<DirectedEdge_T>> inEdges;
    private final ST<Vertex, Integer> outdegree;
    private int E;

    public EdgeWeightedDigraph_T() {
        this.vertexMap = new ST<>();
        this.inEdges = new ST<>();
        this.outdegree = new ST<>();
        this.E = 0;
    }

    public void addVertex(Vertex v) {
        if (!vertexMap.contains(v)) {
            vertexMap.put(v, true);
            inEdges.put(v, new Bag<>());
            outdegree.put(v, 0);
        }
    }

    public void addEdge(DirectedEdge_T e) {
        Vertex from = e.from();
        Vertex to = e.to();
        addVertex(from);
        addVertex(to);
        inEdges.get(to).add(e);
        outdegree.put(from, outdegree.get(from) + 1);
        E++;
    }

    public Iterable<DirectedEdge_T> inEdges(Vertex v) {
        if (inEdges.contains(v)) return inEdges.get(v);
        return new Bag<>();
    }

    // Trả về các cạnh đi vào đỉnh v (inEdges)
    public Iterable<DirectedEdge_T> adj(Vertex v) {
        return inEdges(v);
    }

    // Số cạnh đi ra khỏi đỉnh v
    public int outdegree(Vertex v) {
        if (outdegree.contains(v)) return outdegree.get(v);
        return 0;
    }

    // Tất cả các cạnh trong đồ thị
    public Iterable<DirectedEdge_T> edges() {
        List<DirectedEdge_T> list = new ArrayList<>();
        for (Vertex v : inEdges.keys()) {
            for (DirectedEdge_T e : inEdges.get(v)) {
                list.add(e);
            }
        }
        return list;
    }

    // Số đỉnh
    public int V() {
        return vertexMap.size();
    }

    // Số cạnh
    public int E() {
        return E;
    }
    
    public int vertexCount() {
        int count = 0;
        for (Vertex v : getVertices()) {
            count++;
        }
        return count;
    }

    // Tất cả đỉnh
    public Iterable<Vertex> getVertices() {
        return vertexMap.keys();
    }

    @Override
    public String toString() {
        StringBuilder sb = new StringBuilder();
        sb.append(V()).append(" vertices, ").append(E()).append(" edges\n");
        for (Vertex v : getVertices()) {
            sb.append(v).append(": ");
            for (DirectedEdge_T e : inEdges(v)) {
                sb.append(e).append("  ");
            }
            sb.append("\n");
        }
        return sb.toString();
    }
}
