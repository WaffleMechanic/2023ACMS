namespace _2023ACMS.Models
{
    public partial class MediaSpecialty
    {
        public int MediaSpecialtyId { get; set; }
        public int PersonId { get; set; }
        public int MediaId { get; set; }

        public virtual Media Media { get; set; } = null!;
        public virtual Person Person { get; set; } = null!;
    }
}
