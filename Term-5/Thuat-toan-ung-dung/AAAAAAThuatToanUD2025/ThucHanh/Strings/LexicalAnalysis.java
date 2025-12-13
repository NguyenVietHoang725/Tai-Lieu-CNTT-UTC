import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
//import java.util.Scanner;
import java.io.*;

public class LexicalAnalysis {

/**
* @param args
*/
private String[] keywords = { "abstract", "boolean", "byte", "case",
"catch", "char", "class", "continue", "default", "do", "double",
"else", "extends", "final", "finally", "float", "for", "if", "main", "args",
"implements", "import", "instanceof", "int", "interface", "long",
"native", "new", "package", "private", "protected", "public",
"return", "short", "static", "super", "switch", "synchronized",
"this", "throw", "throws", "transient", "try", "void", "volatile",
"while", "false", "true", "null" };
HashMap<String, ArrayList<Integer>> keywordsTable;
HashMap<String, ArrayList<Integer>> otherWords = new HashMap<String, ArrayList<Integer>>();
ST<String, String> typesTable;
public LexicalAnalysis(String fileName)  {

int lineNumber = 0;

try {
System.setIn(new FileInputStream(new File(fileName)));
} catch (FileNotFoundException e) {
e.printStackTrace();
}

keywordsTable = new HashMap<String, ArrayList<Integer>>();
for(int i = 0; i < 49; i++){
keywordsTable.put(keywords[i], new ArrayList<Integer>());
}

typesTable = new ST<String, String>();
typesTable.put("(0|1)(0|1)*", "int");
typesTable.put("((s|k)|m)(((((e|y)|s)|t)|a)|x)*", "id");


while(!StdIn.isEmpty()){

lineNumber++;

String line = StdIn.readLine();

String[] lineparts = line.split("\\s+|\\.+|\\;+|\\(+|\\)+|\\\"+|\\:+|\\[+|\\]+|\\<+|\\>|\\,|\\+|\\++|\\=|\\{|\\}|\\!");
// \\s space, \\s+ nhieu khoang trang, \\. dau cham, \\.+ nhieu dau cham, \\; dau cham phay, \\;+ nhieu dau cham phay
for(String x: lineparts){

ArrayList<Integer> list = keywordsTable.get(x);
if(list == null){
    list = otherWords.get(x);
    if(list == null){
    ArrayList<Integer> temp = new ArrayList<Integer>();
    temp.add(lineNumber);
    otherWords.put(x,temp);
    }
    else{
    list.add(lineNumber); 
    }

}
else{
list.add(lineNumber); 
}
}
}
System.out.println("Keywords:");
printMap(keywordsTable);
System.out.println();
System.out.println("Other Words:");
printMap(otherWords);
printType(otherWords,typesTable);
}


public static void printMap(Map<String, ArrayList<Integer>> mp) {
Iterator<Map.Entry<String, ArrayList<Integer>>> it = mp.entrySet().iterator();
while (it.hasNext()) {
Map.Entry<String, ArrayList<Integer>> pairs = (Map.Entry<String, ArrayList<Integer>>)it.next();
System.out.print(pairs.getKey() + " = ");
printList(pairs.getValue());
System.out.println();
}
}
public static void printList(List x){

for(Object m : x){
System.out.print(m + ", ");
}

}

public static void printType(Map<String, ArrayList<Integer>> mp, ST<String,String> types) {
Iterator<Map.Entry<String, ArrayList<Integer>>> it = mp.entrySet().iterator();
System.out.println("Find a type for OtherWords");
while (it.hasNext()) {
Map.Entry<String, ArrayList<Integer>> pairs = (Map.Entry<String, ArrayList<Integer>>)it.next();
 String id = pairs.getKey();
 System.out.println(id);
  for(String reg: types.keys()){
            NFA nfa = new NFA(reg);
            if (nfa.recognizes(id)) StdOut.println(id + " is a type " + types.get(reg));
    }
 
}
}

public static void main(String[] args) throws IOException {
new LexicalAnalysis("FrequencyCounter.txt");
}

}

