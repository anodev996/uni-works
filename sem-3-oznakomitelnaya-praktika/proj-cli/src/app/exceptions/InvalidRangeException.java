package app.exceptions;

public class InvalidRangeException extends Exception{
    public InvalidRangeException() {
        super();
    }
    public InvalidRangeException(String message) {
        super(message);
    }
}
