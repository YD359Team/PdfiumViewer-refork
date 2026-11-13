using System.Drawing;

namespace PdfiumViewer;

public readonly struct PdfViewState
{
    public PdfViewState(double zoom, PdfViewerZoomMode zoomMode, PdfRotation rotation, Point scrollOffset, int pageIndex)
    {
        Zoom = zoom;
        ZoomMode = zoomMode;
        Rotation = rotation;
        ScrollOffset = scrollOffset;
        PageIndex = pageIndex;
    }

    public readonly double Zoom { get; }
    public readonly PdfViewerZoomMode ZoomMode { get; }
    public readonly PdfRotation Rotation { get; }

    public readonly Point ScrollOffset { get; }
    public readonly int PageIndex { get; }
}
