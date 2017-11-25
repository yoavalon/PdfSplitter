using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace PdfPicker
{
    public class Filter
    {
        public int GetNumberOfPages(string path)
        {
            int Pages = -1;

            try
            {
                PdfReader pdfReader = new PdfReader(path);
                Pages = pdfReader.NumberOfPages;
            }
            catch (Exception ex)
            {
                HandleException(ex, false);
            }
            return Pages;
        }

        public void CreateNewPdf(string path, string targetFile, List<int> Pages)
        {
            try
            {
                using (FileStream stream = new FileStream(targetFile, FileMode.Create))
                using (Document doc = new Document())
                using (PdfCopy pdf = new PdfCopy(doc, stream))
                {
                    doc.Open();

                    PdfReader reader = new PdfReader(path);
                    PdfImportedPage page = null;

                    foreach (int pageNumber in Pages)
                    {
                        page = pdf.GetImportedPage(reader, pageNumber);
                        pdf.AddPage(page);
                    }

                    pdf.FreeReader(reader);
                    reader.Close();
                }
             }
            catch (Exception ex)
            {
                HandleException(ex, false);
            }
        }

        private void HandleException(Exception ex, bool Show)
        {

        }

    }
}
