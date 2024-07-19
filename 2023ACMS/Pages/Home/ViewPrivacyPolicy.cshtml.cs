using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2023ACMS.Pages.Home;

public class PrivacyPolicyModel : PageModel
{

    public string MessageColor;
    public string Message;

    public void OnGet()
    {
        //Set the message
        MessageColor = "Green";
        Message = "Welcome to the privacy page.";
    }

}