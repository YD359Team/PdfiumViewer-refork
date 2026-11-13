using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace PdfiumViewer
{
    /// <summary>
    /// Configuration for printing multiple PDF pages on a single page.
    /// </summary>
    public class PdfPrintMultiplePages
    {
        /// <summary>
        /// Gets the number of pages to print horizontally.
        /// </summary>
        public int Horizontal { get; }

        /// <summary>
        /// Gets the number of pages to print vertically.
        /// </summary>
        public int Vertical { get; }

        /// <summary>
        /// Gets the orientation in which PDF pages are layed out on the
        /// physical page.
        /// </summary>
        public Orientation Orientation { get; }

        /// <summary>
        /// Gets the margin between PDF pages in device units.
        /// </summary>
        public float Margin { get; }

        /// <summary>
        /// Creates a new instance of the PdfPrintMultiplePages class.
        /// </summary>
        /// <param name="horizontal">The number of pages to print horizontally.</param>
        /// <param name="vertical">The number of pages to print vertically.</param>
        /// <param name="orientation">The orientation in which PDF pages are layed out on
        /// the physical page.</param>
        /// <param name="margin">The margin between PDF pages in device units.</param>
        public PdfPrintMultiplePages(int horizontal, int vertical, Orientation orientation, float margin)
        {
            if (horizontal < 1)
                throw new ArgumentOutOfRangeException(nameof(horizontal), horizontal, "Horizontal page count must be at least one.");
            if (vertical < 1)
                throw new ArgumentOutOfRangeException(nameof(vertical), vertical, "Vertical page count must be at least one.");
            if (!Enum.IsDefined(typeof(Orientation), orientation))
                throw new ArgumentOutOfRangeException(nameof(orientation));
            if (Single.IsNaN(margin) || Single.IsInfinity(margin) || margin < 0)
                throw new ArgumentOutOfRangeException(nameof(margin), margin, "Margin must be finite and non-negative.");
            if ((long)horizontal * vertical > Int32.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(horizontal), "The total pages per sheet is too large.");

            Horizontal = horizontal;
            Vertical = vertical;
            Orientation = orientation;
            Margin = margin;
        }
    }
}
