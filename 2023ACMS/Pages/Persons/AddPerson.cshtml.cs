using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Persons;

[BindProperties]
public class AddPersonModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public AddPersonModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public SelectList PersonSelectList;

    public Person Person { get; set; }

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
            _2023ACMSContext.Person.Add(Person);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Person.FirstName + " " + Person.LastName + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            //A database exception occurred while saving the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Person.FirstName + " " + Person.LastName +
                " was NOT added. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): "
                + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainPersons");

    }
}
