using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2023ACMS.Pages.Common;

public class ViewWelcomeModel : PageModel
{
    public string MessageColor;
    public string Message;

    public void OnGet()
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Welcome to the Art Competition Management System!";
    }

}
