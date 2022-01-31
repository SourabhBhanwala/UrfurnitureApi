
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.IO;
using urfurniture.Models;
using urfurniture.DAL.Data;
using urfurniture.DAL.Repository;
using urfurniture.DAL.Entities;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace urfurniture.DAL.Implementation
{

    public class MailService : IMail
    {

        private readonly EmailSetting _mailSettings;
        private readonly urfurnitureContext _dbcontext;
        private readonly IConfiguration _configuration;
        public MailService(IOptions<EmailSetting> mailSettings, urfurnitureContext dbcontext, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _configuration = configuration;
            _dbcontext = dbcontext;
        }
        //verification Email
        public async Task<Tuple<bool, string>> SendConfirmationEmailAsync(TblUser user)
        {
       
            _mailSettings.Password= _configuration["MailPassword"];
            string scheme = "https";
            string host = "urfurnitureapi.azurewebsites.net";
            Random r = new Random();
            string OTP = r.Next(1000, 9999).ToString();


            // var user = await _dbcontext.TblUsers.FirstOrDefaultAsync(x=>x.Email==toemail);
            var varifyUrl = scheme + "://" + host + ":" + "/api/ManageAccount/ConfirmEmail?userid=" + user.UserId + "&otp=" + OTP;

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://urfurnitureadmin.file.core.windows.net/template/EmailConfirmation.html?sp=r&st=2021-03-24T14:47:07Z&se=2038-02-18T14:47:00Z&sv=2020-02-10&sig=IAZSt2idxAfrrS1avBPUrU6kro1mpgGvjgthodW0A1A%3D&sr=f");
            ///var pageContents =
            //string FilePath = filepath + "/Template/EmailConfirmation.html";
                //StreamReader str = new StreamReader(FilePath);
                string MailText = await response.Content.ReadAsStringAsync();
            //str.ReadToEnd();
            //str.Close();

                MailText = MailText.Replace("[confirmlink]", varifyUrl);
                var email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Mail)
                };
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = $"Welcome {user.FirstName}";
                var builder = new BodyBuilder
                {
                    HtmlBody = MailText
                };
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                TblOtpConfirmation otp = new TblOtpConfirmation
                {
                    UserId = user.UserId,
                    Otp = OTP,
                    Email = user.Email,
                    IsActive = true

                };
                await _dbcontext.TblOtpConfirmations.AddAsync(otp);
                await _dbcontext.SaveChangesAsync();
            
            
            return new Tuple<bool, string>(true, "Email Send Successfully Please verify Your Email");

        }
        public async Task SendWelcomeEmailAsync(string toemail, string username)
        {
            _mailSettings.Password = _configuration["MailPassword"];

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://urfurnitureadmin.file.core.windows.net/template/WelcomeTemplate.html?sp=r&st=2021-03-24T14:54:28Z&se=2027-06-26T14:54:00Z&sv=2020-02-10&sig=fxewZHVvL9AXc6gYLmaXl8Jg3YN0N6LkyMdJ8O1bt2M%3D&sr=f");
            ///var pageContents =
            //string FilePath = filepath + "/Template/EmailConfirmation.html";
            //StreamReader str = new StreamReader(FilePath);
            string MailText = await response.Content.ReadAsStringAsync();
            //str.ReadToEnd();
            //str.Close();

            MailText = MailText.Replace("[username]", username);
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };


            email.To.Add(MailboxAddress.Parse(toemail));
            email.Subject = $"Welcome {username}";
            var builder = new BodyBuilder
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task<Tuple<bool, string>> SendForgetPasswordEmail(TblUser user)
        {
            _mailSettings.Password = _configuration["MailPassword"];
            Random r = new Random();
            string OTP = r.Next(10000, 99999).ToString();


            var varifyUrl = OTP;



            HttpClient client = new HttpClient();
            var response = await client.GetAsync(" https://urfurnitureadmin.file.core.windows.net/template/ForgetPassword.html?sp=r&st=2021-03-24T14:53:04Z&se=2030-06-19T14:53:00Z&sv=2020-02-10&sig=cj%2FnwX9EcBQGnTh8vsjpGbaot7lTqYcdwXEsK%2BLhWsE%3D&sr=f");
            ///var pageContents =
            //string FilePath = filepath + "/Template/EmailConfirmation.html";
            //StreamReader str = new StreamReader(FilePath);
            string MailText = await response.Content.ReadAsStringAsync();
            //str.ReadToEnd();
            //str.Close();

            MailText = MailText.Replace("[confirmlink]", varifyUrl);
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = $"Recover Password";
            var builder = new BodyBuilder
            {
                HtmlBody = MailText
            };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            TblOtpConfirmation otp = new TblOtpConfirmation
            {
                UserId = user.UserId,
                Otp = OTP,
                Email = user.Email,
                IsActive = true
            };
            await _dbcontext.TblOtpConfirmations.AddAsync(otp);
            await _dbcontext.SaveChangesAsync();
            return new Tuple<bool, string>(true, "Email Send Successfully Please Enter Otp");
        }
    }
}