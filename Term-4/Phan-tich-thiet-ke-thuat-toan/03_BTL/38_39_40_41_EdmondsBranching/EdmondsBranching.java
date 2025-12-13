import java.util.*;

public class EdmondsBranching {
    private final ST<Vertex, DirectedEdge_T> edgeTo = new ST<>();
    private final Set<Vertex> visited = new HashSet<>();
    private final Vertex root;

    public EdmondsBranching(EdgeWeightedDigraph_T G, Vertex root) {
        this.root = root;
        computeBranching(G);
    }

    private void computeBranching(EdgeWeightedDigraph_T G) {
        // Bước 1: chọn cạnh vào nhẹ nhất cho mỗi đỉnh (trừ gốc)
        for (Vertex v : G.getVertices()) {
            if (v.equals(root)) continue;

            DirectedEdge_T minEdge = null;
            for (DirectedEdge_T e : G.inEdges(v)) {
                if (minEdge == null || e.weight() < minEdge.weight()) {
                    minEdge = e;
                }
            }
            if (minEdge != null) {
                edgeTo.put(v, minEdge);
            }
        }

        // Bước 2: phát hiện chu trình
        EdgeWeightedDigraph_T branchingGraph = new EdgeWeightedDigraph_T();
        for (DirectedEdge_T e : edgeTo.values()) {
            branchingGraph.addEdge(e);
        }

        EdgeWeightedDirectedCycle_T finder = new EdgeWeightedDirectedCycle_T(branchingGraph);
        if (finder.hasCycle()) {
            System.out.println("Chu trình phát hiện được trong đồ thị nhánh:");
            for (DirectedEdge_T e : finder.cycle()) {
                System.out.println(e);
            }
            throw new IllegalArgumentException("Chu trình tồn tại - không thể tạo nhánh bao trùm hợp lệ");
        }

        // Bước 3: Kiểm tra tính liên thông có định hướng từ root
        dfs(root);
        if (visited.size() < G.vertexCount()) {
            System.out.println("Không thể tạo nhánh bao trùm - đồ thị không liên thông từ gốc");
            throw new IllegalArgumentException("Không thể tạo nhánh bao trùm - đồ thị không liên thông từ gốc");
        }
    }

    private void dfs(Vertex v) {
        visited.add(v);
        for (Vertex w : edgeTo.keys()) {
            DirectedEdge_T e = edgeTo.get(w);
            if (e.from().equals(v) && !visited.contains(w)) {
                dfs(w);
            }
        }
    }

    public Iterable<DirectedEdge_T> edges() {
        return edgeTo.values();
    }

    public double weight() {
        double total = 0.0;
        for (DirectedEdge_T e : edgeTo.values()) {
            total += e.weight();
        }
        return total;
    }

    // ------------------ MAIN TEST SAMPLE -------------------
    public static void main(String[] args) {
        EdgeWeightedDigraph_T G = new EdgeWeightedDigraph_T();
    
        // Tạo các đỉnh
        Vertex A = new Node("A");
        Vertex B = new Node("B");
        Vertex C = new Node("C");
        Vertex D = new Node("D");
        Vertex E = new Node("E");
        Vertex F = new Node("F");
        Vertex I = new Node("I");
        Vertex H = new Node("H");
    
        // Thêm cạnh (from -> to, weight) - tất cả trọng số dương
        G.addEdge(new DirectedEdge_T(A, B, 4.0));
        G.addEdge(new DirectedEdge_T(A, C, 2.0));
        G.addEdge(new DirectedEdge_T(B, C, 3.0));
        G.addEdge(new DirectedEdge_T(B, D, 5.0));
        G.addEdge(new DirectedEdge_T(B, E, 6.0));
        G.addEdge(new DirectedEdge_T(C, D, 1.0));
        G.addEdge(new DirectedEdge_T(C, F, 4.0));
        G.addEdge(new DirectedEdge_T(D, E, 2.0));
        G.addEdge(new DirectedEdge_T(D, F, 3.0));
        G.addEdge(new DirectedEdge_T(E, F, 1.0));
        G.addEdge(new DirectedEdge_T(E, I, 5.0));
        G.addEdge(new DirectedEdge_T(F, I, 2.0));
        G.addEdge(new DirectedEdge_T(I, H, 3.0));
        G.addEdge(new DirectedEdge_T(H, B, 4.0)); 
        G.addEdge(new DirectedEdge_T(F, A, 6.0)); 
        G.addEdge(new DirectedEdge_T(I, D, 2.0)); 
    
        // In đồ thị
        System.out.println("Đồ thị:");
        System.out.println(G);
    
        // Tìm nhánh bao trùm nhỏ nhất từ đỉnh A
        try {
            EdmondsBranching branching = new EdmondsBranching(G, A);
            System.out.println("\nNhánh bao trùm nhỏ nhất (Min-Branching) từ đỉnh A:");
            for (DirectedEdge_T e : branching.edges()) {
                System.out.println(e);
            }
            System.out.printf("Tổng trọng số: %.2f\n", branching.weight());
        } catch (IllegalArgumentException e) {
            System.err.println("Lỗi: " + e.getMessage());
        }
    }
}
