using Microsoft.AspNetCore.Mvc;
using ST10070933_PROG7312_MunicipalServices.Models;
using ST10070933_PROG7312_MunicipalServices.Services;
using System.Diagnostics;

namespace ST10070933_PROG7312_MunicipalServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _data;


        public HomeController(IDataService data)
        {
            _data = data;
        }


        public IActionResult Index()
        {
            // The main menu. We will only enable Report Issues; other options are placeholders/disabled until implemented.
            return View();
        }


        public IActionResult About()
        {
            return View();
        }
    }
}
