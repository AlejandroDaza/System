using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemTask.BDHelper;
using SystemTask.Models;

namespace SystemTask.Controllers
{
    public class StatusController : Controller
    {
        private AccessDB Bd;

        public ActionResult GetStatus()
        {
            Bd = new AccessDB();
            Bd.GetAllStatus();
            ModelState.Clear();
            return View(Bd.GetAllStatus());
        }

        [HttpGet]
        public ActionResult AddStatus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStatus( Status statusObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Bd = new AccessDB();
                    if (Bd.AddNewStatus(statusObj))
                    {
                        ViewBag.Mensagem = "Status added!";
                    }                
                }
                return View();


            }
            catch (Exception)
            {
                return View("GetStatus");
            }
        }
        
       // [HttpGet]
        public ActionResult EditStatus(int idStatus)
        {
            Bd = new AccessDB();
            return View(Bd.GetAllStatus().Find(t => t.idStatus == idStatus));
        }

        [HttpPost]
        public ActionResult EditStatus(int idStatus, Status statusObj)
        {
            try
            {
                Bd = new AccessDB();
                Bd.UpdateStatus(statusObj);
                return RedirectToAction("GetStatus");

            }
            catch (Exception)
            {
                return View("GetStatus");
            }
        }

        public ActionResult DeleteStatus(int idStatus)
        {
            try
            {
                Bd = new AccessDB();
                if (Bd.DeleteStatus(idStatus))
                {
                    ViewBag.Mensagem = "Status deleted!";
                }
                return RedirectToAction("GetStatus");

            }
            catch (Exception)
            {
                return View("GetStatus");
            }
        }
    }
}