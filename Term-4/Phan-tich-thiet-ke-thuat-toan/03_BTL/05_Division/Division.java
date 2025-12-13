public class Division implements Expression {

    private final Expression leftExpression;
    private final Expression rightExpression;

    public Division(Expression leftExpression, Expression rightExpression) {
        this.leftExpression = leftExpression;
        this.rightExpression = rightExpression;
    }

    @Override
    public int interpret() {
        int divisor = rightExpression.interpret();
        if (divisor == 0) {
            throw new ArithmeticException("Division by zero is not allowed.");
        }
        return leftExpression.interpret() / divisor;
    }

}