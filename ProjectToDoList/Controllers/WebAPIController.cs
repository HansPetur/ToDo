using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProjectToDoList.Models;
using System.Web.Http.Cors;

namespace ProjectToDoList.Controllers
{
    [EnableCors("*", "*", "*")]
    public class WebAPIController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WebAPI
        public IQueryable<ToDoList> GetToDoLists()
        {
            return db.ToDoLists;
        }

        // GET: api/WebAPI/5
        [ResponseType(typeof(ToDoList))]
        public async Task<IHttpActionResult> GetToDoList(int id)
        {
            ToDoList toDoList = await db.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            return Ok(toDoList);
        }

        // PUT: api/WebAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutToDoList(int id, ToDoList toDoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDoList.Id)
            {
                return BadRequest();
            }

            db.Entry(toDoList).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoListExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/WebAPI
        [ResponseType(typeof(ToDoList))]
        public async Task<IHttpActionResult> PostToDoList(ToDoList toDoList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ToDoLists.Add(toDoList);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = toDoList.Id }, toDoList);
        }

        // DELETE: api/WebAPI/5
        [ResponseType(typeof(ToDoList))]
        public async Task<IHttpActionResult> DeleteToDoList(int id)
        {
            ToDoList toDoList = await db.ToDoLists.FindAsync(id);
            if (toDoList == null)
            {
                return NotFound();
            }

            db.ToDoLists.Remove(toDoList);
            await db.SaveChangesAsync();

            return Ok(toDoList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ToDoListExists(int id)
        {
            return db.ToDoLists.Count(e => e.Id == id) > 0;
        }
    }
}