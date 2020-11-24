using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteCapgeminiMvc.Models;

namespace TesteCapgeminiMvc.Controllers
{
    public class ExcelController1 : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new List<Excel>());
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            List<Excel> excels = new List<Excel>();
            var fileName = "./Excels.xlsx";

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        excels.Add(new Excel
                        {
                           /* DtEntrega = reader.GetValue(0).ToString(),
                            NomeProduto = reader.GetValue(1).ToString(),
                            Quantidade = reader.GetValue(2).ToString(),
                            ValorUnitario = reader.GetValue(3).ToString() */
                        });
                    }
                }
            }
            return View(excels);
        }
    }
}
