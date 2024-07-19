using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Students
{
    [BindProperties]
    public class ModifyStudentModel : PageModel
    {

        public string MessageColor;
        public string Message;

        private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

        public ModifyStudentModel(_2023ACMS.Models._2023ACMSContext ACMSC)
        {
            _2023ACMSContext = ACMSC;
        }

        public class JoinResult
        {
            public int MediaId;
            public string MediaName;
        }

        public IList<JoinResult> JoinResultIList;

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int intStudentID)
        {
            //Set the message.
            MessageColor = "Green";
            Message = "Please modify the information below and click Modify.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from m in _2023ACMSContext.Media
                select new JoinResult
                {
                    MediaId = m.MediaId,
                    MediaName = m.Media1
                })
                .AsNoTracking()
                .ToListAsync();

            //Attempt to retrieve the row from the table.
            Student = await _2023ACMSContext.Student.FindAsync(intStudentID);

            return Page();

        }

        public async Task<IActionResult> OnPostModifyAsync()
        {
            try
            {
                //Modify the row in the table.
                _2023ACMSContext.Student.Update(Student);
                await _2023ACMSContext.SaveChangesAsync();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Student.StudentFirstName + " " + Student.StudentLastName + " was successfully modified.";
            }
            catch (DbUpdateException objDbUdateException)
            {
                // A database exception occured while saving to the database.
                //Set the message.
                TempData["MessageColor"] = "Red";
                TempData["Message"] = Student.StudentFirstName + " " + Student.StudentLastName +
                    " was NOT modified. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                    objDbUdateException.InnerException.Message;
            }
            return Redirect("MaintainStudents");

        }

    }
}
