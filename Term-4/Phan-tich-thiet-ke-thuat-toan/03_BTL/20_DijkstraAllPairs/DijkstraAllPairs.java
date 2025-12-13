import java.util.*;

public class DijkstraAllPairs {
    private DijkstraSP[] all;

    public DijkstraAllPairs(EdgeWeightedDigraph G) {
        all = new DijkstraSP[G.V()];
        for (int v = 0; v < G.V(); v++) {
            all[v] = new DijkstraSP(G, v);
        }
    }

    public Iterable<DirectedEdge> path(int s, int t) {
        return all[s].pathTo(t);
    }

    public double dist(int s, int t) {
        return all[s].distTo(t);
    }

    public boolean hasPath(int s, int t) {
        return all[s].hasPathTo(t);
    }

    public static void main(String[] args) {
        // Đọc đồ thị từ file
        In in = new In(args[0]);
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(in);

        DijkstraAllPairs allPairs = new DijkstraAllPairs(G);

        for (int s = 0; s < G.V(); s++) {
            for (int t = 0; t < G.V(); t++) {
                if (allPairs.hasPath(s, t)) {
                    StdOut.printf("%d to %d (%.2f): ", s, t, allPairs.dist(s, t));
                    for (DirectedEdge e : allPairs.path(s, t)) {
                        StdOut.print(e + "  ");
                    }
                    StdOut.println();
                } else {
                    StdOut.printf("%d to %d: no path\n", s, t);
                }
            }
        }
    }
}
