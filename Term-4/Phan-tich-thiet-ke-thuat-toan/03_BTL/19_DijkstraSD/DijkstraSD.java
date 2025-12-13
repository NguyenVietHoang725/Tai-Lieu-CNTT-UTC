import java.util.PriorityQueue;
import java.util.Stack;
import java.util.Comparator;

public class DijkstraSD {
    private double[] distTo;
    private DirectedEdge[] edgeTo;
    private PriorityQueue<Integer> pq;

    public DijkstraSD(EdgeWeightedDigraph G, int t) {
        EdgeWeightedDigraph reversed = G.reverse(); // đảo đồ thị
        int V = G.V();

        distTo = new double[V];
        edgeTo = new DirectedEdge[V];
        for (int v = 0; v < V; v++) {
            distTo[v] = Double.POSITIVE_INFINITY;
        }
        distTo[t] = 0.0;

        pq = new PriorityQueue<>(Comparator.comparingDouble(v -> distTo[v]));
        pq.add(t);

        while (!pq.isEmpty()) {
            int v = pq.poll();
            for (DirectedEdge e : reversed.adj(v)) {
                relax(e);
            }
        }
    }

    private void relax(DirectedEdge e) {
        int v = e.from(), w = e.to();
        if (distTo[w] > distTo[v] + e.weight()) {
            distTo[w] = distTo[v] + e.weight();
            edgeTo[w] = e;
            pq.remove(w); // xóa nếu đã có
            pq.add(w);    // thêm lại với trọng số mới
        }
    }

    public double distTo(int v) {
        return distTo[v];
    }

    public boolean hasPathTo(int v) {
        return distTo[v] < Double.POSITIVE_INFINITY;
    }

    public Iterable<DirectedEdge> pathTo(int v) {
        if (!hasPathTo(v)) return null;
        Stack<DirectedEdge> path = new Stack<>();
        for (DirectedEdge e = edgeTo[v]; e != null; e = edgeTo[e.from()]) {
            path.push(new DirectedEdge(e.to(), e.from(), e.weight())); // đảo lại để khớp đồ thị gốc
        }
        return path;
    }
    
    public static void main(String[] args) {
        In in = new In(args[0]);
        EdgeWeightedDigraph G = new EdgeWeightedDigraph(in);
        int destination = Integer.parseInt(args[1]);

        // compute shortest paths
        DijkstraSD sd = new DijkstraSD(G, destination);

        // print shortest path
        for (int v = 0; v < G.V(); v++) {
            if (sd.hasPathTo(v)) {
                System.out.printf("Path from %d to %d (distance: %.2f): ", v, destination, sd.distTo(v));
                for (DirectedEdge e : sd.pathTo(v)) {
                    System.out.print(e + " ");
                }
                System.out.println();
            } else {
                System.out.println("No path from " + v + " to " + destination);
            }
        }
    }
}
