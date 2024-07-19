using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Students;

[BindProperties]
public class AddStudentModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public AddStudentModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int MediaId;
        public string MediaName;
    }

    public IList<JoinResult> JoinResultIList;

    public SelectList StudentSelectList;

    public Student Student { get; set; }

    public async Task OnGetAsync()
    {

        //Set the message.
        MessageColor = "Green";
        Message = "Please add the information below and click add.";

        //Retrieve the rows for display.
        JoinResultIList = await (
            from m in _2023ACMSContext.Media
            orderby m.Media1
            select new JoinResult
            {
                MediaId = m.MediaId,
                MediaName = m.Media1
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {

        try
        {
            //Add the row to the table.
            _2023ACMSContext.Student.Add(Student);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Student.StudentFirstName + " " + Student.StudentLastName + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            //A database exception occurred while saving the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Student.StudentFirstName + " " + Student.StudentLastName +
                " was NOT added. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): "
                + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainStudents");

    }
}
