using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Artworks;

public class DeleteArtworkModel : PageModel
{

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;
    private readonly IWebHostEnvironment IWebHostEnvironment;

    public DeleteArtworkModel(_2023ACMS.Models._2023ACMSContext ACMSC, IWebHostEnvironment IWHE)
    {
        _2023ACMSContext = ACMSC;
        IWebHostEnvironment = IWHE;
    }

    private Artwork Artwork { get; set; }
    private string filePath = "~/images/Artwork/";

    public async Task<IActionResult> OnGetAsync(int intArtworkId)
    {
        //Look up the row in the table to see if it still exists.
        Artwork = await _2023ACMSContext.Artwork.FindAsync(intArtworkId);

        if (Artwork != null)
        {
            try
            {
                //Delete the row in the table.
                _2023ACMSContext.Artwork.Remove(Artwork);
                await _2023ACMSContext.SaveChangesAsync();

                string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images\\Artwork");
                //string strFileName = ;
                string strFilePath = Path.Combine(strImagesPath, Artwork.Filename);

                System.IO.File.Delete(strFilePath);

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Artwork.Title + " was successfully deleted.";
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
                    TempData["Message"] = Artwork.Title + " was NOT deleted because " +
                        "There is a delete restric in the database.";
                }
                else
                {
                    //A database exception occured while saving to the database.
                    //Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Artwork.Title +
                        " was NOT deleted. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
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
            TempData["Message"] = Artwork.Title + " was successfully deleted.";

        }
        return Redirect("MaintainArtworks");
    }
}
