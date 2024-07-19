using System.ComponentModel.DataAnnotations;

namespace _2023ACMS.Models
{
    public partial class Artwork
    {
        public int ArtworkId { get; set; }

        [Display(Name = "Student Last Name")]
        public int StudentId { get; set; }

        [Display(Name = "Media Name")]
        public int MediaId { get; set; }

        [Display(Name = "Judge")]
        public int? PersonId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Display(Name = "File Name")]
        public string Filename { get; set; } = null!;

        [Display(Name = "Judge Notes")]
        public string? JudgeNotes { get; set; }

        [Display(Name = "Accept")]
        public bool Accept { get; set; }

        [Display(Name = "Judged")]
        public bool Judged { get; set; }

        public virtual Media Media { get; set; } = null!;
        public virtual Person? Person { get; set; }
        public virtual Student Student { get; set; } = null!;
    }
}
