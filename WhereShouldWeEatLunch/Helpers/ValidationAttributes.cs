using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WhereShouldWeEatLunch.Models;

namespace WhereShouldWeEatLunch.Helpers
{
    public class ValidationAttributes
    {
        [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
        public class EmailAddressValidAndAvailable : DataTypeAttribute
        {
            private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);

            public EmailAddressValidAndAvailable()
                : base(DataType.EmailAddress)
            {

            }

            public override bool IsValid(object value)
            {

                string str = Convert.ToString(value, CultureInfo.CurrentCulture);
                if (string.IsNullOrEmpty(str))
                    return true;

                Match match = regex.Match(str);
                var valid = ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
                ErrorMessage = "Please enter a valid email address.";
                if(!valid)
                    return false;
                ErrorMessage = String.Format("The user {0} already exists.",str);
                return UserExists(str);
            }

            //slow since it has to query the DB - but I dont know a better way to validate uniqueness
            private static bool UserExists(string str)
            {
                var db = new WhereShouldWeEatLunchContext();
                var user =
                    db.UserModels.FirstOrDefault(c => c.EmailAddress.Trim().ToLower() == str.Trim().ToLower());

                return user == null;
            }
        }
    }
}