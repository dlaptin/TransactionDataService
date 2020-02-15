using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Utils;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private const int maxFileSize = 1024 * 1024;
        private const string noResultsString = "No results.";

        public ActionResult Index()
        {
            return View(new MainModel());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            var validationMessages = new List<string>();

            if (file.ContentLength > maxFileSize)
            {
                validationMessages.Add("Max file size is 1MB.");
            }
            else if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);

                validationMessages = new ParserService.ParserService().ProcessFile(path);
                System.IO.File.Delete(path);
            }

            var model = new MainModel
            {
                ValidationMessages = string.Join(Environment.NewLine, validationMessages)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GetByCode(MainModel model)
        {
            var connString = ConfigurationManager.AppSettings["connectionString"];
            var items = new DataService(connString).Get(model.Code).Select(item => item.ToUIString());

            if (items.Count() > 0)
            {
                model.Transactions = string.Join(Environment.NewLine, items);
            }
            else
            {
                model.Transactions = noResultsString;
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult GetByDateRange(MainModel model)
        {
            var connString = ConfigurationManager.AppSettings["connectionString"];
            var items = new DataService(connString).Get(model.StartDate.Value, model.EndDate.Value).Select(item => item.ToUIString());

            if (items.Count() > 0)
            {
                model.Transactions = string.Join(Environment.NewLine, items);
            }
            else
            {
                model.Transactions = noResultsString;
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult GetByStatus(MainModel model)
        {
            var connString = ConfigurationManager.AppSettings["connectionString"];

            var items = model.Status.HasValue 
                ? new DataService(connString).Get((int)model.Status).Select(item => item.ToUIString())
                : new List<string>();

            if (items.Count() > 0)
            {
                model.Transactions = string.Join(Environment.NewLine, items);
            }
            else
            {
                model.Transactions = noResultsString;
            }

            return View("Index", model);
        }
    }
}