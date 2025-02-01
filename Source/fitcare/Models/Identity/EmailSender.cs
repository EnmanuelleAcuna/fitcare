using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace fitcare.Models.Identity;

public class EmailSender : IEmailSender
{
	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		try
		{
			var smtpClient = new SmtpClient("smtp.gmail.com", 587)
			{
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("gimnasiosperformancecenter@gmail.com", "yccb sxlf mhje tmbu")
			};

			await smtpClient.SendMailAsync(
				new MailMessage("gimnasiosperformancecenter@gmail.com", email, subject, htmlMessage)
				{
					IsBodyHtml = true
				}
			);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}
