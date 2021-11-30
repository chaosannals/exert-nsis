# one

dev.nsi 定义了变量，可以通过 makensisw 编译。

```bash
# 因为引入了外部定义的常量，所以需要通过命令行执行。
makensis /DONE_VERSION="0.2.0" one.nsi
```
