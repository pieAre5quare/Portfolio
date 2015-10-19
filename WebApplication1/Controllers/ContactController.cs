using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication1.Controllers
{
    [RequireHttps]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage(string firstname, string lastname, string email, string message)
        {
            var name = firstname + " " + lastname;
            new EmailService().SendAsync(new IdentityMessage()
            {
                Destination = email,
                Subject = name,
                Body = message
            });

            TempData["Message"] = "Your contact message has been sent";

            return RedirectToAction("Index");
        }
    }
}