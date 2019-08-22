using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace WebAPI.Controllers
{
    [Authorize]

    public class ValuesController : ApiController
    {
        TaskManagementEntities db = new TaskManagementEntities();
        // GET api/values
     
        [HttpGet]
        public IEnumerable<Tasks> GetTasks()
        {
        
            return db.DBTasks;
        }

        // GET api/values/5
       
        [HttpGet]
        public IHttpActionResult GetTask(int id)
        {
            Tasks ts = db.DBTasks.Find(id);
            if (ts == null)
            {
                return NotFound();
            }
            return Ok(ts);
        }

        // POST api/values
      
        [HttpPost]
        public IHttpActionResult Post([FromBody] Tasks task)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.DBTasks.Add(task);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = task.Quote_ }, task);
        }

        // PUT api/values/5
       
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Tasks task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != task.Quote_)
            {
                return BadRequest();
            }
            db.Entry(task).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(id))
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

        // DELETE api/values/5

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            Tasks ts = db.DBTasks.Find(id);
            if (ts == null)
            {
                return NotFound();
            }
            db.DBTasks.Remove(ts);
            db.SaveChanges();
            return Ok(ts);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TaskExists(int id)
        {
            return db.DBTasks.Count(t => t.Quote_ == id) > 0;
        }
    }
}
