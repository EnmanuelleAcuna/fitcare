using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using Microsoft.Identity.Client;

namespace fitcare.Models.Identity;

public class EmailSender : IEmailSender
{
	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		MailboxAddress correoOrigen = new("fitcare", "gimnasiosperformancecenter@outlook.com");

		MimeMessage message = new();
		message.From.Add(correoOrigen);
		message.To.Add(new MailboxAddress(email, email));
		message.Subject = subject;
		message.Body = new TextPart("html") { Text = htmlMessage };

		using SmtpClient client = new();
		await client.ConnectAsync("smtp.office365.com", 465, true);
		await client.AuthenticateAsync(correoOrigen.Address, "hyxcrrbzlhndphou");
		_ = await client.SendAsync(message);
		await client.DisconnectAsync(true);
	}
}
