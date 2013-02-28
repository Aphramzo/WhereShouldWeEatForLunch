using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereShouldWeEatLunch.Models
{
    public class CrewMemberModel
    {
        public int Id { get; set; }
        public virtual UserModel UserModel { get; set; }
    }
}