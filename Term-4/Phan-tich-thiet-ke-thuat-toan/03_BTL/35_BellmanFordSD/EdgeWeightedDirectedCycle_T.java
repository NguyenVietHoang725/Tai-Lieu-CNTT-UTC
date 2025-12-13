public class EdgeWeightedDirectedCycle_T {
    private boolean[] marked;
    private DirectedEdge[] edgeTo;
    private boolean[] onStack;
    private Stack<DirectedEdge> cycle;

    public EdgeWeightedDirectedCycle_T(EdgeWeightedDigraph_T G) {
        marked = new boolean[G.V()];
        onStack = new boolean[G.V()];
        edgeTo = new DirectedEdge[G.V()];

        for (int v = 0; v < G.V(); v++) {
            if (!marked[v] && cycle == null) dfs(G, v);
        }
    }

    private void dfs(EdgeWeightedDigraph_T G, int v) {
        onStack[v] = true;
        marked[v] = true;
        for (DirectedEdge e : G.adj(v)) {
            int w = e.from(); // đảo hướng: đi từ w → v

            if (cycle != null) return;
            else if (!marked[w]) {
                edgeTo[w] = e;
                dfs(G, w);
            } else if (onStack[w]) {
                cycle = new Stack<>();
                DirectedEdge f = e;
                while (f.from() != w) {
                    cycle.push(f);
                    f = edgeTo[f.from()];
                }
                cycle.push(f);
                return;
            }
        }
        onStack[v] = false;
    }

    public boolean hasCycle() {
        return cycle != null;
    }

    public Iterable<DirectedEdge> cycle() {
        return cycle;
    }
}
