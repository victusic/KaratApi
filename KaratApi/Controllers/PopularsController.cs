using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KaratApi.Models;

namespace KaratApi.Controllers
{
    public class PopularsController : ApiController
    {
        private KaratDbEntities db = new KaratDbEntities();

        // GET: api/Populars
        public IQueryable<Popular> GetPopulars()
        {
            return db.Populars;
        }

        // GET: api/Populars/5
        [ResponseType(typeof(Popular))]
        public IHttpActionResult GetPopular(int id)
        {
            Popular popular = db.Populars.Find(id);
            if (popular == null)
            {
                return NotFound();
            }

            return Ok(popular);
        }

        // PUT: api/Populars/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPopular(int id, Popular popular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != popular.Id)
            {
                return BadRequest();
            }

            db.Entry(popular).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopularExists(id))
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

        // POST: api/Populars
        [ResponseType(typeof(Popular))]
        public IHttpActionResult PostPopular(Popular popular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Populars.Add(popular);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = popular.Id }, popular);
        }

        // DELETE: api/Populars/5
        [ResponseType(typeof(Popular))]
        public IHttpActionResult DeletePopular(int id)
        {
            Popular popular = db.Populars.Find(id);
            if (popular == null)
            {
                return NotFound();
            }

            db.Populars.Remove(popular);
            db.SaveChanges();

            return Ok(popular);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PopularExists(int id)
        {
            return db.Populars.Count(e => e.Id == id) > 0;
        }
    }
}