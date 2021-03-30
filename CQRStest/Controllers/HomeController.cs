using CQRStest.Commands;
using CQRStest.Handler;
using CQRStest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CQRStest.Controllers
{
    public class HomeController : Controller
    {
        private ProductsCommandHandler _commandHandler;

        public HomeController(ProductsCommandHandler commandHandler)
        {
            this._commandHandler = commandHandler;
        }
        public IActionResult Index()
        {
            var command = new CreateProductCommand
            {
                Name = "Eggs",
                CurrentStock = 34,
                Description = "very good",
                UnitPrice = 78
            };

            _commandHandler.Handler(command);

           return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
