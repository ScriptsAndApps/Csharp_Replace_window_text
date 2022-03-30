@echo off
echo Run me as admin if you are debugging!!
taskkill /f /im TitleChangercsharp.exe
cd %~dp0
for /l %%a in (1,1,2) do (
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /out:TitleChangercsharp.exe /target:winexe vlcremovebranding.cs && (
     #you could start if is no errors
     #TitleChangercsharp.exe
     
  exit
) || (
  echo ERROR!
)
)
pause
