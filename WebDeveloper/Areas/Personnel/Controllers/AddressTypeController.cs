﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDeveloper.Model;
using WebDeveloper.Repository;

namespace WebDeveloper.Areas.Personnel.Controllers
{
    public class AddressTypeController : PersonBaseController<AddressType>
    {
        public AddressTypeController(IRepository<AddressType> repository)
            :base(repository)
        {

        }
        // GET: Personnel/AddressType
        public ActionResult Index()
        {
            return View();
        }
    }
}