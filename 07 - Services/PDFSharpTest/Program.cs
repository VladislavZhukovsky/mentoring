﻿using System;
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
        static int DPI = 72;
        static double PDF_WIDTH_INCHES = 8.27;
        static double PDF_HEIGHT_INCHES = 11.69;
        static int PDF_PIXELWIDTH = 595;
        static int PDF_PIXELHEIGHT = 842;

        static void Main(string[] args)
        {
            double x = (double)1920 / 614;
            int y = (int)Math.Round(1200 / x);

            var path = @"War_Machine_mk_1_.jpg";
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);



            AddPicture(gfx, page, path, 0, 0);

            doc.Save(Path.GetFileNameWithoutExtension(path) + "_1.pdf");
        }

        static void AddPicture(XGraphics gfx, PdfPage page, string imagePath, int xPosition, int yPosition)
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException(String.Format("Could not find image {0}.", imagePath));
            }

            XImage xImage = XImage.FromFile(imagePath);
            int imagePdfWidth;
            int imagePdfHeight;
            GetPdfImageSize(page, xImage, out imagePdfWidth, out imagePdfHeight);
            gfx.DrawImage(xImage, xPosition, yPosition, imagePdfWidth, imagePdfHeight);//xImage.PixelWidth, xImage.PixelWidth);
        }

        static void GetPdfImageSize(PdfPage page, XImage image, out int width, out int height)
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
