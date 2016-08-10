﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Ig2.Models.PlainHolders
{
    public class SearchInfo
    {
        [Required(ErrorMessage="Search field cannot be empty")]
        public string neededItem { get; set; }
        public string searchIndex { get; set; }
    }
}