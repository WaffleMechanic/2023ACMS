using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2023ACMS.Pages.ArtworkSubmissions
{
    public class ThankYouModel : PageModel
    {

        private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;

        public ThankYouModel(_2023ACMS.Models._2023ACMSContext ACMSC, IWebHostEnvironment IWHE)
        {
            _2023ACMSContext = ACMSC;
        }

        public string MessageColor;
        public string Message;
        public bool DefaultStatus;

        public Default Default { get; set; }

        public async Task OnGetAsync()
        {

            Default = await _2023ACMSContext.Default.FindAsync(1);

            if (Default.AllowSubmissions == true)
            {
                if (TempData["MessageColor"] != null)
                {
                    //Set the message.
                    MessageColor = TempData["MessageColor"].ToString();
                    Message = TempData["Message"].ToString();
                }
                else
                {
                    //Set the message.
                    MessageColor = "Green";
                    Message = "Your artwork was successfully submitted! Thank you for your submission!";
                }
            }
            else
            {
                //Set the message.
                MessageColor = "Red";
                Message = "We are no longer accepting submissions. Please contact Randi Frye if you would like to submit your artwork. rfrye@FranklinCollege.edu";
            }



        }
    }
}
