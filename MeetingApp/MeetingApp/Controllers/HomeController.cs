using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MeetingApp.DAL;
using MeetingApp.Models;
using PagedList.Mvc;
using PagedList;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
        private WPG_ConferenceEntities _db;
        public HomeController()
        {
            _db = new WPG_ConferenceEntities();
        }
        public ActionResult Index(int? page)
        {
            var model = new IndexModel();
            var user = _db.Employees.FirstOrDefault(x => x.Email == User.Identity.Name);
            var empInfo = _db.Employees.FirstOrDefault(x => x.Email == User.Identity.Name);
            var meetings = empInfo?.Meeting_Employee.Where(x => x.Employee_Id == empInfo.Id).Select(x => x.Meeting)
                .ToList();
            var notes = empInfo?.Notes.ToList();
            var guests = empInfo?.Employee_Guest.Where(x => x.Employee_Id == empInfo.Id).Select(x => x.Guest).ToList();
            if (empInfo != null)
            {
                 model = new IndexModel()
                {
                    Employee = empInfo,
                    Meetings = meetings,
                    Guests = guests,
                    Notes = notes
                };
            }

            return View(model);
        }


    }
}