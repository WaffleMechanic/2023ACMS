using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Judges;

[BindProperties]
public class JudgeArtworkModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public JudgeArtworkModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int StudentId;
        public string StudentName;
        public int MediaId;
        public string MediaName;
        public int PersonId;
        public string PersonFirstName;
    }

    public IList<JoinResult> JoinResultIList;
    public IList<JoinResult> MediaJoinResultIList;
    public IList<JoinResult> PersonJoinResultIList;

    public Artwork Artwork { get; set; }
    public Media Media { get; set; }

    public async Task<IActionResult> OnGetAsync(int intArtworkID)
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please enter your notes and whether or not they have been accepted into the art show and then click submit.";

        //Retrieve the rows for display.
        JoinResultIList = await (
            from s in _2023ACMSContext.Student
            select new JoinResult
            {
                StudentId = s.StudentId,
                StudentName = s.StudentFirstName
            })
            .AsNoTracking()
            .ToListAsync();

        //Retrieve the rows for display.
        MediaJoinResultIList = await (
            from m in _2023ACMSContext.Media
            select new JoinResult
            {
                MediaId = m.MediaId,
                MediaName = m.Media1
            })
            .AsNoTracking()
            .ToListAsync();

        PersonJoinResultIList = await (
            from p in _2023ACMSContext.Person
            select new JoinResult
            {
                PersonId = p.PersonId,
                PersonFirstName = p.FirstName
            })
            .AsNoTracking()
            .ToListAsync();

        //Attempt to retrieve the row from the table.
        Artwork = await _2023ACMSContext.Artwork.FindAsync(intArtworkID);
        Media = await _2023ACMSContext.Media.FindAsync(Artwork.MediaId);

        return Page();
    }

    public async Task<IActionResult> OnPostJudgeAsync()
    {
        try
        {

            //Modify the row in the table.
            _2023ACMSContext.Artwork.Update(Artwork);
            await _2023ACMSContext.SaveChangesAsync();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Artwork.Title + " was successfully judged.";
        }
        catch (DbUpdateException objDbUdateException)
        {
            // A database exception occured while saving to the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Artwork.Title +
                " was NOT judged. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                objDbUdateException.InnerException.Message;
        }
        return Redirect("ViewArtworks");

    }

}
