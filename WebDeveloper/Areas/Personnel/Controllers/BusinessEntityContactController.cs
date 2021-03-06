﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class BusinessEntityContactController : PersonBaseController<BusinessEntityContact>
    {
        public BusinessEntityContactController(IRepository<BusinessEntityContact> repository)
            :base(repository)
        {

        }
        // GET: Personnel/BusinessEntityContact
        public ActionResult Index()
        {
            return View();
        }
    }
}