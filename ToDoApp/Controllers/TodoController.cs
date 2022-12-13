using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoApp.DAL;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class TodoController : Controller
    {
        ToDoDAL tododal = new ToDoDAL();
        // GET: Todo
        public ActionResult Index()
        {
            var list = tododal.GetAllTodos();
            return View(list);
        }

        // GET: Todo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        public ActionResult Create(TodoModel todo)
        {
            bool isCreated = false;
            try
            {
                // TODO: Add insert logic here
                isCreated = tododal.Create(todo);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            var list = tododal.GetATodo(id);

            return View(list[0]);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, TodoModel todo)
        {
            bool isUpdated = false;
            try
            {
                isUpdated = tododal.Update(id, todo);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] =ex.Message;
                return View();
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            var todo = tododal.GetATodo(id);
            return View(todo);
        }

        // POST: Todo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, TodoModel todo)
        {

            bool isDeleted = false;
            try
            {
                // TODO: Add insert logic here
                isDeleted = tododal.Delete(todo);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
           
        }
    }
}
