using System.ComponentModel.DataAnnotations;

namespace _2023ACMS.Models
{
    public partial class Default
    {
        public int DefaultId { get; set; }

        [Display(Name = "Allow Submissions")]
        public bool AllowSubmissions { get; set; }
    }
}
