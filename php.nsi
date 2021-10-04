; 安装器名称
Name "PHPSetup"

; 安装器文件名
OutFile "php-setup.exe"

; 执行需要用户级别
RequestExecutionLevel admin

;强制压缩
SetCompressor /SOLID lzma
SetCompress force

; 是否使用 Unicode
Unicode True

; 安装路径
InstallDir $PROGRAMFILES\php

; ------------------------------------
; 页面

Page components
Page directory ; 固定的界面选择安装目录。
Page instfiles ; 固定界面，安装文件。

UninstPage uninstConfirm ; 固定界面，卸载前确定界面。
UninstPage instfiles ; 固定界面，卸载文件。

; ------------------------------------

Section "Install"
    SectionIn RO ; 只读，用户不可修改。

    SetOutPath $INSTDIR

    ; 下载文件，这个下载不了，php 官网做了反爬。
    NSISdl::download "https://windows.php.net/downloads/releases/php-8.0.11-nts-Win32-vs16-x64.zip" "php-8.0.11-nts-Win32-vs16-x64.zip"

    ; 添加卸载程序
    WriteUninstaller "$INSTDIR\uninstall.exe"
SectionEnd

; 卸载段脚本
Section "Uninstall"
    ; 逐个删除文件
    Delete "$INSTDIR\uninstall.exe"

    ; 删除目录，如果目录非空不会被删除。
    RMDir "$INSTDIR"
SectionEnd