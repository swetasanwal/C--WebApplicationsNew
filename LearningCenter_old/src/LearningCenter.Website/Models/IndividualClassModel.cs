using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningCenter.Website.Models
{
    public class IndividualClassModel
    {
        public int ClassId { get; set; }
        public int UserId { get; set; }

        public IEnumerable<SelectListItem> classes { get; set; }

    }
}