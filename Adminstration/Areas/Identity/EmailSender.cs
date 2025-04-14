using Microsoft.AspNetCore.Identity.UI.Services;

namespace Adminstration.Areas.Identity;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        // You can log the email or just do nothing for now
        Console.WriteLine($"Email to: {email}, Subject: {subject}, Message: {message}");
        return Task.CompletedTask;
    }
}