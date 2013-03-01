using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereShouldWeEatLunch.Models
{
    public class LunchAppointment
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public String FourSquareId { get; set; }

        public virtual UserModel UserModel { get; set; }
    }
}