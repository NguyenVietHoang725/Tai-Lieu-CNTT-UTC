public class Lecture implements Comparable<Lecture> {
    int start, finish;

    public Lecture(int start, int finish) {
        this.start = start;
        this.finish = finish;
    }

    @Override
    public int compareTo(Lecture other) {
        return Integer.compare(this.start, other.start);
    }

    @Override
    public String toString() {
        return "Lecture(" + start + ", " + finish + ")";
    }
}
