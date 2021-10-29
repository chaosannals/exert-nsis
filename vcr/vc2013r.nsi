; 安装器名称
Name "VC2013DEMO"

; 安装器文件名
OutFile "vc2013r.exe"

; 执行需要用户级别
RequestExecutionLevel admin

;强制压缩
SetCompressor /SOLID lzma
SetCompress force

; 是否使用 Unicode
Unicode True

; 安装路径
InstallDir $PROGRAMFILES\vc2013demo

!include "vc2013r_util.nsi"

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
    
    File /oname=vc2013redist_x86.exe "vc2013redist_x86.exe"

    Call InstallVC2013Redist

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