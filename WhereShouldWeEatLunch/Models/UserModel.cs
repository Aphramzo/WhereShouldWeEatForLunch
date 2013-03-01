using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhereShouldWeEatLunch.Helpers;

namespace WhereShouldWeEatLunch.Models
{
    public class UserModel
    {

        public int Id { get; set; }

        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "An Email Address is Required.")]
        [ValidationAttributes.EmailAddressValidAndAvailable]
        [Display(Name = "Email Address")]
        public String EmailAddress { get; set; }

        public virtual List<CrewModel> CrewModels { get; set; }

        public virtual List<LunchAppointment> LunchAppointments { get; set; } 
    }
}