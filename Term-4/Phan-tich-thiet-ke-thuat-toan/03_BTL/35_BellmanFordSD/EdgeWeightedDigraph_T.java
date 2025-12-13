public class EdgeWeightedDigraph_T {
    private static final String NEWLINE = System.getProperty("line.separator");

    private final int V;                        // number of vertices
    private int E;                              // number of edges
    private Bag<DirectedEdge>[] inEdges;       // inEdges[v] = list of edges going into v
    private int[] outdegree;                   // outdegree[v] = number of edges going out from v

    @SuppressWarnings("unchecked")
    public EdgeWeightedDigraph_T(int V) {
        if (V < 0) throw new IllegalArgumentException("Number of vertices must be nonnegative");
        this.V = V;
        this.E = 0;
        this.outdegree = new int[V];
        inEdges = (Bag<DirectedEdge>[]) new Bag[V];
        for (int v = 0; v < V; v++) {
            inEdges[v] = new Bag<>();
        }
    }

    public EdgeWeightedDigraph_T(EdgeWeightedDigraph G) {
        this(G.V());
        for (DirectedEdge e : G.edges()) {
            addEdge(e);  // tự động xác định to từ e
        }
    }

    public int V() {
        return V;
    }

    public int E() {
        return E;
    }

    public Iterable<DirectedEdge> inEdges(int v) {
        validateVertex(v);
        return inEdges[v];
    }

    public void addEdge(DirectedEdge e) {
        int to = e.to();
        addEdge(to, e);
    }

    public void addEdge(int to, DirectedEdge e) {
        int from = e.from();
        validateVertex(to);
        validateVertex(from);
        inEdges[to].add(e);       // thêm cạnh vào danh sách các cạnh đi vào đỉnh 'to'
        outdegree[from]++;
        E++;
    }

    public Iterable<DirectedEdge> adj(int v) {
        return inEdges(v);        // alias cho dễ quen với các thuật toán
    }

    public int outdegree(int v) {
        validateVertex(v);
        return outdegree[v];
    }

    public Iterable<DirectedEdge> edges() {
        Bag<DirectedEdge> list = new Bag<>();
        for (int v = 0; v < V; v++) {
            for (DirectedEdge e : inEdges[v]) {
                list.add(e);
            }
        }
        return list;
    }

    private void validateVertex(int v) {
        if (v < 0 || v >= V)
            throw new IllegalArgumentException("vertex " + v + " is not between 0 and " + (V - 1));
    }

    public String toString() {
        StringBuilder s = new StringBuilder();
        s.append(V + " vertices, " + E + " edges " + NEWLINE);
        for (int v = 0; v < V; v++) {
            s.append(String.format("%d: ", v));
            for (DirectedEdge e : inEdges[v]) {
                s.append(e + "  ");
            }
            s.append(NEWLINE);
        }
        return s.toString();
    }
}
