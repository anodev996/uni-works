# Build

```sh
javac -d prog_small prog_small/Main.java && \
javac -d prog_medium prog_medium/Main.java && \
javac -d prog_large prog_large/Main.java
```

# Run

```sh
java -cp prog_small Main && \
java -cp prog_medium Main && \
java -cp prog_large Main
```

# Build & run

```sh
javac -d prog_small prog_small/Main.java && \
java -cp prog_small Main && \
javac -d prog_medium prog_medium/Main.java && \
java -cp prog_medium Main && \
javac -d prog_large prog_large/Main.java && \
java -cp prog_large Main
```