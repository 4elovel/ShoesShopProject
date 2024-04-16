using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
namespace ShoesShopProject.Services;

public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("ShoesShop", "shoes.shop.project312@gmail.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlMessage };
        using (var client = new SmtpClient())
        {
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("shoes.shop.project312@gmail.com", "koyx fwvt diyx dwwk");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
