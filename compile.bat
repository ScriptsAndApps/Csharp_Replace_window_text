@echo off
echo Run me as admin if you are debugging!!
taskkill /f /im vlcremovebranding.exe
cd %~dp0
for /l %%a in (1,1,2) do (
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:vlcremovebranding.exe /target:winexe vlcremovebranding.cs && (
     #you could start if is no errors
     #vlcremovebranding.exe
     
  exit
) || (
  echo ERROR!
)
)
pause
