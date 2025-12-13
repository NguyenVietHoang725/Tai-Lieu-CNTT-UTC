 

public class Start implements State{

    private final Automata automata;
    
    public Start(Automata automata){
        this.automata = automata;
    }
     
    @Override
    public void digit() {
        automata.setState(automata.getInteg());
        //System.out.println("It is a integer");
    }

    @Override
    public void letter() {
        automata.setState(automata.getIdentity());
        //System.out.println("It is an identity");
    }

    @Override
    public void underscore() {
        System.out.println("automata is not supported to read _ here");
        
    }
    public void print(){
        System.out.println("It is a start");
    }        

}
