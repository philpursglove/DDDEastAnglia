﻿using System.Collections.Generic;

namespace DDDEastAnglia.Models
{
    public class SessionIndexModel
    {
        public bool IsOpenForSubmission { get; set; }
        public bool IsOpenForVoting { get; set; }

        public IEnumerable<SessionDisplayModel> Sessions { get; set; }
    }
}