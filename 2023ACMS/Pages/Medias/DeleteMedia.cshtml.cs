using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Medias;

public class DeleteMediaModel : PageModel
{

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public DeleteMediaModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    private Media Media { get; set; }

    public async Task<IActionResult> OnGetAsync(int intMediaId)
    {
        //Look up the row in the table to see if it stille exists.
        Media = await _2023ACMSContext.Media.FindAsync(intMediaId);

        if (Media != null)
        {
            try
            {
                //Delete the row in the table.
                _2023ACMSContext.Media.Remove(Media);
                await _2023ACMSContext.SaveChangesAsync();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Media.Media1 + " was successfully deleted.";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                //A database exception occured.
                SqlException objSqlException = objDbUpdateException.InnerException as SqlException;
                if (objSqlException.Number == 547)
                {
                    //A foreign key constraint database exception occured.
                    //Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Media.Media1 + " was NOT deleted because " +
                        "it is being used with a person or an artwork. To delete this media, you need to delete the artwork and or person that it is associated with.";
                }
                else
                {
                    //A database exception occured while saving to the database.
                    //Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Media.Media1 + " was NOT deleted. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                        objDbUpdateException.InnerException.Message; ;
                }
            }
        }
        else
        {
            //Set the message.
            //Even though someone else deleted the item first,
            //we will still inform the user that the item was deleted successfully.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Media.Media1 + " was successfully deleted.";

        }
        return Redirect("MaintainMedia");
    }
}
