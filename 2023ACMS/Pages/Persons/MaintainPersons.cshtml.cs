using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Persons;

public class MaintainPersonsModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public MaintainPersonsModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int PersonId;
        public string FirstName;
        public string LastName;
        public string EmailAddress;
        public string Status;
    }

    public IList<JoinResult> JoinResultIList;

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
            Message = "Welcome to the maintain persons page.";
        }

        //Retrieve the rows for display.
        JoinResultIList = await (
            from p in _2023ACMSContext.Person
            orderby p.LastName, p.FirstName
            select new JoinResult
            {
                PersonId = p.PersonId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                EmailAddress = p.EmailAddress,
                Status = p.Status
            })
            .AsNoTracking()
            .ToListAsync();
    }

}
