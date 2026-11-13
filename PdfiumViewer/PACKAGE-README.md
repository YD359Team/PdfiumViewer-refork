# PdfiumViewer.Refork

Windows Forms PDF viewer and rendering library based on PDFium.

## Installation

Add one package reference to the application project:

```xml
<PackageReference Include="PdfiumViewer.Refork" Version="1.0.0" />
```

The package brings the x86 and x64 PDFium native packages transitively and
copies the matching native DLLs to the application's build and publish output.
No separate native package references or manual DLL-copy settings should be
needed for ordinary `PackageReference` consumers.

## Target frameworks

- .NET Framework 4.8 (`net48`)
- .NET 10 for Windows (`net10.0-windows`)

This is a Windows-only library because the viewer uses Windows Forms and GDI.

## Ownership

`PdfDocument.Load(Stream)` takes ownership of the supplied stream. The stream
is disposed when the returned document is disposed.

## Native engine

The current package uses the PdfiumViewer native V8/XFA binaries version
`2018.4.8.256`. This package automates their delivery; it does not upgrade the
PDFium engine itself. Those legacy native binaries may require the appropriate
Microsoft Visual C++ runtime on the target machine.

## License

The managed library is distributed under Apache-2.0. PDFium and bundled
components retain their respective licenses; see the repository license files.
