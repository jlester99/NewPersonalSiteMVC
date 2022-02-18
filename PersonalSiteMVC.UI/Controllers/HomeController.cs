using System;
using System.Web.Mvc;
using PersonalSiteMVC.UI.Models;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace PersonalSiteMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Classmates()
        {
            return View();
        }

        public ActionResult Resume()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                //send the cvm back to the form with completed inputs
                return View(cvm);
            }
            //build the message
            string message = $"An email has been received from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";
            //MailMessage - What sends the email
            MailMessage mm = new MailMessage(
            //FROM
            ConfigurationManager.AppSettings["EmailUser"].ToString(),
            //TO
            ConfigurationManager.AppSettings["EmailTo"].ToString(),
            cvm.Subject,
            message);

            //MailMessage properties
            //Allow HTML in the email
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            //Respond to the sender, and not our website
            mm.ReplyToList.Add(cvm.Email);

            //SmtpClient - This is the info from the host that allows this to be sent
            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

            //Client Credentials
            client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(), ConfigurationManager.AppSettings["EmailPass"].ToString());

            //Try to send the email
            try
            {
                //attempt to send
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be completed at this time. Please try again later. Error Message:<br/>{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);
        }
    }//end class
}//end namespace