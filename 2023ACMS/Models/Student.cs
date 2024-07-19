using System.ComponentModel.DataAnnotations;

namespace _2023ACMS.Models
{
    public partial class Student
    {
        public Student()
        {
            Artwork = new HashSet<Artwork>();
        }

        public int StudentId { get; set; }

        [Display(Name = "Media Preference")]
        public int MediaPreferenceId { get; set; }

        [Display(Name = "Student First Name")]
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(50)]
        public string StudentFirstName { get; set; } = null!;

        [Display(Name = "Student Last Name")]
        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(50)]
        public string StudentLastName { get; set; } = null!;

        [Display(Name = "Student Email")]
        [RegularExpression(@"\S+\@\S+\.\S+", ErrorMessage = "Please enter an email address in the format aaa@bbb.ccc.")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [StringLength(50)]
        public string StudentEmail { get; set; } = null!;

        [Display(Name = "Parent Name")]
        [StringLength(50)]
        public string? ParentName { get; set; }

        [Display(Name = "Parent Email")]
        [RegularExpression(@"\S+\@\S+\.\S+", ErrorMessage = "Please enter an email address in the format aaa@bbb.ccc.")]
        [StringLength(50)]
        public string? ParentEmail { get; set; }

        [Display(Name = "Teacher Name")]
        [StringLength(50)]
        public string? TeacherName { get; set; }

        [Display(Name = "Teacher Email")]
        [RegularExpression(@"\S+\@\S+\.\S+", ErrorMessage = "Please enter an email address in the format aaa@bbb.ccc.")]
        [StringLength(50)]
        public string? TeacherEmail { get; set; }

        [Display(Name = "Portfolio Review")]
        public bool PortfolioReview { get; set; }

        [Display(Name = "Attendance")]
        public bool Attending { get; set; }

        public virtual Media MediaPreference { get; set; } = null!;
        public virtual ICollection<Artwork> Artwork { get; set; }
    }
}
