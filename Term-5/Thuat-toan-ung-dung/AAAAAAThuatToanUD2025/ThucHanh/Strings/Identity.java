 

public class Identity implements State{

    private final Automata automata;
    
    public Identity(Automata automata){
        this.automata = automata;
    }
     
    @Override
    public void letter() {

    }

    @Override
    public void digit() {
        //System.out.println("It is an identity");
    }

    @Override
    public void underscore() {
        automata.setState(automata.getUnderscore());
        //System.out.println("Go to an Underscore");
    }
    public void print(){
        System.out.println("It is an identity");
    } 
}
