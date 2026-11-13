@echo off
setlocal
pushd "%~dp0"
if errorlevel 1 exit /b 1

dotnet pack "PdfiumViewer\PdfiumViewer.csproj" -c Release -o "artifacts"
set "EXIT_CODE=%ERRORLEVEL%"
popd
endlocal & exit /b %EXIT_CODE%
