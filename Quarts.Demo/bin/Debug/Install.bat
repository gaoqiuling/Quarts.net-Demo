%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe /u %~dp0\Quarts.Demo.exe
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe  "%~dp0\Quarts.Demo.exe"
rem sc config JobService1 start= auto
rem Net Start JobService1
pause