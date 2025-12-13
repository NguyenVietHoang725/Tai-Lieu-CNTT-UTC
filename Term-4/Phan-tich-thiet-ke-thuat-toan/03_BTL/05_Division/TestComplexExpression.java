public class TestComplexExpression {

    public static void main(String[] args) {
        // Ví dụ: (7 + 3) * 5 / 2 -> hậu tố là: 7 3 + 5 * 2 /
        String tokenString = "7 3 + 5 * 2 /";
        ComplexExpression suffixExp = new ComplexExpression(tokenString);
        int result = suffixExp.interpret();
        System.out.println("Ket qua tinh " +tokenString + " la: " + result);
        }
}