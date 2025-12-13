

public class Automata implements State{
    
    private State start;
    private State integ;
    private State identity;
    private State underscore;
    
    private State state;
    
    public Automata(){
           this.start = new Start(this);
           this.integ = new Integ(this);
           this.identity = new Identity(this);
           this.underscore = new Underscore(this);
           this.state = start;
    }
     
    @Override
    
    public void digit() {
           state.digit();
    }

    @Override
    public void letter() {
           state.letter(); 
    }

    @Override
    public void underscore() {
           state.underscore();
        
    }
    
    public void setState (State state){
           this.state = state;   
    }    
    public void setStart (State start){
           this.start = start;   
    } 
    public void setInteg (State integ){
           this.integ = integ;   
    } 
    public void setIdentity (State identity){
           this.identity = identity;   
    } 
    public void setUnderscore (State underscore){
           this.underscore = underscore;   
    }
    
    public State getState() {
		return state; 
    }
    public State getStart() {
		return start; 
    }
    public State getInteg() {
		return integ; 
    }
    public State getIdentity() {
		return identity; 
    }
    public State getUnderscore() {
		return underscore; 
    }
    public void print(){
        state.print();
    } 
    public boolean recognizes(String txt) {
        this.state = start;
        for (int i = 0; i < txt.length(); i++) {
            if(Character.isDigit(txt.charAt(i))) digit();
            else if (Character.isLetter(txt.charAt(i))) letter();
            else underscore();
        }
        if ((state == integ) || (state == identity)) return true;
        else return false;
    }    

}
