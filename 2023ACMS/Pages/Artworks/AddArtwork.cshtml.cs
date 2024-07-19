using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages.Artworks;

[BindProperties]
public class AddArtworkModel : PageModel
{

    public string MessageColor;
    public string Message;
    public string FilePath;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;
    private readonly IWebHostEnvironment IWebHostEnvironment;

    public AddArtworkModel(_2023ACMS.Models._2023ACMSContext ACMSC, IWebHostEnvironment IWHE)
    {
        _2023ACMSContext = ACMSC;
        IWebHostEnvironment = IWHE;
    }

    public class JoinResult
    {
        public int StudentId;
        public string StudentFirstName;
        public string StudentLastName;
        public int MediaId;
        public string MediaName;
        public int PersonId;
        public string PersonFirstName;
    }

    public IFormFile ArtworkImage { get; set; }

    public IList<JoinResult> JoinResultIList;
    public IList<JoinResult> MediaJoinResultIList;
    public IList<JoinResult> PersonJoinResultIList;

    public SelectList ArtworkSelectList;

    public Artwork Artwork { get; set; }
    public Media Media { get; set; }
    public Person Person { get; set; }

    public async Task OnGetAsync()
    {
        //Set the message.
        MessageColor = "Green";
        Message = "Please add the information below and click add.";

        //Retrieve the rows for display.
        JoinResultIList = await (
            from s in _2023ACMSContext.Student
            orderby s.StudentLastName, s.StudentFirstName
            select new JoinResult
            {
                StudentId = s.StudentId,
                StudentFirstName = s.StudentFirstName,
                StudentLastName = s.StudentLastName
            })
            .AsNoTracking()
            .ToListAsync();

        //Retrieve the rows for display.
        MediaJoinResultIList = await (
            from m in _2023ACMSContext.Media
            orderby m.Media1
            select new JoinResult
            {
                MediaId = m.MediaId,
                MediaName = m.Media1
            })
            .AsNoTracking()
            .ToListAsync();

        PersonJoinResultIList = await (
            from p in _2023ACMSContext.Person
            select new JoinResult
            {
                PersonId = p.PersonId,
                PersonFirstName = p.FirstName
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {

        try
        {
            //This value can not be null.
            Artwork.Judged = false;
            Artwork.Accept = false;
            Artwork.Filename = ArtworkImage.FileName;

            //Add the row to the table.
            _2023ACMSContext.Artwork.Add(Artwork);
            await _2023ACMSContext.SaveChangesAsync();
            CreateImage();

            //Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Artwork.Title + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            //A database exception occurred while saving the database.
            //Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Artwork.Title +
                " was NOT added. Please report this message to Robert Beasley (rbeasley@franklincollege.edu): "
                + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainArtworks");

    }

    //Creates the image.
    protected async void CreateImage()
    {
        if (ArtworkImage != null)

        {
            // Upload the file.
            string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images\\Artwork");
            string strFileName = Path.GetFileName(ArtworkImage.FileName);
            string strFilePath = Path.Combine(strImagesPath, strFileName);

            FileStream objFileStream = new FileStream(strFilePath, FileMode.Create);

            ArtworkImage.CopyTo(objFileStream);
            objFileStream.Close();

            // Set the message.
            MessageColor = "Green";
            Message = "You have uploaded the image successfully!";

        }
        else
        {
            // Set the message.
            MessageColor = "Red";
            Message = "Please select an image to upload and try again.";
        }
    }
}
