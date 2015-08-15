using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CalendarApp.Models;
using System.Text;
using System.Data.Entity.Core.Objects;
using CalendarApp.Common;
using Gma.QrCodeNet.Encoding;
using System.IO;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;

namespace CalendarApp.Controllers
{
    public class EventController : Controller
    {
        // GET: /Event/
        public ActionResult Index()
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                return View(db.CalendarEvents.ToList());
            }
        }

        // GET: /Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                CalendarEvent calendarevent = db.CalendarEvents.Find(id);
                if (calendarevent == null)
                {
                    return HttpNotFound();
                }
                return View(calendarevent);
            }
        }

        // GET: /Event/Create
        public ActionResult Create()
        {
            List<SelectListItem> liEventGroups = new List<SelectListItem>();
            List<SelectListItem> liEventTypes = new List<SelectListItem>();
            List<SelectListItem> liEventGroupNames = new List<SelectListItem>();
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                if (db.EventGroupTitles.Count() > 0)
                {
                    foreach (EventGroupTitle group in db.EventGroupTitles.Where(x => !x.IsDeleted).OrderBy(x => x.Title).ToList())
                    {
                        liEventGroups.Add(new SelectListItem { Text = group.Title, Value = group.Title });
                    }
                }
                if (db.EventGroupNames.Count() > 0)
                {
                    foreach (EventGroupName group in db.EventGroupNames.Where(x => !x.IsDeleted).OrderBy(x => x.GroupName).ToList())
                    {
                        liEventGroupNames.Add(new SelectListItem { Text = group.GroupName, Value = group.GroupName });
                    }
                }
                if (db.EventTypeOnes.Count() > 0)
                {
                    foreach (EventTypeOne typ in db.EventTypeOnes.Where(x => !x.IsDeleted).OrderBy(x => x.EventTypeName).ToList())
                    {
                        liEventTypes.Add(new SelectListItem { Text = typ.EventTypeName, Value = typ.EventTypeName });
                    }
                }
            }
            ViewData["typeList"] = liEventTypes;
            ViewData["groupList"] = liEventGroups;
            ViewData["groupNameList"] = liEventGroupNames;
            return View();
        }

        // POST: /Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventTitle,Abstract,EventDescription,StartDate,EndDate,Location,Url,Price,EventGroupTitle,EventGroupName,EventTypeOne")] CalendarEvent calendarevent)
        {
            if (ModelState.IsValid)
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    try
                    {
                        calendarevent.CreatedDateTime = DateTime.UtcNow;
                        calendarevent.LastUpdateDateTime = DateTime.UtcNow;
                        calendarevent.IsDeleted = false;
                        db.CalendarEvents.Add(calendarevent);
                        db.SaveChanges();
                        return Content("Event has been created successfully!");
                    }
                    catch (Exception ex)
                    {
                        return Content("There is problem in saving the event.       Please try again.");
                    }
                }
            }
            else
            {
                return View(calendarevent);
            }
        }

        // GET: /Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                CalendarEvent calendarevent = db.CalendarEvents.Find(id);
                if (calendarevent == null)
                {
                    return HttpNotFound();
                }
                return View(calendarevent);
            }
        }

        // POST: /Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventTitle,Abstract,EventDescription,StartDate,EndDate,Location,Url,Price,EventGroupTitle,EventGroupName,EventTypeOne,CreatedDateTime,LastUpdateDateTime,IsDeleted")] CalendarEvent calendarevent)
        {
            if (ModelState.IsValid)
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    db.Entry(calendarevent).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(calendarevent);
        }

        // GET: /Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                CalendarEvent calendarevent = db.CalendarEvents.Find(id);
                if (calendarevent == null)
                {
                    return HttpNotFound();
                }
                return View(calendarevent);
            }
        }

        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                CalendarEvent calendarevent = db.CalendarEvents.Find(id);
                db.CalendarEvents.Remove(calendarevent);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult EventFilter()
        {
            List<SelectListItem> liEventGroupTitles = new List<SelectListItem>();
            List<SelectListItem> liEventGroupNames = new List<SelectListItem>();
            List<SelectListItem> liEventTypes = new List<SelectListItem>();
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                if (db.EventGroupTitles.Count() > 0)
                {
                    foreach (EventGroupTitle group in db.EventGroupTitles.Where(x => !x.IsDeleted).OrderBy(x => x.Title).ToList())
                    {
                        liEventGroupTitles.Add(new SelectListItem { Text = group.Title, Value = group.Title });
                    }
                }
                if (db.EventGroupNames.Count() > 0)
                {
                    foreach (EventGroupName group in db.EventGroupNames.Where(x => !x.IsDeleted).OrderBy(x => x.GroupName).ToList())
                    {
                        liEventGroupNames.Add(new SelectListItem { Text = group.GroupName, Value = group.GroupName });
                    }
                }
                if (db.EventTypeOnes.Count() > 0)
                {
                    foreach (EventTypeOne typ in db.EventTypeOnes.Where(x => !x.IsDeleted).OrderBy(x => x.EventTypeName).ToList())
                    {
                        liEventTypes.Add(new SelectListItem { Text = typ.EventTypeName, Value = typ.EventTypeName });
                    }
                }
            }
            ViewData["typeList"] = liEventTypes;
            ViewData["groupList"] = liEventGroupTitles;
            ViewData["groupNameList"] = liEventGroupNames;

            return View();
        }

        [HttpPost]
        public ActionResult EventFilter(FormCollection formCollection)
        {
            try
            {
                string guid = Guid.NewGuid().ToString();
                string filterValue = CreateFilterXml(formCollection);
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    ObjectParameter guidOutput = new ObjectParameter("Guid", typeof(string));
                    guidOutput.Value = guid;
                    db.usp_AddEventFilter(filterValue, guidOutput);
                    guid = Convert.ToString(guidOutput.Value);

                }
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    List<CalendarEvent> calendarEvents = db.usp_GetEventsByGuid(guid).ToList();
                    if (calendarEvents == null || calendarEvents.Count == 0)
                        return Content("No event is associated with your filter criteria. Please try different filter settings.,0");
                }
                //guid changed to base 64 encrypted string
                guid = Extensions.EncodeTo64(guid);
                ViewData["eventUrl"] = HttpContext.Request.Url.ToString().Replace("EventFilter", "DownloadEvent/") + guid;
                return Content("Your filter has been applied, please download the calendar event!," + HttpContext.Request.Url.ToString().Replace("EventFilter", "DownloadEvent/") + guid);
            }
            catch (Exception)
            {
                return Content("There is problem while working on filter. Please try again.");
            }
        }

        private string CreateFilterXml(FormCollection formCollection)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<EventFilter>");
            stringBuilder.Append("<Groups>");


            if (formCollection["hfEventGroupName"].Equals("All"))
                stringBuilder.Append(string.Format("<GroupName>{0}</GroupName>", "All"));
            else
            {
                foreach (string organizerName in formCollection["hfEventGroupName"].Split(','))
                {
                    stringBuilder.Append(string.Format("<GroupName>{0}</GroupName>", organizerName));
                }
            }

            if (formCollection["hfEventGroupTitle"].Equals("All"))
                stringBuilder.Append(string.Format("<GroupTitle>{0}</GroupTitle>", "All"));
            else
            {
                foreach (string organizerName in formCollection["hfEventGroupTitle"].Split(','))
                {
                    stringBuilder.Append(string.Format("<GroupTitle>{0}</GroupTitle>", organizerName));
                }
            }

            stringBuilder.Append("</Groups>");

            stringBuilder.Append("<EventTypes>");
            if (formCollection["hfEventTypeOne"].Equals("All"))
                stringBuilder.Append(string.Format("<EventTypeOne>{0}</EventTypeOne>", "All"));
            else
            {
                foreach (string eventType in formCollection["hfEventTypeOne"].Split(','))
                {
                    stringBuilder.Append(string.Format("<EventTypeOne>{0}</EventTypeOne>", eventType));
                }
            }
            stringBuilder.Append("</EventTypes>");

            if (!string.IsNullOrEmpty(formCollection["StartDate"]))
            {
                stringBuilder.Append(string.Format("<StartDate>{0}</StartDate>", formCollection["StartDate"]));
            }
            if (!string.IsNullOrEmpty(formCollection["EndDate"]))
            {
                stringBuilder.Append(string.Format("<EndDate>{0}</EndDate>", formCollection["EndDate"]));
            }
            stringBuilder.Append("</EventFilter>");

            return stringBuilder.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public void DownloadEvent(string id)
        {
            this.Download(id);
        }

        /// <summary>
        /// ics file download
        /// </summary>
        /// <param name="guid"></param>
        private void Download(string guid)
        {
            if (!string.IsNullOrEmpty(guid))
            {

                List<CalendarEvent> calendarEvents;
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    calendarEvents = db.usp_GetEventsByGuid(Extensions.DecodeFrom64(guid)).ToList();
                }
                string fileName = Guid.NewGuid().ToString() + ".ics";
                Response.Write("BEGIN:VCALENDAR" + Environment.NewLine);
                Response.Write("VERSION:2.0" + Environment.NewLine);
                Response.Write("PRODID: -//ScheduleOnce//EN" + Environment.NewLine);
                Response.Write("METHOD:PUBLISH" + Environment.NewLine);
                Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                StringBuilder eventInICSFormat = new StringBuilder();
                foreach (CalendarEvent calEvent in calendarEvents)
                {
                    eventInICSFormat.Append("BEGIN:VEVENT" + Environment.NewLine);
                    eventInICSFormat.Append("DTEND:" + (calEvent.EndDate.ToString("s").Replace("-", "").Replace(":", "")) + Environment.NewLine);
                    eventInICSFormat.Append("DTSTART:" + calEvent.StartDate.ToString("s").Replace("-", "").Replace(":", "") + Environment.NewLine);
                    StringBuilder eventDescription = new StringBuilder();
                    if (!string.IsNullOrWhiteSpace(calEvent.Abstract))
                    {
                        eventDescription.Append(calEvent.Abstract + "\\n");
                    }
                    if (!string.IsNullOrWhiteSpace(calEvent.EventDescription))
                    {
                        eventDescription.Append("Event Details : " + calEvent.EventDescription.Replace(Environment.NewLine, "\\n") + "\\n");
                    }
                    if (!string.IsNullOrWhiteSpace(calEvent.Url))
                    {
                        eventDescription.Append("Url : " + calEvent.Url + "\\n");
                    }
                    if (calEvent.Price.HasValue)
                    {
                        eventDescription.Append("Price : " + calEvent.Price.Value.ToString());
                    }
                    eventInICSFormat.Append("DESCRIPTION:" + eventDescription.ToString() + Environment.NewLine);
                    eventInICSFormat.Append("LOCATION:" + calEvent.Location + Environment.NewLine);
                    eventInICSFormat.Append("PRIORITY:5" + Environment.NewLine);
                    eventInICSFormat.Append("SEQUENCE:0" + Environment.NewLine);
                    eventInICSFormat.Append("SUMMARY;LANGUAGE=en-us:" + calEvent.EventTitle + Environment.NewLine);
                    eventInICSFormat.Append("TRANSP:OPAQUE" + Environment.NewLine);
                    eventInICSFormat.Append(string.Format("UID:{0}", Guid.NewGuid().ToString().ToUpper()) + Environment.NewLine);
                    eventInICSFormat.Append("BEGIN:VALARM" + Environment.NewLine);
                    eventInICSFormat.Append("TRIGGER:-PT15M" + Environment.NewLine);
                    eventInICSFormat.Append("ACTION:DISPLAY" + Environment.NewLine);
                    eventInICSFormat.Append("DESCRIPTION:Reminder" + Environment.NewLine);
                    eventInICSFormat.Append("END:VALARM" + Environment.NewLine);
                    eventInICSFormat.Append("END:VEVENT" + Environment.NewLine);
                }
                eventInICSFormat.Append("END:VCALENDAR" + Environment.NewLine);
                Response.Write(eventInICSFormat);
                Response.End();
            }
        }

        public ActionResult GetQRCode(string text)
        {
            //BarcodeImage(text);


            ViewData["eventUrl"] = text;
            return View("GetQRCodeView");
        }
        /// <summary>
        /// Qr Code generation
        /// </summary>
        /// <param name="barcodeText"></param>
        /// <returns></returns>
        public ActionResult BarcodeImage(String barcodeText)
        {
            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(barcodeText, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);
            Stream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);

            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;
            var resultStream = new FileStreamResult(memoryStream, "image/png");
            resultStream.FileDownloadName = String.Format("{0}.png", barcodeText);
            return resultStream;


        }
    }
}