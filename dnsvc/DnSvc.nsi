; 安装器名称
Name "DnSvcSetup"

; 安装器文件名
OutFile "dnsvc-setup.exe"

; 执行需要用户级别
RequestExecutionLevel admin

;强制压缩
SetCompressor /SOLID lzma
SetCompress force

; 是否使用 Unicode
Unicode True

; 安装路径
InstallDir $PROGRAMFILES\DnSvc

; ------------------------------------
; 页面

Page components
Page directory ; 固定的界面选择安装目录。
Page instfiles ; 固定界面，安装文件。

UninstPage uninstConfirm ; 固定界面，卸载前确定界面。
UninstPage instfiles ; 固定界面，卸载文件。

; ------------------------------------

!include "DotNet.nsi"

; ------------------------------------

Section "Install"
    SectionIn RO ; 只读，用户不可修改。

    SetOutPath $INSTDIR

    ; /oname=输出名 输出路径
    File /oname=dotnetfx35.exe "dotnetfx35.exe"
    File /oname=dnsvc.exe "dnsvc\bin\Release\dnsvc.exe"
    File /oname=websocket-sharp.dll "dnsvc\bin\Release\websocket-sharp.dll"

    ; 下载文件
    NSISdl::download "http://nginx.org/download/nginx-1.21.3.zip" "nginx-1.21.3.zip"

    ; 添加卸载程序
    WriteUninstaller "$INSTDIR\uninstall.exe"
SectionEnd

; 卸载段脚本
Section "Uninstall"
    ; 逐个删除文件
    Delete "$INSTDIR\dnsvc.exe"
    Delete "$INSTDIR\websocket-sharp.dll"
    Delete "$INSTDIR\uninstall.exe"

    ; 删除目录，如果目录非空不会被删除。
    RMDir "$INSTDIR"
SectionEnd