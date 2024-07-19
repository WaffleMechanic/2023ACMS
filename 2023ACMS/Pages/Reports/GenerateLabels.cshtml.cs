using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Reports;

public class GenerateLabelsModel : PageModel
{
    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public GenerateLabelsModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int ArtworkId;
        public string FirstName;
        public string LastName;
        public string Title;
        public string Media;
        public bool Attending;
        public bool Accepted;
    }

    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        //Set the message
        MessageColor = "Green";
        Message = "Welcome to the generate labels page.";

        //Retrieve the rows for display.
        JoinResultIList = await (
            from a in _2023ACMSContext.Artwork
            join s in _2023ACMSContext.Student
                on a.StudentId equals s.StudentId
            join m in _2023ACMSContext.Media
                on a.MediaId equals m.MediaId
            select new JoinResult
            {
                ArtworkId = a.ArtworkId,
                FirstName = s.StudentFirstName,
                LastName = s.StudentLastName,
                Title = a.Title,
                Media = m.Media1,
                Attending = s.Attending,
                Accepted = a.Accept
            })
            .AsNoTracking()
            .ToListAsync();

    }
}
