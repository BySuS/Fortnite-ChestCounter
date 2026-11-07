@echo off
chcp 65001 >nul
title Building ChestCounter
cd /d "%~dp0"

where dotnet >nul 2>nul
if errorlevel 1 (
  echo The .NET 10 SDK is not installed.
  echo Download it here: https://dotnet.microsoft.com/download/dotnet/10.0  ^(".NET SDK", Windows x64^)
  echo Then run build.bat again.
  pause
  exit /b 1
)

echo Building (1 to 2 minutes the first time)...
dotnet publish src\ChestCounter\ChestCounter.csproj -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:EnableCompressionInSingleFile=true -p:DebugType=none -o dist
if errorlevel 1 (
  echo.
  echo The build failed. See the message above.
  pause
  exit /b 1
)

echo.
echo Done! The file to share: %~dp0dist\ChestCounter.exe
explorer dist
pause
