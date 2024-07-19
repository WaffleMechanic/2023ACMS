using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Artworks
{
    [BindProperties]
    public class ModifyArtworkModel : PageModel
    {

        public string MessageColor;
        public string Message;

        private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

        public ModifyArtworkModel(_2023ACMS.Models._2023ACMSContext ACMSC)
        {
            _2023ACMSContext = ACMSC;
        }

        public class StudentJoinResult
        {
            public int StudentId;
            public string StudentLastName;
        }

        public class MediaJoinResult
        {
            public int MediaId;
            public string MediaName;
        }

        public class PersonJoinResult
        {
            public int PersonId;
            public string PersonFirstName;
        }

        public IList<StudentJoinResult> StudentJoinResultIList;
        public IList<MediaJoinResult> MediaJoinResultIList;
        public IList<PersonJoinResult> PersonJoinResultIList;

        public Artwork Artwork { get; set; }

        public async Task<IActionResult> OnGetAsync(int intArtworkID)
        {
            //Set the message.
            MessageColor = "Green";
            Message = "Please modify the information below and click Modify.";

            //Retrieve the artwork row for display.
            Artwork = await _2023ACMSContext.Artwork.FindAsync(intArtworkID);

            //Retrieve the student rows for display.
            StudentJoinResultIList = await (
                from s in _2023ACMSContext.Student
                select new StudentJoinResult
                {
                    StudentId = s.StudentId,
                    StudentLastName = s.StudentLastName
                })
                .AsNoTracking()
                .ToListAsync();

            //Retrieve the media row for display.
            MediaJoinResultIList = await (
                from m in _2023ACMSContext.Media
                select new MediaJoinResult
                {
                    MediaId = m.MediaId,
                    MediaName = m.Media1
                })
                .AsNoTracking()
                .ToListAsync();

            //Retrieve the person row for display.
            PersonJoinResultIList = await (
                from p in _2023ACMSContext.Person
                select new PersonJoinResult
                {
                    PersonId = p.PersonId,
                    PersonFirstName = p.FirstName
                })
                .AsNoTracking()
                .ToListAsync();

            return Page();

        }

        public async Task<IActionResult> OnPostModifyAsync()
        {
            try
            {
                //Modify the row in the table.
                _2023ACMSContext.Artwork.Update(Artwork);
                await _2023ACMSContext.SaveChangesAsync();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Artwork.Title + " was successfully modified.";
            }
            catch (DbUpdateException objDbUdateException)
            {
                //A database exception occured while saving to the database.
                //Set the message.
                TempData["MessageColor"] = "Red";
                TempData["Message"] = Artwork.Title +
                    " was NOT modified. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): " +
                    objDbUdateException.InnerException.Message;
            }
            return Redirect("MaintainArtworks");

        }

    }
}
