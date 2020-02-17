using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.Utils;

namespace WebUI.Controllers
{
    public class DataServiceController : Controller
    {
        private const int maxFileSize = 1024 * 1024;
        private readonly IDataService dataService;

        public DataServiceController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public ActionResult MainView()
        {
            return View(new MainModel());
        }

        [HttpPost]
        public ActionResult MainView(HttpPostedFileBase file)
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
            model.Transactions = dataService.Get(model.Code).ToUIString();

            return View("MainView", model);
        }

        [HttpPost]
        public ActionResult GetByDateRange(MainModel model)
        {
            model.Transactions = dataService.Get(model.StartDate.Value, model.EndDate.Value).ToUIString();

            return View("MainView", model);
        }

        [HttpPost]
        public ActionResult GetByStatus(MainModel model)
        {
            model.Transactions = model.Status.HasValue 
                ? dataService.Get((int)model.Status).ToUIString()
                : StringResources.NoResultsString;

            return View("MainView", model);
        }
    }
}