using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DDDEastAnglia.Areas.Admin.Models
{
    public class AddSponsorContentViewModel
    {
        public IEnumerable<SelectListItem> Sponsors { get; set; }

        [Required]
        public int SponsorId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}