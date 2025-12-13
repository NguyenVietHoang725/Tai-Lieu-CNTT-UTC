import java.util.Stack;
import edu.princeton.cs.algs4.ST;

public class EdgeWeightedDirectedCycle_T {
    private final ST<Vertex, Boolean> marked;
    private final ST<Vertex, DirectedEdge_T> edgeTo;
    private final ST<Vertex, Boolean> onStack;
    private Stack<DirectedEdge_T> cycle;

    public EdgeWeightedDirectedCycle_T(EdgeWeightedDigraph_T G) {
        marked = new ST<>();
        edgeTo = new ST<>();
        onStack = new ST<>();
        cycle = null;

        for (Vertex v : G.getVertices()) {
            if (marked.get(v) == null && cycle == null) {
                dfs(G, v);
            }
        }
    }

    private void dfs(EdgeWeightedDigraph_T G, Vertex v) {
        marked.put(v, true);
        onStack.put(v, true);

        for (DirectedEdge_T e : G.adj(v)) {
            Vertex w = e.from(); // giả sử e.from() trả về đỉnh đầu cạnh

            if (cycle != null) return;

            if (marked.get(w) == null) {
                edgeTo.put(w, e);
                dfs(G, w);
            } else if (onStack.get(w) != null && onStack.get(w)) {
                cycle = new Stack<>();
                DirectedEdge_T f = e;
                while (f != null && !f.from().equals(w)) {
                    cycle.push(f);
                    f = edgeTo.get(f.from());
                }
                if (f != null) cycle.push(f);
                return;
            }
        }

        onStack.put(v, false);
    }

    public boolean hasCycle() {
        return cycle != null;
    }

    public Iterable<DirectedEdge_T> cycle() {
        return cycle;
    }
}
