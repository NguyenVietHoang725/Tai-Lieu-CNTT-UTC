import java.util.Comparator;

public class Student implements Comparable<Student> {
    private final String fullName;   // họ tên sinh viên
    private final VDate birthDate;   // ngày sinh
    private final double gpa;        // điểm trung bình chung

    // Constructor chính
    public Student(String fullName, VDate birthDate, double gpa) {
        if (Double.isNaN(gpa) || Double.isInfinite(gpa))
            throw new IllegalArgumentException("GPA cannot be NaN or infinite");
        this.fullName = fullName;
        this.birthDate = birthDate;
        this.gpa = gpa;
    }

    // Constructor từ chuỗi: "Nguyen Van A 01/02/2000 3.45"
    public Student(String input) {
        String[] parts = input.trim().split("\\s+");
        if (parts.length < 4)
            throw new IllegalArgumentException("Invalid input");

        StringBuilder nameBuilder = new StringBuilder();
        for (int i = 0; i < parts.length - 2; i++) {
            nameBuilder.append(parts[i]).append(" ");
        }

        this.fullName = nameBuilder.toString().trim();
        this.birthDate = new VDate(parts[parts.length - 2]);
        this.gpa = Double.parseDouble(parts[parts.length - 1]);

        if (Double.isNaN(gpa) || Double.isInfinite(gpa))
            throw new IllegalArgumentException("GPA cannot be NaN or infinite");
    }

    public String fullName() {
        return fullName;
    }

    public VDate birthDate() {
        return birthDate;
    }

    public double gpa() {
        return gpa;
    }

    @Override
    public String toString() {
        return String.format("%-20s %10s %5.2f", fullName, birthDate, gpa);
    }

    @Override
    public int compareTo(Student that) {
        return Double.compare(this.gpa, that.gpa);
    }

    @Override
    public boolean equals(Object other) {
        if (this == other) return true;
        if (other == null || getClass() != other.getClass()) return false;
        Student that = (Student) other;
        return Double.compare(this.gpa, that.gpa) == 0 &&
               this.fullName.equals(that.fullName) &&
               this.birthDate.equals(that.birthDate);
    }

    @Override
    public int hashCode() {
        int hash = 1;
        hash = 31 * hash + fullName.hashCode();
        hash = 31 * hash + birthDate.hashCode();
        hash = 31 * hash + Double.hashCode(gpa);
        return hash;
    }

    // Comparator theo họ tên
    public static class NameOrder implements Comparator<Student> {
        public int compare(Student s1, Student s2) {
            return s1.fullName.compareTo(s2.fullName);
        }
    }

    // Comparator theo ngày sinh
    public static class BirthDateOrder implements Comparator<Student> {
        public int compare(Student s1, Student s2) {
            return s1.birthDate.compareTo(s2.birthDate);
        }
    }

    // Comparator theo GPA
    public static class GpaOrder implements Comparator<Student> {
        public int compare(Student s1, Student s2) {
            return Double.compare(s1.gpa, s2.gpa);
        }
    }

    // Unit test
    public static void main(String[] args) {
        Student[] students = new Student[3];
        students[0] = new Student("Nguyen Van A 01/02/2000 3.45");
        students[1] = new Student("Tran Thi B 12/11/1999 3.85");
        students[2] = new Student("Le Van C 20/05/2001 2.95");

        System.out.println("Unsorted:");
        for (Student s : students)
            System.out.println(s);
        System.out.println();

        System.out.println("Sort by name:");
        java.util.Arrays.sort(students, new Student.NameOrder());
        for (Student s : students)
            System.out.println(s);
        System.out.println();

        System.out.println("Sort by birth date:");
        java.util.Arrays.sort(students, new Student.BirthDateOrder());
        for (Student s : students)
            System.out.println(s);
        System.out.println();

        System.out.println("Sort by GPA:");
        java.util.Arrays.sort(students, new Student.GpaOrder());
        for (Student s : students)
            System.out.println(s);
    }
}
