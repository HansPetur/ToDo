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
    public class ToDoItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUserManager manager;

        public ToDoItemsController()
        {
            manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        // GET: ToDoItems
        public async Task<ActionResult> Index()
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());

            ViewBag.Title = db.ToDoLists.Find(currentUser.CurrentListId).Name;

            return View(await db.Events.Where(list => list.Owner != null && list.Owner.Id == currentUser.CurrentListId).ToListAsync());
        }

        // GET: ToDoItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.Events.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Description,Created,IsDone")] ToDoItem toDoItem)
        {
            var currentUser = await manager.FindByIdAsync(User.Identity.GetUserId());
            if (ModelState.IsValid)
            {
                toDoItem.Owner = await db.ToDoLists.FindAsync(currentUser.CurrentListId);
                db.Events.Add(toDoItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(toDoItem);
        }

        // GET: ToDoItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.Events.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Description,Created,IsDone")] ToDoItem toDoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDoItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(toDoItem);
        }

        // GET: ToDoItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDoItem toDoItem = await db.Events.FindAsync(id);
            if (toDoItem == null)
            {
                return HttpNotFound();
            }
            return View(toDoItem);
        }

        // POST: ToDoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ToDoItem toDoItem = await db.Events.FindAsync(id);
            db.Events.Remove(toDoItem);
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
