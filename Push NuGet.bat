@echo off
setlocal EnableExtensions EnableDelayedExpansion
pushd "%~dp0"
if errorlevel 1 exit /b 1

if "%NUGET_API_KEY%"=="" (
    echo Set the NUGET_API_KEY environment variable before pushing.
    popd
    exit /b 1
)

set "PACKAGE="
for %%F in (artifacts\PdfiumViewer.Refork.*.nupkg) do if exist "%%~fF" (
    if defined PACKAGE (
        echo Multiple PdfiumViewer.Refork packages found in artifacts.
        popd
        exit /b 1
    )
    set "PACKAGE=%%~fF"
)

if not defined PACKAGE (
    echo No PdfiumViewer.Refork package found in artifacts.
    popd
    exit /b 1
)

dotnet nuget push "!PACKAGE!" --api-key "%NUGET_API_KEY%" --source "https://api.nuget.org/v3/index.json" --skip-duplicate
set "EXIT_CODE=%ERRORLEVEL%"
popd
endlocal & exit /b %EXIT_CODE%
