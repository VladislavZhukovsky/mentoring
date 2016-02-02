using DocumentProcessor.Core;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentProcessor.PdfProcessor
{
    public class PdfProcessor: IProcessor
    {
        private const string PDF_EXTENSION = ".pdf";

        private NamingManager namingManager;

        public PdfProcessor()
        {
            namingManager = new NamingManager();
        }

        public void Process(IEnumerable<string> files, string destinationFolder)
        {
            var nextPdfName = namingManager.GetNextDocumentName(destinationFolder, PDF_EXTENSION);
            var pdfPath = Path.Combine(destinationFolder, nextPdfName + PDF_EXTENSION);
            CreatePdf(files, pdfPath);
        }

        private void CreatePdf(IEnumerable<string> imagePaths, string pdfPath)
        {
            PdfDocument doc = new PdfDocument();
            foreach (var imagePath in imagePaths)
            {
                PdfPage page = doc.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                AddPicture(gfx, page, imagePath, 0, 0);
            }
            doc.Save(pdfPath);
        }

        private void AddPicture(XGraphics gfx, PdfPage page, string imagePath, int xPosition, int yPosition)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException(String.Format("Could not find image {0}.", imagePath));
            }

            XImage xImage = XImage.FromFile(imagePath);
            int imagePdfWidth;
            int imagePdfHeight;
            GetPdfImageSize(page, xImage, out imagePdfWidth, out imagePdfHeight);
            gfx.DrawImage(xImage, xPosition, yPosition, imagePdfWidth, imagePdfHeight);
        }

        private void GetPdfImageSize(PdfPage page, XImage image, out int width, out int height)
        {
            if (image.PixelWidth > page.Width)
            {
                var k = (double)image.PixelWidth / page.Width.Value;
                width = (int)page.Width.Value;
                height = (int)Math.Round(image.PixelHeight / k);
            }
            else
            {
                width = image.PixelWidth;
                height = image.PixelHeight;
            }
            if (height > page.Height)
            {
                var k = (double)height / page.Height.Value;
                height = (int)page.Height.Value;
                width = (int)Math.Round(width / k);
            }
        }
    }
}
