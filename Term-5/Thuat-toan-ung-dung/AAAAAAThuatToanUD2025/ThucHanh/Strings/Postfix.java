import java.io.*; 
public class Postfix { 
public static void main(String[] args) throws IOException{ 
String expression = "71  +  33 - 50  + 221 - 47";
Parser parse = new Parser(expression); 
parse.expr(); 
System.out.println("Bieu thuc hau to: " + parse.getPostfix()); 
} 
}