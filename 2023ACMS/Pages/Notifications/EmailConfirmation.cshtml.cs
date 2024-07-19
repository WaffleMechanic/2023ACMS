using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2023ACMS.Pages.Notifications
{
    public class EmailConfirmationModel : PageModel
    {

        public string MessageColor;
        public string Message;

        public void OnGet()
        {
            if (TempData["MessageColor"] != null)
            {
                //Set the message.
                MessageColor = TempData["MessageColor"].ToString();
                Message = TempData["Message"].ToString();
            }
            else
            {
                //Set the message.
                MessageColor = "Green";
                Message = "The notification email has been sent. Check your inbox for the email.";
            }

        }
    }
}
