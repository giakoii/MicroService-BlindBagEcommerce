using System.Net.Mail;
using AuthService.Controllers;
using AuthService.Models;
using AuthService.Models.Helpers;
using Microsoft.EntityFrameworkCore;
using OpenIdConnect.Utils.Consts;

namespace AuthService.Logics.Commons;

/// <summary>
/// Send mail config.
/// </summary>
public static class SendMailLogic
{
    /// <summary>
    ///  Send mail
    /// </summary>
    /// <param name="mailAddress"></param>
    /// <param name="title"></param>
    /// <param name="body"></param>
    private static async Task SendAsync(string mailAddress, string title, string body, AppDbContext context)
    {
        var configs = await context.SystemConfigs.AsNoTracking().ToListAsync();
        // The SMTP server is obtained from the system configuration (SYSTEM_CONFIG).
        var mailSmtp = configs.Find(c => c.Id == OpenIdConnect.Utils.Consts.SystemConfig.MailSmtp)?.Value;
        // The sender of the email is obtained from the system configuration (SYSTEM_CONFIG).
        var mailFrom = configs.Find(c => c.Id == OpenIdConnect.Utils.Consts.SystemConfig.MailFrom)?.Value;
        // The BCC email sender is obtained from the system configuration (SYSTEM_CONFIG).
        var mailToBcc = configs.Find(c => c.Id == OpenIdConnect.Utils.Consts.SystemConfig.MailToBcc)?.Value;
        // The password for the email is obtained from the system configuration (SYSTEM_CONFIG).
        var mailPassword = configs.Find(c => c.Id == OpenIdConnect.Utils.Consts.SystemConfig.MailPassword)?.Value;
        
        var client = new SmtpClient
        {
            Host = mailSmtp,
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(mailFrom, mailPassword)
        };

        // Generate a message instance and set parameters.
        using (var message = new MailMessage())
        {
            message.From = new MailAddress(mailFrom);
            message.To.Add(mailAddress);
            message.Subject = title;
            message.Body = body;
            message.Bcc.Add(mailToBcc);
            message.IsBodyHtml = true;
            await client.SendMailAsync(message);
        }
    }

    /// <summary>
    /// Send mail
    /// </summary>
    /// <param name="emailTemplate"></param>
    /// <param name="mailAddress"></param>
    /// <param name="context"></param>
    /// <param name="detailErrorList"></param>
    /// <returns></returns>
    public static async Task<bool> SendMail(EmailTemplate emailTemplate, string mailAddress, AppDbContext context, List<DetailError> detailErrorList)
    {
        if (string.IsNullOrEmpty(emailTemplate.Title))
        {
            detailErrorList.Add(new DetailError()
            {
                field = emailTemplate.Title,
                MessageId = "E00000",
                ErrorMessage = "Title is empty"
            });
        }
        if (string.IsNullOrEmpty(emailTemplate.Body))
        {
            detailErrorList.Add(new DetailError()
            {
                field = emailTemplate.Body,
                MessageId = "E00000",
                ErrorMessage = "Body is empty"
            });
        }
        if (string.IsNullOrEmpty(mailAddress))
        {
            detailErrorList.Add(new DetailError()
            {
                field = mailAddress,
                MessageId = "E00000",
                ErrorMessage = "Mail address is empty"
            });
        }

        try
        {
            // Send mail
            await SendAsync(mailAddress, emailTemplate.Title, emailTemplate.Body, context);
            return true;
        }
        catch (Exception e)
        {
            var detailError = new DetailError();
            detailError.SetMessage(MessageId.E99999);
            detailErrorList?.Add(detailError);
            return false;
        }
    }
}