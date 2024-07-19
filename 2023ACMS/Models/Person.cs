using System.ComponentModel.DataAnnotations;

namespace _2023ACMS.Models
{
    public partial class Person
    {
        public Person()
        {
            Artwork = new HashSet<Artwork>();
            MediaSpecialty = new HashSet<MediaSpecialty>();
        }

        public int PersonId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Display(Name = "Email Address")]
        [RegularExpression(@"\S+\@\S+\.\S+", ErrorMessage = "Please enter an email address in the format aaa@bbb.ccc.")]
        [Required(ErrorMessage = "Please enter an email address.")]
        [StringLength(50)]
        public string EmailAddress { get; set; } = null!;

        [Display(Name = "Status")]
        [StringLength(1)]
        public string Status { get; set; } = null!;

        public virtual ICollection<Artwork> Artwork { get; set; }
        public virtual ICollection<MediaSpecialty> MediaSpecialty { get; set; }
    }
}
