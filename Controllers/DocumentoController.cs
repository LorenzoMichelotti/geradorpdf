using GeradorDocumento;
using Microsoft.AspNetCore.Mvc;

namespace GerarDocumentoPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentoController : Controller
    {
        [HttpGet(Name = "GerarDocumentoPdf")]
        public IActionResult Get()
        {
            var documento = new GeradorDocumentoServico().GerarDocumentoPdf();

            return File(documento, "application/pdf");
        }
    }
}
