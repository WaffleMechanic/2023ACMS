using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Medias;

[BindProperties]
public class ModifyMediaModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public ModifyMediaModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public Media Media { get; set; }

    public async Task<IActionResult> OnGetAsync(int intMediaID)
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please modify the information below and click Modify.";

        //Attempt to retrieve the row from the table.
        Media = await _2023ACMSContext.Media.FindAsync(intMediaID);

        return Page();

        //Media = await _2023ACMSContext.Media
        //    .Where(p => p.MediaId == intMediaID)
        //    .FirstOrDefaultAsync();

        //if (Media != null)
        //{
        //    return Page();
        //}
        //else
        //{
        //    //Set the message.
        //    TempData["MessageColor"] = "Red";
        //    TempData["Message"] = "The selected media was recently deleted by someone else.";

        //    return Redirect("MaintainMedia");
        //}
    }

    public async Task<IActionResult> OnPostModifyAsync()
    {
        try
        {
            //Modify the row in the table.
            _2023ACMSContext.Media.Update(Media);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Media.Media1 + " was successfully modified.";
        }
        catch (DbUpdateException objDbUdateException)
        {
            // A database exception occured while saving to the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Media.Media1 + " was NOT modified. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                objDbUdateException.InnerException.Message;
        }
        return Redirect("MaintainMedia");

    }

}
