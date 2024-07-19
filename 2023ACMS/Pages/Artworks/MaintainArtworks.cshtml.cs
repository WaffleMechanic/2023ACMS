using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Artworks;

[BindProperties]
public class MaintainArtworksModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public MaintainArtworksModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int ArtworkId;
        public string Title;
        public string File;
        public bool Accept;
        public int StudentId; //student table
        public string StudentFirstName;
        public string StudentLastName;
        public int MediaId; //media table
        public string Media1;
    }

    public IList<JoinResult> JoinResultIList;

    public Artwork Artwork { get; set; }
    public string Search { get; set; }
    public string AcceptanceSearch { get; set; }

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
            Message = "Welcome to the maintain artworks page.";
        }

        //Retrieve the rows for display.
        JoinResultIList = await (
            from a in _2023ACMSContext.Artwork
            join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
            join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
            orderby s.StudentLastName, s.StudentFirstName, a.Title
            select new JoinResult
            {
                ArtworkId = a.ArtworkId,
                StudentFirstName = s.StudentFirstName,
                StudentLastName = s.StudentLastName,
                Media1 = m.Media1,
                Title = a.Title,
                File = a.Filename,
                Accept = a.Accept
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task OnPostSearchAsync()
    {

        if (AcceptanceSearch == "All")
        {
            MessageColor = "Green";
            Message = "No search filters are in use.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where (a.Title.Contains(Search)) || (s.StudentFirstName.Contains(Search)) || (s.StudentLastName.Contains(Search))
                orderby a.Title
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Media1 = m.Media1,
                    Title = a.Title,
                    File = a.Filename,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
        else if (AcceptanceSearch == "Not Accepted") // Not sure acceptance search can be null.
        {
            //Set the message.
            MessageColor = "Green";
            Message = "You are filtering your search with the items below.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where ((a.Title.Contains(Search)) || (s.StudentFirstName.Contains(Search)) || (s.StudentLastName.Contains(Search))) && (a.Accept == false) //Do not need this now. You will need a new joinresultilist. if acceptance search is clicked then you will use the acceptance search joinresult.
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Media1 = m.Media1,
                    Title = a.Title,
                    File = a.Filename,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
        else
        {
            //Set the message.
            MessageColor = "Green";
            Message = "You are filtering your search with the items below.";

            //Retrieve the rows for display.
            JoinResultIList = await (
                from a in _2023ACMSContext.Artwork
                join m in _2023ACMSContext.Media on a.MediaId equals m.MediaId
                join s in _2023ACMSContext.Student on a.StudentId equals s.StudentId
                where ((a.Title.Contains(Search)) || (s.StudentFirstName.Contains(Search)) || (s.StudentLastName.Contains(Search))) && (a.Accept == true)  //Do not need this now. You will need a new joinresultilist. if acceptance search is clicked then you will use the acceptance search joinresult.
                select new JoinResult
                {
                    ArtworkId = a.ArtworkId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    Media1 = m.Media1,
                    Title = a.Title,
                    File = a.Filename,
                    Accept = a.Accept
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public async Task<IActionResult> OnPostClearSearchAsync()
    {
        return Redirect("MaintainArtworks");
    }

    protected void DeleteFile()
    {
        //Delete the file.
        Message = "";

    }

}
