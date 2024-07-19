using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Persons;

[BindProperties]
public class ModifyPersonModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public ModifyPersonModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public Person Person { get; set; }

    public async Task<IActionResult> OnGetAsync(int intPersonID)
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please modify the information below and click Modify.";

        //Attempt to retrieve the row from the table.
        Person = await _2023ACMSContext.Person.FindAsync(intPersonID);

        return Page();

        //I think we can delete this.
        //if (Person != null)
        //{
        //    return Page();
        //}
        //else
        //{
        //    //Set the message.
        //    TempData["MessageColor"] = "Red";
        //    TempData["Message"] = "The selected person was recently deleted by someone else.";

        //    return Redirect("MaintainPersons");
        //}
    }

    public async Task<IActionResult> OnPostModifyAsync()
    {
        try
        {
            //Modify the row in the table.
            _2023ACMSContext.Person.Update(Person);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Person.FirstName + " " + Person.LastName + " was successfully modified.";
        }
        catch (DbUpdateException objDbUdateException)
        {
            // A database exception occured while saving to the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Person.FirstName + " " + Person.LastName +
                " was NOT modified. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                objDbUdateException.InnerException.Message;
        }
        return Redirect("MaintainPersons");
    }

}
