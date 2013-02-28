using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhereShouldWeEatLunch.Models
{
    public class CrewModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public virtual UserModel UserModel { get; set; }

        public virtual List<CrewMemberModel> CrewMembers { get; set; }
    }
}