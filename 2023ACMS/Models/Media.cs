using System.ComponentModel.DataAnnotations;

namespace _2023ACMS.Models
{
    public partial class Media
    {
        public Media()
        {
            Artwork = new HashSet<Artwork>();
            MediaSpecialty = new HashSet<MediaSpecialty>();
            Student = new HashSet<Student>();
        }

        public int MediaId { get; set; }

        [Display(Name = "Media Name")]
        [Required(ErrorMessage = "Please enter a media name.")]
        [StringLength(50)]
        public string Media1 { get; set; } = null!;

        public virtual ICollection<Artwork> Artwork { get; set; }
        public virtual ICollection<MediaSpecialty> MediaSpecialty { get; set; }
        public virtual ICollection<Student> Student { get; set; }
    }
}
