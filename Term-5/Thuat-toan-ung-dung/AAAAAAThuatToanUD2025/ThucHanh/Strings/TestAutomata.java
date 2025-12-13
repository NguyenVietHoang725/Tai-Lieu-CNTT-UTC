 

public class TestAutomata {

	public static void main(String[] args) {
                String txt = args[0];
                Automata automata = new Automata();
                StdOut.println(automata.recognizes(txt));
                automata.print();
	}

}
