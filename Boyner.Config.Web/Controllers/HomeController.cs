using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Boyner.Config.Web.Models;
using Boyner.Config.Model;
using Boyner.Config.Service;
using Boyner.Config.Domain.Repository;
using Boyner.Configurator;

namespace Boyner.Config.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ConfigService _configService;

        public HomeController(ConfigService configService)
        {
            _configService = configService;
        }
        public IActionResult Index()
        {
            var configs = _configService.GetAll();
            HomeViewModel viewModel = new HomeViewModel
            {
                Configs = configs
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ConfigModel viewModel = new ConfigModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ConfigModel viewModel)
        {
            ConfigModel model = _configService.Create(viewModel);

            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                model = new ConfigModel();
                return View(model);
            }
        }

        public IActionResult Update(string id)
        {
            ConfigModel viewModel = _configService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(ConfigModel viewModel)
        {
            ConfigModel model = _configService.Update(viewModel);
            //return Info Message
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            _configService.Delete(id);
            //return Info Message
            return RedirectToAction("Index");
        }

        public IActionResult Test()
        {
            ConfigurationReader configurationReader = new ConfigurationReader("SERVICE-A", "mongodb://localhost:27017", 60000);
            string svalue = configurationReader.GetValue<string>("SiteName");
            bool bvalue = configurationReader.GetValue<bool>("IsBasketEnabled");
            int ivalue = configurationReader.GetValue<int>("MaxItemCount");
            double dvalue = configurationReader.GetValue<double>("Price");

            TestViewModel viewModel = new TestViewModel()
            {
                StringValue = svalue,
                BoolValue = bvalue,
                IntegerValue = ivalue,
                DoubleValue = dvalue
            };
            return View(viewModel);
        }

    }
}
