# exert-nsis

*.nsi 文件必须以 UTF-8 with BOM 编码。

## DnSvc

- 需要自行下载 dotnetfx35.exe（.Net Framework 3.5 SP1 ）安装包。

## 证书

```bash
openssl req -newkey rsa:2048 -nodes -keyout dnsvc.key -x509 -days 365 -out dnsvc.cer
openssl pkcs12 -export -in dnsvc.cer -inkey dnsvc.key -out dnsvc.pfx
```
