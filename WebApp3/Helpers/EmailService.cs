using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace WebApp3.Helpers
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            #region pass

            string password = "bulay5325353256";
            #endregion
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "alexbas13@mail.ru")); // откуда
            emailMessage.To.Add(new MailboxAddress("", email)); // куда
            emailMessage.Subject = subject; // тема сообщения
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) // содержимоое в формате html
            {
                Text = message
            };
            using (var client = new SmtpClient()) // протокол для передачи email
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true); // сервер исходящей почты, протокол шифрования
                await client.AuthenticateAsync("alexbas13@mail.ru", password); // логин-пароль аккаунта-отправителя
                await client.SendAsync(emailMessage); // отправка данных
                await client.DisconnectAsync(true);
            }
        }
    }
}
