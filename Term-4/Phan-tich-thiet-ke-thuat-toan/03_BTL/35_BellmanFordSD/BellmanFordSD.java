public class BellmanFordSD {
    private double[] distTo;               // distTo[v] = khoảng cách ngắn nhất từ v đến t
    private DirectedEdge[] edgeTo;         // edgeTo[v] = cạnh cuối cùng trên đường đi ngắn nhất từ v đến t
    private boolean[] onQueue;             // onQueue[v] = đỉnh v có đang trong hàng đợi không
    private Queue<Integer> queue;          // hàng đợi các đỉnh cần relax
    private int cost;                      // số lần gọi relax
    private Iterable<DirectedEdge> cycle;  // chu trình âm nếu tồn tại

    /**
     * Tính đường đi ngắn nhất từ mọi đỉnh đến đỉnh đích t.
     * @param G đồ thị có hướng và trọng số (với danh sách cạnh đi vào đỉnh)
     * @param t đỉnh đích
     */
    public BellmanFordSD(EdgeWeightedDigraph_T G, int t) {
        int V = G.V();
        distTo = new double[V];
        edgeTo = new DirectedEdge[V];
        onQueue = new boolean[V];

        for (int v = 0; v < V; v++)
            distTo[v] = Double.POSITIVE_INFINITY;
        distTo[t] = 0.0;

        queue = new Queue<>();
        queue.enqueue(t);
        onQueue[t] = true;

        while (!queue.isEmpty() && !hasNegativeCycle()) {
            int v = queue.dequeue();
            onQueue[v] = false;
            relax(G, v);
        }
    }

    private void relax(EdgeWeightedDigraph_T G, int v) {
        for (DirectedEdge e : G.inEdges(v)) {
            int u = e.from();
            if (distTo[u] > distTo[v] + e.weight()) {
                distTo[u] = distTo[v] + e.weight();
                edgeTo[u] = e;
                if (!onQueue[u]) {
                    queue.enqueue(u);
                    onQueue[u] = true;
                }
            }

            if (++cost % G.V() == 0) {
                findNegativeCycle(G.V());
                if (hasNegativeCycle()) return;
            }
        }
    }

    private void findNegativeCycle(int V) {
        EdgeWeightedDigraph_T spt = new EdgeWeightedDigraph_T(V);
        for (int v = 0; v < V; v++)
            if (edgeTo[v] != null)
                spt.addEdge(edgeTo[v]);

        EdgeWeightedDirectedCycle_T finder = new EdgeWeightedDirectedCycle_T(spt);
        cycle = finder.cycle();
    }

    public boolean hasNegativeCycle() {
        return cycle != null;
    }

    public Iterable<DirectedEdge> negativeCycle() {
        return cycle;
    }

    public double distTo(int v) {
        validateVertex(v);
        if (hasNegativeCycle())
            throw new UnsupportedOperationException("Đồ thị chứa chu trình âm");
        return distTo[v];
    }

    public boolean hasPathTo(int v) {
        validateVertex(v);
        return distTo[v] < Double.POSITIVE_INFINITY;
    }

    public Iterable<DirectedEdge> pathTo(int v) {
        validateVertex(v);
        if (hasNegativeCycle()) throw new UnsupportedOperationException("Chu trình âm tồn tại");
        if (!hasPathTo(v)) return null;

        Stack<DirectedEdge> path = new Stack<>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.to()]) {
            path.push(e);
        }
        return path;
    }

    private void validateVertex(int v) {
        int V = distTo.length;
        if (v < 0 || v >= V)
            throw new IllegalArgumentException("Đỉnh không hợp lệ: " + v);
    }
    
    public static void main(String[] args) {
        In in = new In(args[0]);              // Đọc file đầu vào
        int t = Integer.parseInt(args[1]);    // Đỉnh đích
    
        // Tạo đồ thị ban đầu và đảo cạnh để lấy đồ thị in-edges
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(in);
        EdgeWeightedDigraph_T G_T = new EdgeWeightedDigraph_T(G);
    
        // Tạo đối tượng BellmanFordSD để tìm đường đi ngắn nhất đến đỉnh t
        BellmanFordSD sd = new BellmanFordSD(G_T, t);
    
        // Nếu có chu trình âm thì in ra
        if (sd.hasNegativeCycle()) {
            StdOut.println("Negative cycle detected:");
            for (DirectedEdge e : sd.negativeCycle()) {
                StdOut.println(e);
            }
        } else {
            // In đường đi ngắn nhất từ từng đỉnh v đến t
            for (int v = 0; v < G.V(); v++) {
                if (sd.hasPathTo(v)) {
                    StdOut.printf("%d to %d (%5.2f)  ", v, t, sd.distTo(v));
                    for (DirectedEdge e : sd.pathTo(v)) {
                        StdOut.print(e + "   ");
                    }
                    StdOut.println();
                } else {
                    StdOut.printf("%d to %d           no path\n", v, t);
                }
            }
        }
    }

}
