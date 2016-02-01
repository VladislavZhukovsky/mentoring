using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;

namespace PDFSharpTest
{
    public class Program
    {
        static int PdfWidthPixels = 614;

        static void Main(string[] args)
        {
            double x = (double)1920 / 614;
            int y = (int)Math.Round(1200 / x);

            var path = @"D:\Vlad\Multimedia\venom-logo.jpg";
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            AddPicture(gfx, page, path, 0, 0);

            doc.Save(Path.GetFileNameWithoutExtension(path) + ".pdf");
        }

        static void AddPicture(XGraphics gfx, PdfPage page, string imagePath, int xPosition, int yPosition)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException(String.Format("Could not find image {0}.", imagePath));
            }

            XImage xImage = XImage.FromFile(imagePath);
            gfx.DrawImage(xImage, xPosition, yPosition, PdfWidthPixels, (double)xImage.PixelHeight * PdfWidthPixels / xImage.PixelWidth);//xImage.PixelWidth, xImage.PixelWidth);
        }
    }
}
