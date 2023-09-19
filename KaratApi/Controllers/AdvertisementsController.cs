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
    public class AdvertisementsController : ApiController
    {
        private KaratDbEntities db = new KaratDbEntities();

        // GET: api/Advertisements
        public IQueryable<Advertisement> GetAdvertisements()
        {
            return db.Advertisements;
        }

        // GET: api/Advertisements/5
        [ResponseType(typeof(Advertisement))]
        public IHttpActionResult GetAdvertisement(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return Ok(advertisement);
        }

        // PUT: api/Advertisements/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdvertisement(int id, Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != advertisement.AdverId)
            {
                return BadRequest();
            }

            db.Entry(advertisement).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
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

        // POST: api/Advertisements
        [ResponseType(typeof(Advertisement))]
        public IHttpActionResult PostAdvertisement(Advertisement advertisement)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Advertisements.Add(advertisement);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = advertisement.AdverId }, advertisement);
        }

        // DELETE: api/Advertisements/5
        [ResponseType(typeof(Advertisement))]
        public IHttpActionResult DeleteAdvertisement(int id)
        {
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return NotFound();
            }

            db.Advertisements.Remove(advertisement);
            db.SaveChanges();

            return Ok(advertisement);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdvertisementExists(int id)
        {
            return db.Advertisements.Count(e => e.AdverId == id) > 0;
        }
    }
}