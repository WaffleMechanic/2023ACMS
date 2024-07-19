using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Persons;

public class DeletePersonModel : PageModel
{

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public DeletePersonModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    private Person Person { get; set; }

    public async Task<IActionResult> OnGetAsync(int intPersonId)
    {
        //Look up the row in the table to see if it still exists.
        Person = await _2023ACMSContext.Person.FindAsync(intPersonId);

        if (Person != null)
        {
            try
            {
                //Delete the row in the table.
                _2023ACMSContext.Person.Remove(Person);
                await _2023ACMSContext.SaveChangesAsync();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Person.FirstName + " " + Person.LastName + " was successfully deleted.";
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
                    TempData["Message"] = Person.FirstName + " " + Person.LastName + " was NOT deleted because " +
                        "he/she has judged one or more artworks. To delete this person, you need to delete the artworks they have judged.";
                }
                else
                {
                    //A database exception occured while saving to the database.
                    //Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Person.FirstName + " " + Person.LastName +
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
            TempData["Message"] = Person.FirstName + " " + Person.LastName + " was successfully deleted.";

        }
        return Redirect("MaintainPersons");
    }
}
