﻿using DAL;
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

        [Route]
        public ActionResult MainView()
        {
            return View(new MainModel());
        }


        [Route("UploadFile")]
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
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

            return View("MainView", model);
        }

        [Route("GetByCode")]
        [HttpPost]
        public ActionResult GetByCode(string code)
        {
            var model = new MainModel
            {
                Transactions = dataService.Get(code).ToUIString()
            };

            return View("MainView", model);
        }

        [Route("GetByDateRange")]
        [HttpPost]
        public ActionResult GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var model = new MainModel
            {
                Transactions = dataService.Get(startDate, endDate).ToUIString()
            };
            
            return View("MainView", model);
        }

        [Route("GetByStatus")]
        [HttpPost]
        public ActionResult GetByStatus(Status? status)
        {
            var transactions = status.HasValue
                ? dataService.Get((int)status).ToUIString()
                : StringResources.NoResultsString;

            var model = new MainModel
            {
                Transactions = transactions
            };

            return View("MainView", model);
        }
    }
}