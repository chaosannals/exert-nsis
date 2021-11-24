﻿; 安装器名称
Name "OneSetup"

; 安装器文件名
OutFile "one-setup.exe"

; 执行需要用户级别
RequestExecutionLevel admin

;强制压缩
SetCompressor /SOLID lzma
SetCompress force

; 是否使用 Unicode
Unicode True

; 安装路径
InstallDir $PROGRAMFILES\One

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

    ; /r 指定匹配复制到顶层，中间可以加入 /x "" 多个来排除
    File /r /x "*setup.exe" /x "*.nsi" "*"

    ; 添加卸载程序
    WriteUninstaller "$INSTDIR\uninstall.exe"

    ; 添加注册表，加入“添加删除程序”
    ; 图标路径，可以直接指向带图标的 exe 或者 ico 文件
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\One" "DisplayIcon" "$INSTDIR\one.ico"
    ; 程序名
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\One" "DisplayName" "One Nsis Demo"
    ; 版本信息
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\One" "DisplayVersion" "1.0"
    ; 卸载程序路径
    WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\One" "UninstallString" "$INSTDIR\uninstall.exe"
SectionEnd

; 卸载段脚本
Section "Uninstall"
    ; 删除注册表信息
    DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\One"
    ; 删除目录
    RMDir /r "$INSTDIR"
SectionEnd