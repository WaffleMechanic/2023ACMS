using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Defaults;

[BindProperties]
public class ModifyDefaultModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public ModifyDefaultModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public Default Default { get; set; }

    public async Task<IActionResult> OnGetAsync(int intDefaultID)
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please modify the information below and click Modify.";

        //Attempt to retrieve the row from the table.
        Default = await _2023ACMSContext.Default
            .Where(p => p.DefaultId == intDefaultID)
            .FirstOrDefaultAsync();

        if (Default != null)
        {
            return Page();
        }
        else
        {
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected default was recently deleted by someone else.";

            return Redirect("MaintainDefaults");
        }
    }

    public async Task<IActionResult> OnPostModifyAsync()
    {
        try
        {
            //Modify the row in the table.
            _2023ACMSContext.Default.Update(Default);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "The default was successfully modified.";
        }
        catch (DbUpdateException objDbUdateException)
        {
            // A database exception occured while saving to the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The default was NOT modified. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                objDbUdateException.InnerException.Message;
        }
        return Redirect("MaintainDefaults");

    }

}
