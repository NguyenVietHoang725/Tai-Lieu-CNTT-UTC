public class Student implements Comparable<Student> {
    private String id;   
    private String name; 

    public Student(String id, String name) {
        this.id = id;
        this.name = name;
    }

    public String getId() { return id; }
    public String getName() { return name; }

    @Override
    public int compareTo(Student other) {
        return this.id.compareTo(other.id);
    }

    @Override
    public String toString() {
        return id + " - " + name;
    }
}