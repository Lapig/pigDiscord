;Gui, 1:+ToolWindow
;Gui, 1:Font, s8
;Gui, 1:Add, Text, x3 y2 vguiErr, 
;Gui, Color, 008000


;Gui, 1:Show, x1000 y500 w300 h200, poeGUI
;Gui, 1:-Caption +AlwaysOnTop +Disabled +E0x20 +LastFound
;Winset,TransColor, 0xFFFFFF


RandSleep(x,y) {
Random, rand, %x%, %y%
Sleep %rand%
}

;;;PM All
F7::
Loop, read, names.txt
{
if GetKeyState("F3", "P"){
	return
}
line := A_LoopReadLine  
if InStr(line, "stackSize"){
	continue
}
size := StrLen(line) - 11
name := SubStr(line,11,size)
;MsgBox % SubStr(last_line,11,size)
SendInput {Enter}@
Sleep 30
SendInput, %name%
Sleep 30
SendInput {Space}Hi, offer 10c for your last hope
Sleep 10
SendInput {Enter}
RandSleep(1500, 1700)
}
return

;;;Only pm if > stackSize
F8::
Loop, read, names.txt
{
if GetKeyState("F3", "P"){
	return
}
line := A_LoopReadLine  
if InStr(line, "stackSize"){
	stack := SubStr(line, 11, StrLen(line))
	if stack > 2
	{
		SendInput {Enter}@
		Sleep 30
		SendInput, %name%
		Sleep 30
		SendInput {Space}Hi, offer 1c each for your ESSENCE, buying all
		Sleep 10
		SendInput {Enter}
		RandSleep(1500, 1700)	
	}
}
size := StrLen(line) - 11
name := SubStr(line,11,size)
}
return

#If A_IsSuspended=0
LAlt & 1::
{
	;Gui, Color, FF0000
	Suspend
}
#If
return

#If A_IsSuspended
LAlt & 1::
	Suspend
	;Gui, Color, 008000
#If
