# Semester 4, OS, laboratory work #4

## Build and run

```sh
gcc \
-Wall \
-Wextra \
-I./lib/core/include \
src/main.c \
lib/core/src/data.c \
lib/core/src/io.c \
-o bin/prog \
-lpthread \
&& ./bin/prog f1 f2
```

## Create .iso for QNX

```sh
genisoimage -J -R -V "QNX_DATA" \
-o ~/Downloads/qnx_data.iso .
```
