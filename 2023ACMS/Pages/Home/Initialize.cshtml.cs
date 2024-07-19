using _2023ACMS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Okta.AspNetCore;

namespace _2023ACMS.Pages.Home;

public class InitializeModel : PageModel
{

    // IMPORTANT: The Okta.AspCore by Okta, Inc. NuGet package must
    // be installed in the project for Single Sign On to work.

    private readonly _2023ACMSContext _2023ACMSContext;
    public InitializeModel(_2023ACMSContext ACMSC)
    {
        _2023ACMSContext = ACMSC;
    }

    private Person Person;

    private string strEmailAddress;
    public string strStatus;

    public async Task<IActionResult> OnGetAsync()
    {

        //Okta: If the user has not been authenticated,
         //display the LogIn page. Otherwise, continue.
        if (!HttpContext.User.Identity.IsAuthenticated)
        {
            return Challenge(OktaDefaults.MvcAuthenticationScheme);
        }

        //Okta: Get the user's email address.
        foreach (Claim claim in ((ClaimsIdentity)User.Identity).Claims)
        {
            if (claim.Type.Equals("preferred_username"))
            {
                // Get the user's email address.
                strEmailAddress = claim.Value;
                break;
            }
        }

        //strEmailAddress = "james.shelton@franklincollege.edu";

        // Use the email address to look up the user in the application's database.
        Person = await _2023ACMSContext.Person
            .Where(p => p.EmailAddress == strEmailAddress)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (Person.Status == "A")
        {
            strStatus = "Admin";
        }
        else if (Person.Status == "J")
        {
            strStatus = "Judge";
        }

        if (Person != null)
        {
            // The user was found in the application's database.

            //Displays Admin or Judge instead of A or J in the header for clarity.
            //if (Person.Status == "A")
            //{
            //    strStatus = "Admin";
            //}
            //else if (Person.Status == "J")
            //{
            //    strStatus = "Judge";
            //}

            // Save the user's information.
            HttpContext.Session.SetString("EmailAddress", Person.EmailAddress);
            HttpContext.Session.SetString("Status", strStatus);
            // Authenticate the user.
            List<Claim> objClaimList = new List<Claim> { new Claim(ClaimTypes.Role, Person.Status) };
            ClaimsIdentity objClaimsIdentity = new ClaimsIdentity(objClaimList, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(objClaimsIdentity));

            // Set the message.
            //TempData["MessageColor"] = "Green";
            //TempData["Message"] = "Welcome to the Art Competition Management System.";

            // Redirect the user.
            return RedirectToAction("ViewWelcome", "Common");
        }
        else
        {
            // The user was not found in the application's database.
            // Redirect the user.
            return RedirectToAction("LogOut", "Common");
        }

    }

}
