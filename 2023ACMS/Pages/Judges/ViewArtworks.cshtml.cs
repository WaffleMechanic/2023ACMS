using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Judges;

[BindProperties]
public class ViewArtworksModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public ViewArtworksModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int ArtworkId;
        public string Title;
        public string File;
        public string JudgeNotes;
        public bool Accept;
        public int StudentId; //student table
        public string StudentFirstName;
        public string StudentLastName;

    }

    public IList<JoinResult> JoinResultIList;

    public Artwork Artwork { get; set; }
    public string Search { get; set; }
    public string JudgedSearch { get; set; }

    public async Task OnGetAsync()
    {

        //Set the message.
        if (TempData["MessageColor"] != null)
        {
            MessageColor = TempData["MessageColor"].ToString();
            Message = TempData["Message"].ToString();
        }
        else
        {
            MessageColor = "Green";
            Message = "Welcome to the view artwork page.";
        }

        //Retrieve the rows for display.
        JoinResultIList = await (
            from a in _2023ACMSContext.Artwork
            join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
            join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
            orderby a.Title
            select new JoinResult
            {
                ArtworkId = a.ArtworkId,
                StudentFirstName = s.StudentFirstName,
                StudentLastName = s.StudentLastName,
                Title = a.Title,
                File = a.Filename,
                JudgeNotes = a.JudgeNotes,
                Accept = a.Accept
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task OnPostSearchAsync()
    {

        if (Search == null && JudgedSearch == "null")
        {
            //Set the message.
            MessageColor = "Green";
            Message = "No search filters are in use.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                orderby a.Title
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Title = a.Title,
                    File = a.Filename,
                    JudgeNotes = a.JudgeNotes,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
        else if (Search != null && JudgedSearch == "null")
        {

            //Set the message.
            MessageColor = "Green";
            Message = "You are filtering your search with the input below.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where (a.Title.Contains(Search))
                orderby a.Title
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Title = a.Title,
                    File = a.Filename,
                    JudgeNotes = a.JudgeNotes,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
        else if (JudgedSearch == "NotJudged")
        {

            //Set the message.
            MessageColor = "Green";
            Message = "You are filtering your search with the input below.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where (a.JudgeNotes.Contains("")) && (a.Judged == false)
                orderby a.Title
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Title = a.Title,
                    File = a.Filename,
                    JudgeNotes = a.JudgeNotes,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
        else
        {

            //Set the message.
            MessageColor = "Green";
            Message = "You are filtering your search with the input below.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where (a.Judged == true)
                orderby a.Title
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Title = a.Title,
                    File = a.Filename,
                    JudgeNotes = a.JudgeNotes,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }

    }

    public async Task<IActionResult> OnPostClearSearchAsync()
    {
        return Redirect("ViewArtworks");
    }

}
