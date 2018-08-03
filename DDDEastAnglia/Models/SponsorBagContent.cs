using System.ComponentModel.DataAnnotations;

namespace DDDEastAnglia.Models
{
    public class SponsorBagContent
    {
        [Key]
        public int ContentId { get; set; }

        public int SponsorId { get; set; }

        public string ContentText { get; set; }
    }
}