using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;
using NUnit.Framework;
using WhereShouldWeEatLunch.Controllers;
using WhereShouldWeEatLunch.Helpers;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Tests
{
    //Blah blah, I know this is depending on the DB for some of this because I dont
    //have the context mocked but whatever, just playing around for now.
    [TestFixture]
    public class ModelsTests
    {
        [Test]
        public void NoBlankEmails()
        {
            Assert.IsFalse(ValidationAttributes.IsValidEmail("test"));
        }
    }
}