using System.Buffers.Text;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace GeradorDocumento
{
    public class GeradorDocumentoServico
    {
        private string EtiquetaComponent()
        {
            var qrCode = QRCodeGenerator.GenerateQRCodeBase64("1234567890-01/01");

            return $@"
                <td class=""etiqueta-container"">
                    <table>
                        <tr>
                            <td>
                                <img src=""{qrCode}"" alt=""QR Code"" width=""80"" height=""80"" />
                            </td>
                            <td class=""etiqueta-conteudo"">
                                <table>
                                    <tr>
                                        <th>PEDIDO</th>
                                        <th>VOLUME</th>
                                    </tr>
                                    <tr>
                                        <td>123456</td>
                                        <td>01/01</td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <th>NOME CLIENTE</th>
                                    </tr>
                                    <tr>
                                        <td>NOME SOBRENOME</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            ";
        }

        public byte[] GerarDocumentoPdf()
        {
            var html = $@"
                <!DOCTYPE html>
                    <html lang=""en"">
                    <head>
                        <meta charset=""UTF-8"">
                        <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                        <title>Document</title>
                        <style>
                            .page {{
                                size: 21cm 29.7cm;
                                margin: 1cm;
                            }}
                            th, td {{
                                text-align: left;
                                vertical-align: middle;
                                padding: 2px 4px 2px 4px;
                            }}
                            .cabecalho {{
                                border-bottom: 1px solid black;
                                padding-bottom: 4px;
                                margin-bottom: 32px;
                                width: 100%;
                            }}
                            .etiqueta-conteudo {{
                                vertical-align: top;
                            }}
                            .etiquetas-container {{
                                width: 100%;
                                border-collapse: collapse;
                            }}
                            .etiqueta-container {{
                                padding: 8px 16px;
                                border: 1px dashed black;
                            }}
                    </style>
                    </head>
                    <body class=""page"">
                        <h1>Hello World</h1>
                        <table class=""cabecalho"">
                            <thead>
                                <tr>
                                    <th>Hello</th>
                                    <th>Hello</th>
                                    <th>Hello</th>
                                    <th>Hello</th>
                                    <th>Hello</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>123456</td>
                                    <td>1234567890</td>
                                    <td>ababa xeberepepe aaa</td>
                                    <td>1</td>
                                    <td>1</td>
                                </tr>
                            </tbody>
                        </table>
                        <table class=""etiquetas-container"">
                            <tr>
                                {EtiquetaComponent()}
                                {EtiquetaComponent()}
                            </tr>
                            <tr>
                                {EtiquetaComponent()}
                                {EtiquetaComponent()}
                            </tr>
                        </table>
                    </body>
                </html>
            ";

            var document = PdfGenerator.GeneratePdf(html, PdfSharpCore.PageSize.A4);

            using var stream = new MemoryStream();
            document.Save(stream, false);

            return stream.ToArray();
        }
    }
}
