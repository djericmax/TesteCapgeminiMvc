﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteCapgeminiMvc.Models;

namespace TesteCapgeminiMvc.Controllers
{
    public class ExcelController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<Excel> excels = null)
        {
            excels = excels == null ? new List<Excel>() : excels;
            return View(new List<Excel>(excels));
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var excels = this.GetExcelList(file.FileName);
            return Index(excels);
        }

        private List<Excel> GetExcelList(string fName)
        {
            List<Excel> excels = new List<Excel>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            excels.Add(new Excel()
                            {
                                DtEntrega = Convert.ToDateTime(reader.GetValue(0)),
                                NomeProduto = reader.GetValue(1).ToString(),
                                Quantidade = Convert.ToInt32(reader.GetValue(2)),
                                ValorUnitario = Convert.ToDecimal(reader.GetValue(3))
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("erro" + ex);
                        }
                    }
                }
            }
            return excels;
        }
    }
}