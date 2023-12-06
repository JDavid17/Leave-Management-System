using System.Net;
using LeaveManagement.Application.Contracts.Email;
using LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace LeaveManagement.Infrastructure.EmailService;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmail(EmailMessage emailMessage)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);
        var to = new EmailAddress(emailMessage.To);
        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var message =
            MailHelper.CreateSingleEmail(from, to, emailMessage.Subject, emailMessage.Body, emailMessage.Body);
        var response = await client.SendEmailAsync(message);

        return response.IsSuccessStatusCode;
    }
}