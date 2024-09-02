using MailApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace MailApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(FormModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Message = "Eksik bilgi var!!!";
                return View();
            }

            var client = new SmtpClient("smtp.eu.mailgun.org", 587)
            {
                Credentials = new NetworkCredential("postmaster@bildirim.miraykaragoz.com.tr", ""),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("postmaster@bildirim.miraykaragoz.com.tr", "Miray Karag�z"),
                Subject = "miraykaragoz.com.tr'den mesaj var!",
                Body = $@"Kimden: {model.Name}<br>
                       E-Posta adresi: {model.Email}<br>
                       Konu: {model.Subject}<br>
                       Mesaj: {model.Message}",
                IsBodyHtml = true
            };

            mailMessage.ReplyToList.Add("miraykaragoz@hotmail.com");

            mailMessage.To.Add("miraykaragoz@hotmail.com");

            client.Send(mailMessage);

            ViewBag.Message = "G�nderildi!";

            return View(); 
        }
    }
}
