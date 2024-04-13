using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text.Json;

namespace FinanceTracker.Controllers
{
    public class PartialController : Controller
    {
        public IActionResult Index(int number)
        {
            ViewData["number"] = number;
            return View();
        }

        [HttpPost]
        public IActionResult _FirstPage(int number)
        {
            ViewData["number"] = number;
            return PartialView();
        }
        [HttpPost]
        public IActionResult _SecondPage(int number)
        {
            ViewData["number"] = number;
            return PartialView();
        }
        [HttpPost]
        public IActionResult _ThirdPage(int number)
        {
            /*
            var boblista = new List<bob>();
            for (int i = 0; i < number; i++)
                boblista.Add(new bob());
            var json = JsonSerializer.Serialize(boblista);
            ViewData["jsonbobit"] = json;-*/
            return PartialView();
        }
        [HttpPost]
        public string ReceiveData(string package)
        {
            try
            {
                //var boblist = new List<bob>();
                //boblist = JsonSerializer.Deserialize<List<bob>>(package);
                return "Great success";
            }
            catch
            {
                return "Pooped";
            }
        }
    }
}
