using _2023ACMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _2023ACMS.Pages;

[BindProperties]
public class ArtworkSubmissionFormModel : PageModel
{

    public string MessageColor;
    public string Message;

    private readonly _2023ACMS.Models._2023ACMSContext _2023ACMSContext;
    private readonly IWebHostEnvironment IWebHostEnvironment;

    public ArtworkSubmissionFormModel(_2023ACMS.Models._2023ACMSContext ACMSC, IWebHostEnvironment IWHE)
    {
        _2023ACMSContext = ACMSC;
        IWebHostEnvironment = IWHE;
    }

    public class JoinResult
    {
        public int MediaId;
        public string MediaName;
    }

    public IList<JoinResult> JoinResultIList;

    public SelectList StudentSelectList;

    public IFormFile ArtworkImage { get; set; }
    public IFormFile ArtworkImage02 { get; set; }
    public IFormFile ArtworkImage03 { get; set; }

    public Student Student { get; set; }
    public Artwork Artwork { get; set; }
    public Artwork Artwork02 { get; set; }
    public Artwork Artwork03 { get; set; }
    public Media Media { get; set; }
    public Default Default { get; set; }

    public async Task<RedirectResult> OnGetAsync()
    {
        Default = await _2023ACMSContext.Default.FindAsync(1);

        // Determines if the form is still available.
        if (Default.AllowSubmissions == false)
        {
            return Redirect("ThankYou");
        }

        //Set the message.
        MessageColor = "Green";
        Message = "Please add the information below and click add.";

        //Retrieve the rows for display.
        JoinResultIList = await (
            from m in _2023ACMSContext.Media
            select new JoinResult
            {
                MediaId = m.MediaId,
                MediaName = m.Media1
            })
            .AsNoTracking()
            .ToListAsync();

        return null;
    }

    public async Task<IActionResult> OnPostSubmitAsync()
    {

        if (Artwork02.Title != null && Artwork03.Title != null) //The student has entered 3 artworks.
        {
            //This will add Artworks 1, 2, and 3.
            try
            {

                Artwork.Filename = ArtworkImage.FileName;
                Artwork02.Filename = ArtworkImage02.FileName;
                Artwork03.Filename = ArtworkImage03.FileName;

                //Add the student to the student table.
                _2023ACMSContext.Student.Add(Student);
                await _2023ACMSContext.SaveChangesAsync();

                //Setting the values of non nullable foreign keys.
                Artwork.StudentId = Student.StudentId;
                Artwork02.StudentId = Student.StudentId;
                Artwork03.StudentId = Student.StudentId;

                //Add the artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork);
                await _2023ACMSContext.SaveChangesAsync();

                //Add the second artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork02);
                await _2023ACMSContext.SaveChangesAsync();

                //Add the third artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork03);
                await _2023ACMSContext.SaveChangesAsync();

                CreateImage();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = "Your artwork was successfully added!";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                //A database exception occurred while saving the database.
                //Set the message.
                TempData["MessageColor"] = "Red";
                TempData["Message"] = "Your artwork was not added was NOT added. Please contact Randi Frye. rfrye@FranklinCollege.edu  " //The user should not get this message. Test rigorously.
                    + objDbUpdateException.InnerException.Message;
            }
            return Redirect("ThankYou");
        }
        else if (Artwork.Title != null && Artwork02.Title != null && Artwork03.Title == null) //The user has entered 2 artworks.
        {
            //This will add artwork 1 and 2.
            try
            {

                Artwork.Filename = ArtworkImage.FileName;
                Artwork02.Filename = ArtworkImage02.FileName;

                //Add the student to the student table.
                _2023ACMSContext.Student.Add(Student);
                await _2023ACMSContext.SaveChangesAsync();

                //Setting the values of non nullable foreign keys.
                Artwork.StudentId = Student.StudentId;
                Artwork02.StudentId = Student.StudentId;

                //Add the artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork);
                await _2023ACMSContext.SaveChangesAsync();

                //Add the second artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork02);
                await _2023ACMSContext.SaveChangesAsync();

                CreateImage();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = "Your artwork was successfully added!";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                //A database exception occurred while saving the database.
                //Set the message.
                TempData["MessageColor"] = "Red";
                TempData["Message"] = "Your artwork was not added was NOT added. Please contact Randi Frye. rfrye@FranklinCollege.edu " //The user should not get this message. Test rigorously.
                    + objDbUpdateException.InnerException.Message;
            }
            return Redirect("ThankYou");
        }
        else //The user has entered 1 artwork.
        {
            try
            {

                Artwork.Filename = ArtworkImage.FileName;

                //Add the student to the student table.
                _2023ACMSContext.Student.Add(Student);
                await _2023ACMSContext.SaveChangesAsync();

                //Setting the values of non nullable foreign keys.
                Artwork.StudentId = Student.StudentId;

                //Add the artwork to the artwork table.
                _2023ACMSContext.Artwork.Add(Artwork);
                await _2023ACMSContext.SaveChangesAsync();
                CreateImage();

                //Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = "Your artwork was successfully added!";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                //A database exception occurred while saving the database.
                //Set the message.
                TempData["MessageColor"] = "Red";
                TempData["Message"] = "Your artwork was not added was NOT added. Please contact Randi Frye. rfrye@FranklinCollege.edu" //The user should not get this message. Test rigorously.
                    + objDbUpdateException.InnerException.Message;
            }
            return Redirect("ThankYou");
        }
    }

    //Creates the image.
    protected async void CreateImage()
    {
        //The user is uploading one image.
        if (ArtworkImage != null && ArtworkImage02 == null && ArtworkImage03 == null)
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
        else if (ArtworkImage != null && ArtworkImage02 != null && ArtworkImage03 == null) //The user is uploading two images.
        {
            //Make a list of the images that need to be uploaded.
            List<IFormFile> ImageList = new List<IFormFile>();
            ImageList.Add(ArtworkImage);
            ImageList.Add(ArtworkImage02);

            //Using a loop to upload each image.
            foreach (var image in ImageList)
            {
                string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images\\Artwork");
                string strFileName = Path.GetFileName(image.FileName);
                string strFilePath = Path.Combine(strImagesPath, strFileName);

                FileStream objFileStream = new FileStream(strFilePath, FileMode.Create);

                image.CopyTo(objFileStream);
                objFileStream.Close();
            }

            // Set the message.
            MessageColor = "Green";
            Message = "You have uploaded the images successfully!";
        }
        else if (ArtworkImage != null && ArtworkImage02 != null && ArtworkImage03 != null)
        {

            List<IFormFile> ImageList = new List<IFormFile>();
            ImageList.Add(ArtworkImage);
            ImageList.Add(ArtworkImage02);
            ImageList.Add(ArtworkImage03);

            //Using a loop to upload each image.
            foreach (var image in ImageList)
            {
                string strImagesPath = Path.Combine(IWebHostEnvironment.WebRootPath, "images\\Artwork");
                string strFileName = Path.GetFileName(image.FileName);
                string strFilePath = Path.Combine(strImagesPath, strFileName);

                FileStream objFileStream = new FileStream(strFilePath, FileMode.Create);

                image.CopyTo(objFileStream);
                objFileStream.Close();
            }

            // Set the message.
            MessageColor = "Green";
            Message = "You have uploaded the images successfully!";
        }
        else
        {
            // Set the message.
            MessageColor = "Red";
            Message = "Please select an image to upload and try again.";
        }
    }
}
