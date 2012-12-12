using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereShouldWeEatLunch.Models
{
    public class EateryModel
    {
        public int Id { get; set; }

        [Display(Name = "Walking Distance?")]
        public Boolean IsWalkingDistance { get; set; }

        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Style")]
        public virtual FoodStyleModel FoodStyleModel { get; set; }
    }
}
