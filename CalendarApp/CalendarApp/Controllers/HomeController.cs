using CalendarApp.Common;
using CalendarApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalendarApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEvent()
        {
            ViewBag.Message = "Please add your event here.";

            return View();
        }

        public ActionResult AdminLogin()
        {
            ViewBag.Message = "Please enter your login details";

            return View();
        }
        public ActionResult AdminLoginValidate(FormCollection formCollection)
        {
            try
            {
                //string userId = formCollection["UserId"];
                string psswrd = formCollection["Password"];
                if (psswrd.Equals("adminLogin"))
                {
                    using (EventOrganizerEntities db = new EventOrganizerEntities())
                    {
                        ViewData["grouplist"] = db.EventGroupNames.ToList();
                        ViewData["EventGroupTitle"] = db.EventGroupTitles.ToList();
                        ViewData["EventTypeOne"] = db.EventTypeOnes.ToList();
                    }
                    return View("AdminPanel");
                }
                else
                {
                    return Content("There is problem while working on filter. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return Content("There is problem while working on filter. Please try again.");
            }
        }

        public string SaveChanges(int id, string value, Purpose purpose)
        {
            try
            {
                switch (purpose)
                {
                    case Purpose.DeleteGroupName:
                        DeleteGroupName(id);
                        break;
                    case Purpose.DeleteGroupTitle:
                        DeleteGroupTitle(id);
                        break;
                    case Purpose.DeleteTYpe:
                        DeleteTitleType(id);
                        break;
                    case Purpose.EditGroupName:
                        EditGroupName(value, id);
                        break;
                    case Purpose.EditGroupTitle:
                        EditGroupTitle(value, id);
                        break;
                    case Purpose.EditType:
                        EditTitleType(value, id);
                        break;
                    case Purpose.AddGroupName:
                        AddGroupName(value);
                        break;
                    case Purpose.AddGroupTitle:
                        AddGroupTitle(value);
                        break;
                    case Purpose.AddTitleType:
                        AddTitleType(value);
                        break;
                }


                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    ViewData["grouplist"] = db.EventGroupNames.ToList();
                    ViewData["EventGroupTitle"] = db.EventGroupTitles.ToList();
                    ViewData["EventTypeOne"] = db.EventTypeOnes.ToList();
                }
                return "true";
            }
            catch
            {
                return "false";
            }
        }

        private bool DeleteGroupName(int Id)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                var item = db.EventGroupNames.Where(x => x.Id == Id).FirstOrDefault();
                if (item != null)
                {

                    item.IsDeleted = true;
                    db.SaveChanges();

                }
            }
            return false;
        }

        private bool DeleteGroupTitle(int Id)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                var item = db.EventGroupTitles.Where(x => x.Id == Id).FirstOrDefault();
                if (item != null)
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                }
            }
            return false;
        }

        private bool DeleteTitleType(int Id)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                var item = db.EventTypeOnes.Where(x => x.Id == Id).FirstOrDefault();
                if (item != null)
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                }
            }
            return false;
        }

        private void EditGroupName(string groupName, int id)
        {
            if (!string.IsNullOrEmpty(groupName))
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    var item = db.EventGroupNames.Where(x => x.Id == id).FirstOrDefault();
                    db.Usp_UpdateCalendarEvents(groupName, item.GroupName, (int)Purpose.EditGroupName);
                    if (item != null)
                    {
                        item.GroupName = groupName;
                        db.SaveChanges();
                    }
                }
            }
        }

        private void EditGroupTitle(string groupTitle, int id)
        {
            if (!string.IsNullOrEmpty(groupTitle))
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    var item = db.EventGroupTitles.Where(x => x.Id == id).FirstOrDefault();
                    db.Usp_UpdateCalendarEvents(groupTitle, item.Title, (int)Purpose.EditGroupTitle);
                    if (item != null)
                    {
                        item.Title = groupTitle;
                        db.SaveChanges();
                    }
                }
            }
        }
        private void EditTitleType(string titleType, int id)
        {
            if (!string.IsNullOrEmpty(titleType))
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    var item = db.EventTypeOnes.Where(x => x.Id == id).FirstOrDefault();
                    db.Usp_UpdateCalendarEvents(titleType, item.EventTypeName, (int)Purpose.EditType);
                    if (item != null)
                    {
                        item.EventTypeName = titleType;
                        db.SaveChanges();
                    }
                }
            }
        }

        private void AddGroupName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                using (EventOrganizerEntities db = new EventOrganizerEntities())
                {
                    var item = db.EventGroupNames.Where(x => x.GroupName == name).FirstOrDefault();
                    if (item == null)
                    {
                        EventGroupName obj = new EventGroupName();
                        obj.GroupName = name;
                        obj.IsDeleted = false;
                        obj.CreatedDateTime = DateTime.UtcNow;
                        obj.LastUpdateDateTime = DateTime.UtcNow;
                        db.EventGroupNames.Add(obj);
                        db.SaveChanges();
                    }
                }
            }
        }

        private void AddGroupTitle(string name)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                var item = db.EventGroupTitles.Where(x => x.Title == name).FirstOrDefault();
                if (item == null)
                {
                    EventGroupTitle obj = new EventGroupTitle();
                    obj.Title = name;
                    obj.IsDeleted = false;
                    obj.CreatedDateTime = DateTime.UtcNow;
                    obj.LastUpdateDateTime = DateTime.UtcNow;
                    db.EventGroupTitles.Add(obj);
                    db.SaveChanges();
                }
            }
        }

        private void AddTitleType(string name)
        {
            using (EventOrganizerEntities db = new EventOrganizerEntities())
            {
                var item = db.EventTypeOnes.Where(x => x.EventTypeName == name).FirstOrDefault();
                if (item == null)
                {
                    EventTypeOne obj = new EventTypeOne();
                    obj.EventTypeName = name;
                    obj.IsDeleted = false;
                    obj.CreatedDateTime = DateTime.UtcNow;
                    obj.LastUpdateDateTime = DateTime.UtcNow;
                    db.EventTypeOnes.Add(obj);
                    db.SaveChanges();
                }
            }
        }
        public ActionResult DownloadEvent()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}