using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonalSiteMVC.UI.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name must contain a value.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email Address must contain a value.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "*Message cannot be blank.")]
        [UIHint("MultilineText")]
        public string Message { get; set; }


    }
}