### Build & Run scripts

#### Build

*shell :*

```shell
javac \
--module-path "/c/_vscode_portable/javafx-sdk-21.0.9/lib" \
--add-modules javafx.controls,javafx.fxml,javafx.graphics \
-d out \
src/BisectionMethodVisualizer.java
 ```

 #### Run

*shell :*

 ```shell
javaw \
--module-path "/c/_vscode_portable/javafx-sdk-21.0.9/lib" \
--add-modules javafx.controls,javafx.fxml,javafx.graphics \
-cp out \
BisectionMethodVisualizer
 ```
