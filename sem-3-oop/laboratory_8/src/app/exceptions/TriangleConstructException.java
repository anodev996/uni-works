package app.exceptions;

public class TriangleConstructException extends Exception {

    public TriangleConstructException() {
        super();
    }
    
    public TriangleConstructException(String message) {
        super(message);
    }
}