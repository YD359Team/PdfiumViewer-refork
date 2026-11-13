using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

#pragma warning disable CA1416

namespace PdfiumViewer.Test
{
    /// <summary>
    /// Verifies that independent documents can be loaded and rendered from
    /// several managed threads. .NET (Core) does not support creating and
    /// unloading secondary AppDomains, so the old AppDomain-based test has
    /// been replaced by a concurrency test with the same stress-test intent.
    /// </summary>
    [TestFixture]
    public class PdfiumConcurrencyFixture
    {
        [Test]
        public async Task MultipleWorkersCanLoadAndRenderDocuments()
        {
            const int workerCount = 10;
            const int iterationsPerWorker = 20;

            Task[] workers = Enumerable.Range(0, workerCount)
                .Select(worker => Task.Run(() =>
                {
                    for (int iteration = 0; iteration < iterationsPerWorker; iteration++)
                    {
                        int exampleNumber = ((worker + iteration) % 2) + 1;
                        Stream stream = OpenExample(exampleNumber);

                        using (PdfDocument document = PdfDocument.Load(stream))
                        {
                            for (int page = 0; page < document.PageCount; page++)
                            {
                                using (var image = document.Render(page, 96f, 96f, false))
                                {
                                    Assert.That(image, Is.Not.Null);
                                    Assert.That(image.Width, Is.GreaterThan(0));
                                    Assert.That(image.Height, Is.GreaterThan(0));
                                }
                            }
                        }
                    }
                }))
                .ToArray();

            await Task.WhenAll(workers);
        }

        private static Stream OpenExample(int exampleNumber)
        {
            string resourceName = string.Format(
                "{0}.Example{1}.pdf",
                typeof(PdfiumConcurrencyFixture).Namespace,
                exampleNumber);

            return typeof(PdfiumConcurrencyFixture).Assembly.GetManifestResourceStream(resourceName)
                ?? throw new InvalidOperationException(
                    "Embedded PDF test resource was not found: " + resourceName);
        }
    }
}
