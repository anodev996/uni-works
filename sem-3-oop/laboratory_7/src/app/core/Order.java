package app.core;

public class Order {
    private String id;
    private String name;
    private String courier;
    private String dateTime;
    private int type;

    public Order(String id, String name, String courier, String dateTime, int type) {
        this.id = id;
        this.name = name;
        this.courier = courier;
        this.dateTime = dateTime;
        this.type = type;
    }

    public String getId() {return id;}
    public String getName() {return name;}
    public String getCourier() {return courier;}
    public String getDateTime() {return dateTime;}
    public int getType() {return type;}

    public void setId(String id) {this.id = id;}
    public void setName(String name) {this.name = name;}
    public void setCourier(String courier) {this.courier = courier;}
    public void setDateTime(String dateTime) {this.dateTime = dateTime;}
    public void setType(int type) {this.type = type;}
    
    @Override
    public String toString() {
        return String.format("Order ID: %s\nProduct name: %s\nCourier ID: %s\nDate and time: %s\nOrder type: %d\n",
                             getId(), getName(), getCourier(), getDateTime(), getType());
    }
    public String toStringRaw() {
        return String.format("%s\n%s\n%s\n%s\n%d\n", getId(), getName(), getCourier(), getDateTime(), getType());
    }
    
}