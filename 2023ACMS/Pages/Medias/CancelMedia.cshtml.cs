using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2023ACMS.Pages.Medias;

public class CancelMediaModel : PageModel
{
    public RedirectResult OnGet()
    {
        //Set the message.
        TempData["MessageColor"] = "Red";
        TempData["Message"] = "The operation was canceled. No data was affected.";

        return Redirect("MaintainMedia");
    }
}
