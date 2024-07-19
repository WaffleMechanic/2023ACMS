using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Students;

[BindProperties]
public class MaintainStudentsModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public MaintainStudentsModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int StudentId;
        public string StudentFirstName;
        public string StudentLastName;
        public string StudentEmail;
        public string ParentName;
        public string ParentEmail;
        public string TeacherName;
        public string TeacherEmail;
        public string MediaPreference;
        public bool PortfolioReview;
        public bool Attending;
    }

    public IList<JoinResult> StudentJoinResultIList;

    public string Search { get; set; }

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
            Message = "Welcome to the maintain students page.";
        }

        //Retrieve the rows for display.
        StudentJoinResultIList = await (
            from s in _2023ACMSContext.Student
            join m in _2023ACMSContext.Media on s.MediaPreferenceId equals m.MediaId
            orderby s.StudentFirstName, s.TeacherName
            select new JoinResult
            {
                StudentId = s.StudentId,
                StudentFirstName = s.StudentFirstName,
                StudentLastName = s.StudentLastName,
                StudentEmail = s.StudentEmail,
                ParentName = s.ParentName,
                ParentEmail = s.ParentEmail,
                TeacherName = s.TeacherName,
                TeacherEmail = s.TeacherEmail,
                MediaPreference = m.Media1,
                PortfolioReview = s.PortfolioReview,
                Attending = s.Attending
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task OnPostSearchAsync()
    {

        if (Search == null)
        {
            MessageColor = "Green";
            Message = "No search filters are in use.";

            //Retrieve the rows for display.
            StudentJoinResultIList = await (
                from s in _2023ACMSContext.Student
                join m in _2023ACMSContext.Media on s.MediaPreferenceId equals m.MediaId
                orderby s.StudentFirstName, s.TeacherName
                select new JoinResult
                {
                    StudentId = s.StudentId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    StudentEmail = s.StudentEmail,
                    ParentName = s.ParentName,
                    ParentEmail = s.ParentEmail,
                    TeacherName = s.TeacherName,
                    TeacherEmail = s.TeacherEmail,
                    MediaPreference = m.Media1,
                    PortfolioReview = s.PortfolioReview,
                    Attending = s.Attending
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
            StudentJoinResultIList = await (
                from s in _2023ACMSContext.Student
                join m in _2023ACMSContext.Media on s.MediaPreferenceId equals m.MediaId
                where (s.StudentFirstName.Contains(Search))
                    || (s.StudentLastName.Contains(Search))
                    || (s.StudentEmail.Contains(Search))
                    || (m.Media1.Contains(Search))
                    || (s.StudentLastName.Contains(Search))
                orderby s.StudentLastName, s.StudentFirstName, s.TeacherName
                select new JoinResult
                {
                    StudentId = s.StudentId,
                    StudentFirstName = s.StudentFirstName,
                    StudentLastName = s.StudentLastName,
                    StudentEmail = s.StudentEmail,
                    ParentName = s.ParentName,
                    ParentEmail = s.ParentEmail,
                    TeacherName = s.TeacherName,
                    TeacherEmail = s.TeacherEmail,
                    MediaPreference = m.Media1,
                    PortfolioReview = s.PortfolioReview,
                    Attending = s.Attending
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public async Task<IActionResult> OnPostClearSearchAsync()
    {
        return Redirect("MaintainStudents");
    }

}
