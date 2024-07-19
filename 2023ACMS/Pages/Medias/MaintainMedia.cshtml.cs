using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Medias;

public class MaintainMediaModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

    public MaintainMediaModel(_2023ACMS.Models._2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    public class JoinResult
    {
        public int MediaId;
        public string Media1;
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
            Message = "Welcome to the maintain media page.";
        }

        //Retrieve the rows for display.
        JoinResultIList = await (
            from m in _2023ACMSContext.Media
            orderby m.Media1
            select new JoinResult
            {
                MediaId = m.MediaId,
                Media1 = m.Media1
            })
            .AsNoTracking()
            .ToListAsync();
    }

}
