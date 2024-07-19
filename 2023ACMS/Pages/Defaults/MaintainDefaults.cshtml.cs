using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Defaults;

public class MaintainDefaultsModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public MaintainDefaultsModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int DefaultId;
        public string AllowSubmissions;
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
            Message = "Welcome to the maintain defaults page.";
        }

        //Retrieve the rows for display.
        JoinResultIList = await (
            from d in _2023ACMSContext.Default
            select new JoinResult
            {
                DefaultId = d.DefaultId,
                AllowSubmissions = d.AllowSubmissions.ToString()
            })
            .AsNoTracking()
            .ToListAsync();
    }

}
