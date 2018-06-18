using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneReworked.ViewModel
{
    public class ContactViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string FromName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string FromEmail { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Your Message")]
        public string Body { get; set; }
    }
}