PdfiumViewer-refork - build-fix overlay
======================================

Apply
-----
Extract this archive into the repository root, preserving directory structure
and allowing the listed files to be overwritten. This is an overlay, not a
complete copy of the repository.

What changed
------------
1. Directory.Build.props disables SDK AssemblyInfo generation because all four
   projects contain hand-authored Properties/AssemblyInfo.cs files. This fixes
   the duplicate AssemblyCompany/Title/Product/Version/etc. attributes.
2. PdfiumViewer.csproj removes invalid Reference elements from PropertyGroup.
   SDK-style projects use implicit framework references. A targeting-pack
   package is added for building the net48 target on machines without the
   .NET Framework developer pack.
3. PdfiumViewer.WPFDemo.csproj enables Windows Forms as well as WPF because
   MainWindow.xaml.cs uses System.Windows.Forms.OpenFileDialog.
4. PdfiumViewer.Test.csproj replaces placeholder NuGet versions with concrete
   versions and retains the embedded PDF test data.
5. MultiAppDomainFixture.cs no longer uses AppDomain.CreateDomain,
   AppDomainSetup.Evidence/ApplicationBase/ApplicationName or AppDomain.Unload.
   Those APIs are not available in the same form on modern .NET. The test now
   stresses concurrent open/render/dispose operations using worker tasks.

Build commands
--------------
Run from the repository root in a Windows developer environment:

  dotnet restore PdfiumViewer.slnx
  dotnet build PdfiumViewer.slnx -c Debug
  dotnet test PdfiumViewer.Test/PdfiumViewer.Test.csproj -c Debug

Important runtime limitation
----------------------------
The repository's Libraries/Pdfium/README.md explicitly says the native PDFium
binaries were removed. These changes address the build errors represented by
the supplied diagnostics; they do not provide Pdfium.dll. The runtime tests
will therefore require the matching native PDFium x86/x64 binaries to be
installed/copied into the locations expected by PdfiumViewer.NativeMethods.

Validation status
-----------------
The files were reviewed against the current master source and the provided
compiler diagnostics. A .NET SDK is not installed in the execution environment,
and a local build/test could not be run here. Do not interpret this overlay as
a verified successful build until the commands above have been run on Windows.
