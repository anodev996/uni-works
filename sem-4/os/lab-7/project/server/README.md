# Semester 4, OS, laboratory work #5

## Build and run

```sh
gcc \
-Wall \
-Wextra \
-I./src \
src/server/main.c \
-o bin/server
```

## Build .iso for QNX

```sh
genisoimage -J -R -V "QNX_DATA" \
-o ~/Downloads/qnx_data.iso .
```
