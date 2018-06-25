using System.ComponentModel.DataAnnotations;

namespace DDDEastAnglia.Models
{
    public class BagIndexViewModel
    {
        [Required]
        public string OrderNumber { get; set; }


        public string ErrorMessage { get; set; }
    }
}