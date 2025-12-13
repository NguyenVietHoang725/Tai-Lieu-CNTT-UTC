/******************************************************************************
 *  Compilation:  javac VDate.java
 *  Execution:    java VDate
 *  Dependencies: StdOut.java
 *
 *  An immutable data type for dates in the format DD/MM/YYYY.
 ******************************************************************************/

/**
 *  The {@code VDate} class is an immutable data type to encapsulate a
 *  date (day, month, and year) in the Vietnamese format DD/MM/YYYY.
 *  <p>
 *  For additional documentation,
 *  see <a href="https://algs4.cs.princeton.edu/12oop">Section 1.2</a> of
 *  <i>Algorithms, 4th Edition</i> by Robert Sedgewick and Kevin Wayne.
 *
 *  @author Robert Sedgewick
 *  @author Kevin Wayne
 *  @author Nguyen Viet Hoang IT3K64UTC 231230791
 */
public class VDate implements Comparable<VDate> {
    private static final int[] DAYS = { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    private final int day;     // day (between 1 and DAYS[month])
    private final int month;   // month (between 1 and 12)
    private final int year;    // year

    /**
     * Initializes a new date from the day, month, and year.
     * @param day the day (between 1 and 28–31, depending on the month)
     * @param month the month (between 1 and 12)
     * @param year the year
     * @throws IllegalArgumentException if this date is invalid
     */
    public VDate(int day, int month, int year) {
        if (!isValid(month, day, year)) throw new IllegalArgumentException("Invalid date");
        this.day   = day;
        this.month = month;
        this.year  = year;
    }

    /**
     * Initializes new date from a string in the format DD/MM/YYYY.
     * @param date the string representation of the date
     * @throws IllegalArgumentException if this date is invalid
     */
    public VDate(String date) {
        String[] fields = date.split("/");
        if (fields.length != 3) {
            throw new IllegalArgumentException("Invalid date format. Expected DD/MM/YYYY.");
        }
        day   = Integer.parseInt(fields[0]);
        month = Integer.parseInt(fields[1]);
        year  = Integer.parseInt(fields[2]);
        if (!isValid(month, day, year)) throw new IllegalArgumentException("Invalid date");
    }

    /**
     * Returns the day.
     * @return the day (1–31)
     */
    public int day() {
        return day;
    }

    /**
     * Returns the month.
     * @return the month (1–12)
     */
    public int month() {
        return month;
    }

    /**
     * Returns the year.
     * @return the year
     */
    public int year() {
        return year;
    }

    // Checks if the given date is valid.
    private static boolean isValid(int m, int d, int y) {
        if (m < 1 || m > 12) return false;
        if (d < 1 || d > DAYS[m]) return false;
        if (m == 2 && d == 29 && !isLeapYear(y)) return false;
        return true;
    }

    // Returns true if y is a leap year.
    private static boolean isLeapYear(int y) {
        if (y % 400 == 0) return true;
        if (y % 100 == 0) return false;
        return y % 4 == 0;
    }

    /**
     * Returns the next calendar date.
     * @return the date following this date
     */
    public VDate next() {
        if (isValid(month, day + 1, year))    return new VDate(day + 1, month, year);
        else if (isValid(month + 1, 1, year)) return new VDate(1, month + 1, year);
        else                                  return new VDate(1, 1, year + 1);
    }

    /**
     * Returns true if this date is after the given date.
     */
    public boolean isAfter(VDate that) {
        return compareTo(that) > 0;
    }

    /**
     * Returns true if this date is before the given date.
     */
    public boolean isBefore(VDate that) {
        return compareTo(that) < 0;
    }

    /**
     * Compares two dates chronologically.
     */
    @Override
    public int compareTo(VDate that) {
        if (this.year  < that.year)  return -1;
        if (this.year  > that.year)  return +1;
        if (this.month < that.month) return -1;
        if (this.month > that.month) return +1;
        if (this.day   < that.day)   return -1;
        if (this.day   > that.day)   return +1;
        return 0;
    }

    /**
     * Returns the date string in DD/MM/YYYY format.
     */
    @Override
    public String toString() {
        return String.format("%02d/%02d/%04d", day, month, year);
    }

    /**
     * Returns true if this date equals the other object.
     */
    @Override
    public boolean equals(Object other) {
        if (other == this) return true;
        if (other == null) return false;
        if (other.getClass() != this.getClass()) return false;
        VDate that = (VDate) other;
        return (this.day == that.day) && (this.month == that.month) && (this.year == that.year);
    }

    /**
     * Returns a hash code for this date.
     */
    @Override
    public int hashCode() {
        int hash = 17;
        hash = 31 * hash + day;
        hash = 31 * hash + month;
        hash = 31 * hash + year;
        return hash;
    }

    /**
     * Unit tests the {@code VDate} data type.
     */
    public static void main(String[] args) {
        VDate today = new VDate(25, 2, 2004);
        StdOut.println("Start date:");
        StdOut.println(today);

        StdOut.println("\nNext 10 days:");
        for (int i = 0; i < 10; i++) {
            today = today.next();
            StdOut.println(today);
        }

        StdOut.println("\nCheck comparisons:");
        StdOut.println("today.isAfter(today.next()) = " + today.isAfter(today.next()));   // false
        StdOut.println("today.isAfter(today) = " + today.isAfter(today));                 // false
        StdOut.println("today.next().isAfter(today) = " + today.next().isAfter(today));   // true

        VDate birthday = new VDate(16, 10, 1971);
        StdOut.println("\nBirthday date:");
        StdOut.println(birthday);

        StdOut.println("\nNext 10 days after birthday:");
        for (int i = 0; i < 10; i++) {
            birthday = birthday.next();
            StdOut.println(birthday);
        }

        VDate test = new VDate("04/08/1955");
        StdOut.println("\nTest date from string:");
        StdOut.println(test);
    }
}
