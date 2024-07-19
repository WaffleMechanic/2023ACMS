using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace _2023ACMS.Services;
public class EmailServiceMailKit : IEmailService
{

    private readonly IConfiguration IConfiguration;
    public EmailServiceMailKit(IConfiguration IC)
    {
        IConfiguration = IC;
    }

    public async Task SendEmail(string strToName, string strToAddress, string strSubject, string strBody)
    {
        //Build the email message.
        MimeMessage objMimeMessage = new MimeMessage();
        objMimeMessage.From.Add(new MailboxAddress("No Reply", "noreply@ACMS.com"));
        objMimeMessage.Bcc.Add(new MailboxAddress(strToName, strToAddress)); //strAddress needs to be a list.
        objMimeMessage.Subject = strSubject;
        objMimeMessage.Body = new TextPart(TextFormat.Html) { Text = strBody };

        //Send the email message.
        SmtpClient objSmtpClient = new SmtpClient();
        string strHost = IConfiguration.GetValue<string>("Email:Host");
        int intPort = IConfiguration.GetValue<int>("Email:Port");
        await objSmtpClient.ConnectAsync(strHost, intPort);
        await objSmtpClient.SendAsync(objMimeMessage);
        await objSmtpClient.DisconnectAsync(true);


    }
}
