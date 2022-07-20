using DAL.ViewModel;
using System;
using System.Net;
using System.Net.Mail;

namespace BLL.Helper.SendMail
{
    public class MailSender
    {
        public static string SendMail(MailViewModel model)
        {


            try
            {

                // Insert Host Name and Port Number
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                // Encrypt for request
                smtp.EnableSsl = true;

                // Insert Credentials
                smtp.Credentials = new NetworkCredential("conceptacademy8@gmail.com", "qliutqmigkpcalps");

                // Send Your Message
                smtp.Send("conceptacademy8@gmail.com", model.Email, model.Title, model.Message);

                var Result = "Mail Sent";

                return Result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
