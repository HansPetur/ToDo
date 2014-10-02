using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectToDoList.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectToDoList.Controllers
{
    [Authorize]
    public class ToDoListsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager manager;

        public ToDoListsController()
        {
            manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        // GET: ToDoLists
        public async Task<ActionResult> Index()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            return View(db.ToDoLists.Where(todo => todo.Owner != null && todo.Owner.Id == currentUser.Id).ToList());
        }

        // GET: ToDoLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OwnerID,Name")] ToDoList toDoList)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                toDoList.Owner = currentUser;

                db.ToDoLists.Add(toDoList);

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(toDoList);
        }

        // GET: ToDoLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = await db.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OwnerID,Name")] ToDoList toDoList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoList).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(toDoList);
        }

        // GET: ToDoLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoList toDoList = await db.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return HttpNotFound();
            }
            return View(toDoList);
        }

        // POST: ToDoLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ToDoList toDoList = await db.ToDoLists.FindAsync(id);
            db.ToDoLists.Remove(toDoList);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
