using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BirthdayCardGenerator.Models
{
    public class BirthdayMessageDetails
    {
        [Required(ErrorMessage = "Enter the sender email address")]
        [EmailAddress]
        public string Sender { get; set; }

        [Required(ErrorMessage = "Enter the receiver email address")]
        [EmailAddress]
        public string Receiver { get; set; }

        [Required(ErrorMessage = "Enter the message")]
        public string Message { get; set; }

    }
}