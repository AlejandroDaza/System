using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SystemTask.BDHelper;
using SystemTask.Models;

namespace SystemTask.Controllers
{
    public class TaskController : Controller
    {
        private BDHelperTask Bd;

        public ActionResult GetTask()
        {
            Bd = new BDHelperTask();
            Bd.GetAllTaskGrid();
            ModelState.Clear();
            return View(Bd.GetAllTaskGrid());
        }

        [HttpGet]
        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(Task taskObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int newstatus = Convert.ToInt32(Request.Form["selectStatus"]);

                    Bd = new BDHelperTask();
                    int newId = Bd.GetCountTask()+1;
                    taskObj.IdTask = newId;
                    if (Bd.AddNewTask(taskObj))
                    {
                        ViewBag.Mensagem = "Task added!";
                    }
                }
                return RedirectToAction("GetTask");


            }
            catch (Exception)
            {
                return View("GetTask");
            }
        }

        // [HttpGet]
        public ActionResult EditTask(int idTask)
        {
            Bd = new BDHelperTask();
            return View(Bd.GetAllTask().Find(t => t.IdTask == idTask));
        }

        [HttpPost]
        public ActionResult EditTask(int idTask, Task taskObj)
        {
            try
            {
                Bd = new BDHelperTask();
                Bd.UpdateTask(taskObj);
                return RedirectToAction("GetTask");

            }
            catch (Exception)
            {
                return View("GetTask");
            }
        }

        public ActionResult DeleteTask(int idTask)
        {
            try
            {
                Bd = new BDHelperTask();
                if (Bd.DeleteTask(idTask))
                {
                    ViewBag.Mensagem = "Task deleted!";
                }
                return RedirectToAction("GetTask");

            }
            catch (Exception)
            {
                return View("GetTask");
            }
        }


       


    }

}