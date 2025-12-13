import java.util.HashSet;
import java.util.Set;

public class SuperNode implements Vertex {
    private final Set<Vertex> members = new HashSet<>();
    private final String name;

    public SuperNode(String name) {
        this.name = name;
    }

    public void add(Vertex v) {
        members.add(v);
    }

    public Set<Vertex> getMembers() {
        return members;
    }

    @Override
    public boolean contains(Vertex v) {
        for (Vertex member : members) {
            if (member.contains(v)) return true;
        }
        return false;
    }

    @Override
    public String getName() {
        return name;
    }

    @Override
    public String toString() {
        return "SuperNode(" + name + ")";
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (!(obj instanceof SuperNode)) return false;
        SuperNode other = (SuperNode) obj;
        return name.equals(other.name);
    }

    @Override
    public int hashCode() {
        return name.hashCode();
    }
}
