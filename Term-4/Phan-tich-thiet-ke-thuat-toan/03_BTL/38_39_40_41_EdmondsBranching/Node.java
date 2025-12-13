import java.util.Objects;

public class Node implements Vertex {
    private final String name;

    public Node(String name) {
        this.name = name;
    }

    @Override
    public boolean contains(Vertex v) {
        return this.equals(v);
    }

    @Override
    public String getName() {
        return name;
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (!(obj instanceof Node)) return false;
        Node other = (Node) obj;
        return name.equals(other.name);
    }

    @Override
    public int hashCode() {
        return Objects.hash(name);
    }

    @Override
    public String toString() {
        return name;
    }
}
