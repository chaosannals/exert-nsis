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
