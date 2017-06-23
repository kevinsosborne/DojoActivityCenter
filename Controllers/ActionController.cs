using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DojoActivityCenter.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DojoActivityCenter.Controllers
{
    public class ActionController : Controller
    {
        private CenterContext _context;

        public ActionController(CenterContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("home")]
        public IActionResult Home()
        {
            int UserId = (int)HttpContext.Session.GetInt32("user_id");
            User CurrUser = _context.Users.Where( User => User.UserId == UserId).SingleOrDefault();
            ViewBag.CurrUser = CurrUser;
            ViewBag.UserId = UserId;

            List<Activity> AllActivities = _context.Activities
                    .Include(activiites => activiites.User)
                    .Include(activities => activities.Participants)
                    .OrderBy(x => x.Date)
                    .ToList();
            ViewBag.AllActivities = AllActivities;

            return View("Home");
        }
        [HttpGet]
        [Route("/logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Login");
        }
        [HttpGet]
        [Route("/new")]
        public IActionResult New()
        {
            ViewBag.ActivityErrors = new List<string>(); 
            return View("newactivitypage");
        }
        [HttpPost]
        [Route("/newactivity")]
        public IActionResult NewActivity(ActivityViewModel activity)
        {
            int UserId = (int)HttpContext.Session.GetInt32("user_id");

            DateTime DatetoCheck = DateTime.Now;
            // if (activity.Date < DatetoCheck)
            // {
            //     this.ModelState.AddModelError("Date", "Date has to be in the future");
            // }

            if (ModelState.IsValid)
            {
                Activity newActivity = new Activity
                {
                    Title = activity.Title,
                    Time = activity.Time,
                    Date = activity.Date,
                    Duration = activity.Duration,
                    DurationType = activity.DurationType,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = UserId,
                    Description = activity.Description
                };
                _context.Activities.Add(newActivity);
                _context.SaveChanges();
                return RedirectToAction("activity", new { id = newActivity.ActivityId });
                
            }
            else{
                ViewBag.ActivityErrors = ModelState.Values;
                return View("newactivitypage");
            }
        }
        [HttpGet]
        [Route("/activity/{id}")]
        public IActionResult Activity(int id)
        {
            int UserId = (int)HttpContext.Session.GetInt32("user_id");
            ViewBag.UserId = UserId;

            Activity displayActivity = _context.Activities.Where(Activity => Activity.ActivityId == id).Include(Activity => Activity.User).SingleOrDefault();
            ViewBag.displayActivity = displayActivity;
            
            Activity displayActivityGuests = _context.Activities.Where(Activity => Activity.ActivityId == id).Include(Activity => Activity.Participants).ThenInclude(Participant => Participant.User).SingleOrDefault();
            ViewBag.displayActivityGuests = displayActivityGuests;
            return View("activitypage");
        }
        [HttpGet]
        [Route("/delete/{id}")]
        public IActionResult Delete(int id)
        {
            List<Participant> RemoveParticipants = _context.Participants.Where(participant => participant.ActivityId == id).ToList();
            foreach( var participant in RemoveParticipants)
            {
                _context.Remove(participant);
            }
            _context.SaveChanges();

            int UserId = (int)HttpContext.Session.GetInt32("user_id");
            Activity RemoveActivity = _context.Activities.Where(activities => activities.ActivityId == id).Where(activities => activities.UserId == UserId).SingleOrDefault();
            _context.Remove(RemoveActivity);
            _context.SaveChanges();
            
            return RedirectToAction("home");
        }
        [HttpGet]
        [Route("/join/{id}")]
        public IActionResult Join(int id)
        {
        int UserId = (int)HttpContext.Session.GetInt32("user_id");
        Participant NewParticipant = new Participant
            {
             ActivityId = id,
             UserId = UserId   
            };
            _context.Participants.Add(NewParticipant);
            _context.SaveChanges();

            return RedirectToAction("home");
        }
        [HttpGet]
        [Route("/leave/{id}")]
        public IActionResult Leave(int id)
        {
        int UserId = (int)HttpContext.Session.GetInt32("user_id");
        Participant LeaveParticipant = _context.Participants.Where(participant => participant.ActivityId == id).Where(participant => participant.UserId == UserId).SingleOrDefault();
        _context.Participants.Remove(LeaveParticipant);
        _context.SaveChanges();
        
        return RedirectToAction("home");
        }



    }
}

