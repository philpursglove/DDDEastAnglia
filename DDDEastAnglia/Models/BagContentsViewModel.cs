using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDDEastAnglia.Models
{
    public class BagContentsViewModel
    {
        public BagContentsViewModel()
        {
            SponsorBagContents = new List<SponsorBagContent>();
        }

        public IEnumerable<SponsorBagContent> SponsorBagContents { get; set; }
    }
}