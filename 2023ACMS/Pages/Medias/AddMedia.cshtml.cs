using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Medias;

[BindProperties]
public class AddMediaModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public AddMediaModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public SelectList MediaSelectList;

    public Media Media { get; set; }

    public void OnGet()
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please add the information below and click add.";
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            //Add the row to the table.
            _2023ACMSContext.Media.Add(Media);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Media.Media1 + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            //A database exception occurred while saving the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Media.Media1 + " was NOT added. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): "
                + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainMedia");

    }
}
