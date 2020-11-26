using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesteCapgeminiMvc.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using TesteCapgeminiMvc.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TesteCapgeminiMvc.Controllers
{
    public class ExcelController : Controller
    {
        private readonly DataContext _context;
        
        public ExcelController(DataContext context)
        {
            _context = context;
        }

        //private readonly IConfiguration configuration;

        /*    public ExcelController(IConfiguration config)
             {
                 this.configuration = config;
             } */

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
            try
            {
                using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    try
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
                                    Console.WriteLine($"Tipo de dado não reconhecido. Erro: '{ex}'");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.TryAddModelError("Error", "Este tipo de arquivo não pode ser carregado. Tente um novo arquivo no formato .xlsx");
                        //Console.Log($"Tipo de arquivo não aceito. Erro: '{ex}'");
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Tipo de arquivo não aceito. Erro: '{ex}'");
            }
            return excels;
        }

        /// <summary>
        /// Método que irá receber uma lista do modelo excel e salvar no banco de dados 
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SalvarBd(List<Excel> modelList)
        {
            bool erro = false;
            try
            {
                _context.AddRangeAsync(modelList);
                _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                erro = true;
            }
            return Json(erro);
        }
    }
}