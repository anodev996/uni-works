## Laboratory 1 scripts

### Build

#### .sh script

```shell
gcc -o ./prog_1/prog_1.exe ./prog_1/prog_1.c && \
gcc -o ./prog_2/prog_2.exe ./prog_2/prog_2.c && \
gcc -o ./prog_3/prog_3.exe ./prog_3/prog_3.c
```

#### .bat / .cmd script

`set PATH=`_<path\to\gcc>_`;%PATH%`

```batch
^
set PATH=C:\_vscode_portable\w64devkit\bin;%PATH% && ^
gcc -o prog_1\prog_1.exe prog_1\prog_1.c && ^
gcc -o prog_2\prog_2.exe prog_2\prog_2.c && ^
gcc -o prog_3\prog_3.exe prog_3\prog_3.c
```

***

### Run

#### .sh script

```shell
./prog_1/prog_1.exe && \
./prog_2/prog_2.exe && \
./prog_3/prog_3.exe
```

#### .bat / .cmd script

```batch
^
prog_1\prog_1.exe && ^
prog_2\prog_2.exe && ^
prog_3\prog_3.exe
```

***

### Build & run

#### .sh script

```sh
gcc -o ./prog_1/prog_1.exe ./prog_1/prog_1.c && \
./prog_1/prog_1.exe && \
gcc -o ./prog_2/prog_2.exe ./prog_2/prog_2.c && \
./prog_2/prog_2.exe && \
gcc -o ./prog_3/prog_3.exe ./prog_3/prog_3.c && \
./prog_3/prog_3.exe
```

#### .bat / .cmd script

`set PATH=`_<path\to\gcc>_`;%PATH%`

```batch
^
set PATH=C:\_vscode_portable\w64devkit\bin;%PATH% && ^
gcc -o prog_1\prog_1.exe prog_1\prog_1.c && ^
prog_1\prog_1.exe && ^
gcc -o prog_2\prog_2.exe prog_2\prog_2.c && ^
prog_2\prog_2.exe && ^
gcc -o prog_3\prog_3.exe prog_3\prog_3.c && ^
prog_3\prog_3.exe
```