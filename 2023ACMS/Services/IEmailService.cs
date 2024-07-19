namespace _2023ACMS.Services;

public interface IEmailService
{
    Task SendEmail(string strToName, string strToAddress, string strSubject, string strBody);
}
