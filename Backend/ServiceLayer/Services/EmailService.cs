using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.ComponentModel.DataAnnotations;

public static class EmailService
{
	static string SMTP_USERNAME = Environment.GetEnvironmentVariable("SMTP_USERNAME");
	static string SMTP_PASSWORD = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
	static string SMTP_SERVER = Environment.GetEnvironmentVariable("SMTP_SERVER");
	static int SMTP_PORT = Int32.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));

	/// <summary>
	/// Composes an email using the supplied parameters. The email body will be in plaintext.
	/// </summary>
	/// <param name="senderName">The sender's name</param>
	/// <param name="senderEmail">The sender's email</param>
	/// <param name="recipientName">The recipient's name</param>
	/// <param name="recipientEmail">The recipient's email</param>
	/// <param name="emailSubject">The subject of the email</param>
	/// <param name="emailBodyPlainText">The body of the email, in plaintext form</param>
	/// <returns>Returns an email of type MimeMessage</returns>
	public static MimeMessage CreateEmailPlainBody(string senderName, string senderEmailAddress, string recipientName, string recipientEmailAddress, string emailSubject, string emailBodyPlainText)
    {
		var messageToSend = new MimeMessage();
		messageToSend.From.Add(new MailboxAddress(senderName, senderEmailAddress));
		messageToSend.To.Add(new MailboxAddress(recipientName, recipientEmailAddress));
		messageToSend.Subject = emailSubject;
		messageToSend.Body = new TextPart("plain")
		{
			Text = emailBodyPlainText
		};
		return messageToSend;
	}

	/// <summary>
	/// Composes an email using the supplied parameters. The email body will be in HTML format.
	/// </summary>
	/// <param name="senderName">The sender's name</param>
	/// <param name="senderEmail">The sender's email</param>
	/// <param name="recipientName">The recipient's name</param>
	/// <param name="recipientEmail">The recipient's email</param>
	/// <param name="emailSubject">The subject of the email</param>
	/// <param name="emailBodyHTMLText">The body of the email, formatted with HTML tags</param>
	/// <returns>Returns an email of type MimeMessage</returns>
	public static MimeMessage CreateEmailHTMLBody(string senderName, string senderEmail, string recipientName, string recipientEmail, string emailSubject, string emailBodyHTMLText)
	{
		var messageToSend = new MimeMessage();
		messageToSend.From.Add(new MailboxAddress(senderName, senderEmail));
		messageToSend.To.Add(new MailboxAddress(recipientName, recipientEmail));
		messageToSend.Subject = emailSubject;
		messageToSend.Body = new TextPart("html")
		{
			Text = emailBodyHTMLText
		};
		return messageToSend;
	}

	/// <summary>
	/// Sends an email using the message as a parameter
	/// </summary>
	/// <param name="messageToSend">Expects message to be of type MimeMessage</param>
	/// <returns>Returns boolean value based on whether the email is able to be sent successfully</returns>
	public static bool SendEmail(MimeMessage messageToSend)
	{
		try
		{
			using (var emailClient = new SmtpClient())
			{
				emailClient.Connect(SMTP_SERVER, SMTP_PORT);
				emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
				emailClient.Authenticate(SMTP_USERNAME, SMTP_PASSWORD);
				emailClient.Send(messageToSend);
				emailClient.Disconnect(true);
			}
			return true;
		}
		catch
		{
			//TODO: add logging code
			return false;
		}
	}
	public static bool IsValidEmailAddress(this string address) => 
		new EmailAddressAttribute().IsValid(address ?? throw new ArgumentNullException());
}
