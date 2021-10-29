# exert-nsis

安装 NSIS 3.x 版本，之后会有一个图形软件用于编译。

注：*.nsi 文件必须以 UTF-8 with BOM 编码。

## DnSvc

- 需要自行下载 dotnetfx35.exe（.Net Framework 3.5 SP1 ）安装包。

## 证书

```bash
openssl req -newkey rsa:2048 -nodes -keyout dnsvc.key -x509 -days 365 -out dnsvc.cer
openssl pkcs12 -export -in dnsvc.cer -inkey dnsvc.key -out dnsvc.pfx
```

## 通过注册表判断是否安装

安装自己需要的运行库，这需要运行库在添加删除面板里面有卸载功能。
此时打开 注册表编辑器 在 
- 默认路径但是不全 在 HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
- 64位下的兼容路径32位64位混一堆未验证是否全 在 HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall
找到相应的注册表，判断是否存在来做验证是否安装。

```bash
# 打开注册表
regedit
```
