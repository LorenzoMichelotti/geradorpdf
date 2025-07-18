using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Windows.Compatibility;

namespace GeradorDocumento
{
    public static class QRCodeGenerator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Vai rodar em windows")]
        public static string GenerateQRCodeBase64(string content)
        {
            var writer = new BarcodeWriter<Bitmap>
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Height = 80,  
                    Width = 80,
                    Margin = 1
                },
                Renderer = new BitmapRenderer()
            };

            using Bitmap bitmap = writer.Write(content);
            using var ms = new System.IO.MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);

            string base64 = Convert.ToBase64String(ms.ToArray());
            return $"data:image/png;base64,{base64}";
        }
    }
}
