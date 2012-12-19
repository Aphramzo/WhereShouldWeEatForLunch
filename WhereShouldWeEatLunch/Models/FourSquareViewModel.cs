using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereShouldWeEatLunch.Models
{
    public class FourSquareViewModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}