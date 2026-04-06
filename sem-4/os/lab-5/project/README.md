# Semester 4, OS, laboratory work #5

## Build and run

```sh
gcc \
-Wall \
-Wextra \
#-I./lib/core/include \
#src/main.c \
#lib/core/src/data.c \
#lib/core/src/io.c \
-o bin/prog \
#-lpthread \
```

## Build .iso for QNX

```sh
genisoimage -J -R -V "QNX_DATA" \
-o ~/Downloads/qnx_data.iso .
```
