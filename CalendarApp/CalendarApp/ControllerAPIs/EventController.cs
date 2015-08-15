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
using CalendarApp.Models;

namespace CalendarApp.ControllerAPIs
{
    public class EventController : ApiController
    {
        // GET api/Event
        public IQueryable<CalendarEvent> GetCalendarEvents()
        {
            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                return eventOrganizerEntities.CalendarEvents;
            }
        }

        // GET api/Event/5
        [ResponseType(typeof(CalendarEvent))]
        public IHttpActionResult GetCalendarEvent(int id)
        {
            CalendarEvent calendarevent;
            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                calendarevent = eventOrganizerEntities.CalendarEvents.Find(id);
            }
            if (calendarevent == null)
            {
                return NotFound();
            }

            return Ok(calendarevent);
        }

        // PUT api/Event/5
        public IHttpActionResult PutCalendarEvent(int id, CalendarEvent calendarevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calendarevent.Id)
            {
                return BadRequest();
            }

            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                try
                {
                    eventOrganizerEntities.Entry(calendarevent).State = EntityState.Modified;
                    eventOrganizerEntities.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarEventExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Event
        [ResponseType(typeof(CalendarEvent))]
        public IHttpActionResult PostCalendarEvent(CalendarEvent calendarevent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                eventOrganizerEntities.CalendarEvents.Add(calendarevent);
                eventOrganizerEntities.SaveChanges();
            }

            return CreatedAtRoute("DefaultApi", new { id = calendarevent.Id }, calendarevent);
        }

        // DELETE api/Event/5
        [ResponseType(typeof(CalendarEvent))]
        public IHttpActionResult DeleteCalendarEvent(int id)
        {
            CalendarEvent calendarevent;
            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                calendarevent = eventOrganizerEntities.CalendarEvents.Find(id);
                if (calendarevent == null)
                {
                    return NotFound();
                }
                else
                {
                    eventOrganizerEntities.CalendarEvents.Remove(calendarevent);
                    eventOrganizerEntities.SaveChanges();
                }
            }
            return Ok(calendarevent);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool CalendarEventExists(int id)
        {
            using (EventOrganizerEntities eventOrganizerEntities = new EventOrganizerEntities())
            {
                return eventOrganizerEntities.CalendarEvents.Count(e => e.Id == id) > 0;
            }
        }
    }
}