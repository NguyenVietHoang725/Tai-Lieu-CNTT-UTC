
class Parser { 
    private String[] tokens; 
    private String postfix; 
    private String lookahead; 
    private Automata automata; 
    private int current = 0;
    public Parser(String infix) { 
    tokens = infix.split("\\s+");      
    postfix = "";
    automata = new Automata();
    lookahead = tokens[current];
    }
    void expr() { 
            term(); 
            while(true) { 
                if(lookahead.compareTo("+") == 0) { 
                match("+"); 
                term(); 
                postfix += " ";
                postfix += "+";
                }    
                else if(lookahead.compareTo("-") == 0) { 
                match("-"); 
                term(); 
                postfix += " ";
                postfix += "-";
                } 
                else return; 
            } 
        
    }
    void term() { 
        if(automata.recognizes(lookahead)) { 
            postfix += " ";
            postfix += lookahead;
            match(lookahead); 
        } 
        else throw new Error("syntax error"); 
    }

    void match(String t) { 
        if( lookahead.compareTo(t) == 0 ) {
            if(current < tokens.length-1) 
                lookahead = tokens[++current];
        } 
        else throw new Error("syntax error"); 
    }
    public String getPostfix(){
    return postfix; 
    }    
}


