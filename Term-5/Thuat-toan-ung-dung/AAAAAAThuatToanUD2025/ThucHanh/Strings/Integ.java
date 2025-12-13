 

public class Integ implements State{

    private final Automata automata;
    
    public Integ (Automata automata){
        this.automata = automata;
    }
     
    @Override
    public void digit() {
    }

    @Override
    public void letter() {
        System.out.println("automata is not supported to read a letter here");

        }
    @Override
    public void underscore() {
        System.out.println("automata is not supported to read _ here");
    }
    public void print(){
        System.out.println("It is an integer");
    }  
}
