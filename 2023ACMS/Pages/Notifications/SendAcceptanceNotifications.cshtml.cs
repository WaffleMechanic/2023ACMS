using _2023ACMS.Models;
using _2023ACMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages;

[BindProperties]
public class SendAcceptanceNotificationsModel : PageModel
{

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;
    private readonly IEmailService IEmailService;

    public SendAcceptanceNotificationsModel(_2023ACMS.Models._2023ACMSContext ACMSC, IEmailService IES)
    {
        _2023ACMSContext = ACMSC;
        IEmailService = IES;
    }

    public class JoinResult
    {
        public int ArtworkId;
        public string Title;
        public bool Judged;
        public int StudentId;
        public string StudentEmail;
    }

    public IList<JoinResult> JoinResultIList;

    public string MessageColor;
    public string Message;
    public string Introduction = "Greetings,";
    public string Body = "I am reaching out to congratulate you! You have been accepted into the art competition!";
    public string Salutation = "Regards,";
    public string Signature = "Franklin College Art Department";
    public string FullEmail;
    public List<string> Emails;
    public List<string> Titles;
    public string EmailString;

    public string EmailAddress { get; set; }
    public string EmailIntroduction { get; set; }
    public string EmailBody { get; set; }
    public string EmailSalutation { get; set; }
    public string EmailSignature { get; set; }
    public string Title { get; set; }

    public async Task OnGetAsync()
    {
        EmailIntroduction = Introduction;
        EmailBody = Body;
        EmailSalutation = Salutation;
        EmailSignature = Signature;

        //Retrieve the rows for display.
        JoinResultIList = await (
            from a in _2023ACMSContext.Artwork
            join s in _2023ACMSContext.Student
                on a.StudentId equals s.StudentId
            select new JoinResult
            {
                ArtworkId = a.ArtworkId,
                Judged = a.Judged,
                Title = a.Title,
                StudentId = s.StudentId,
                StudentEmail = s.StudentEmail
            })
            .AsNoTracking()
            .ToListAsync();

        if (TempData["MessageColor"] != null)
        {
            MessageColor = TempData["MessageColor"].ToString();
            Message = TempData["Message"].ToString();
        }
        else
        {
            MessageColor = "Green";
            Message = "Welcome to send notification page.";
        }
    }

    public async Task<RedirectResult> OnPostSendAcceptanceNotificationsAsync() //redirect result changed from pageresult
    {
        Emails = EmailAddress.Split(',').ToList();
        Titles = Title.Split(',').ToList();
        int i = 0;

        try
        {
            //You might want to add this. It currently does not display the message even when uncommented.
            //MessageColor = "Yellow";
            //Message = "Please wait. This may take a moment.";

            foreach (var item in Emails)
            {
                //Configure the email and send it.
                string strToName = ""; //This needs to be automated to send emails to each person that has been accepted into the art competition. The value was set to James Shelton
                string strToAddress = Emails[i];
                string Title = Titles[i];
                string strSubject = "Art Competition";

                i++;

                //Replace this string with a variable so the user can craft an email.
                string strBody = EmailIntroduction + "<br /><br />" + EmailBody + "<br /><br />" + "Artwork accepted: " + Title + "<br /><br />" + EmailSalutation + "<br /><br />" + EmailSignature;
                await IEmailService.SendEmail(strToName, strToAddress, strSubject, strBody);
            }

            //Set the message
            MessageColor = "Green";
            Message = "Notification email has been sent successfully.";
            return Redirect("EmailConfirmation");
        }
        catch
        {
            //Set the message
            MessageColor = "Red";
            Message = "An error occured. Please make sure you are using a proper email format.";
            return Redirect("SendAcceptanceNotifications");
        }
    }
}

