;安装 VC2013
Function "InstallVC2013Redist"
    Push $R0

    ; 查注册表 这里以 验证，这里要预先查看自己的 vc2013redist_x86.exe 的注册表信息，然后去处理。
    ClearErrors
    ReadRegStr $R0 HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{13A4EE12-23EA-3371-91EE-EFB36DDFFF3E}" "Version"
    IfErrors 0 VS2013Installed ; 存在就跳到已安装

    ; 64位的有多个注册表可能的位置，也查一下。
    ClearErrors
    ReadRegStr $R0 HKLM "SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{13A4EE12-23EA-3371-91EE-EFB36DDFFF3E}" "Version"
    IfErrors 0 VS2013Installed ; 存在就跳到已安装
    
    Exec "$INSTDIR\vc2013redist_x86.exe" ; 不存在则安装
    ; Goto VS2013Installed

VS2013Installed:
    Pop $R0
FunctionEnd