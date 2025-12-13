public class BellmanFordAllPair {
    private final int V;
    private double[][] distToAll;           // distToAll[s][v] = shortest distance from s to v
    private DirectedEdge[][] edgeToAll;     // edgeToAll[s][v] = last edge on shortest path s->v
    private boolean hasNegativeCycle;       // nếu có chu trình âm bất kỳ

    /**
     * Chạy Bellman-Ford với mỗi đỉnh làm nguồn để tính khoảng cách tất cả cặp đỉnh.
     * @param G đồ thị trọng số có hướng
     */
    public BellmanFordAllPair(EdgeWeightedDigraph G) {
        this.V = G.V();
        distToAll = new double[V][V];
        edgeToAll = new DirectedEdge[V][V];
        hasNegativeCycle = false;

        // Khởi tạo khoảng cách ban đầu cho tất cả các đỉnh nguồn
        for (int s = 0; s < V; s++) {
            for (int v = 0; v < V; v++) {
                distToAll[s][v] = Double.POSITIVE_INFINITY;
                edgeToAll[s][v] = null;
            }
            distToAll[s][s] = 0.0;

            // Chạy Bellman-Ford cho nguồn s
            if (!bellmanFordFromSource(G, s)) {
                hasNegativeCycle = true;
                break;
            }
        }
    }

    // Chạy Bellman-Ford từ đỉnh nguồn s, trả về false nếu phát hiện chu trình âm
    private boolean bellmanFordFromSource(EdgeWeightedDigraph G, int s) {
        boolean[] onQueue = new boolean[V];
        Queue<Integer> queue = new Queue<>();
        int[] cost = new int[V]; // số lần relax cho từng đỉnh

        queue.enqueue(s);
        onQueue[s] = true;

        while (!queue.isEmpty()) {
            int v = queue.dequeue();
            onQueue[v] = false;

            for (DirectedEdge e : G.adj(v)) {
                int w = e.to();
                if (distToAll[s][w] > distToAll[s][v] + e.weight()) {
                    distToAll[s][w] = distToAll[s][v] + e.weight();
                    edgeToAll[s][w] = e;
                    if (!onQueue[w]) {
                        queue.enqueue(w);
                        onQueue[w] = true;
                    }
                }
                cost[v]++;
                if (cost[v] >= V) {
                    // Có chu trình âm
                    return false;
                }
            }
        }
        return true;
    }

    /** Kiểm tra có chu trình âm trong đồ thị hay không */
    public boolean hasNegativeCycle() {
        return hasNegativeCycle;
    }

    /** Trả về khoảng cách từ s đến v, hoặc +∞ nếu không có đường đi */
    public double dist(int s, int v) {
        validateVertex(s);
        validateVertex(v);
        if (hasNegativeCycle)
            throw new UnsupportedOperationException("Negative cost cycle exists");
        return distToAll[s][v];
    }

    /** Kiểm tra có đường đi từ s đến v hay không */
    public boolean hasPath(int s, int v) {
        validateVertex(s);
        validateVertex(v);
        return distToAll[s][v] < Double.POSITIVE_INFINITY;
    }

    /** Trả về đường đi ngắn nhất từ s đến v dưới dạng Iterable<DirectedEdge> */
    public Iterable<DirectedEdge> path(int s, int v) {
        validateVertex(s);
        validateVertex(v);
        if (hasNegativeCycle)
            throw new UnsupportedOperationException("Negative cost cycle exists");
        if (!hasPath(s, v)) return null;
        Stack<DirectedEdge> path = new Stack<>();
        for (DirectedEdge e = edgeToAll[s][v]; e != null; e = edgeToAll[s][e.from()]) {
            path.push(e);
        }
        return path;
    }

    private void validateVertex(int v) {
        if (v < 0 || v >= V)
            throw new IllegalArgumentException("vertex " + v + " is not between 0 and " + (V-1));
    }

    /** Hàm main demo sử dụng BellmanFordAllPair */
    public static void main(String[] args) {
        if (args.length != 1) {
            System.err.println("Usage: java BellmanFordAllPair filename.txt");
            return;
        }
        In in = new In(args[0]);
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(in);

        BellmanFordAllPair allPairSP = new BellmanFordAllPair(G);

        if (allPairSP.hasNegativeCycle()) {
            System.out.println("Negative cycle detected!");
            return;
        }

        for (int s = 0; s < G.V(); s++) {
            for (int v = 0; v < G.V(); v++) {
                if (allPairSP.hasPath(s, v)) {
                    System.out.printf("%d to %d (%.2f): ", s, v, allPairSP.dist(s, v));
                    for (DirectedEdge e : allPairSP.path(s, v)) {
                        System.out.print(e + "  ");
                    }
                    System.out.println();
                } else {
                    System.out.printf("%d to %d: no path\n", s, v);
                }
            }
        }
    }
}
