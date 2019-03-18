using System;
using System.ComponentModel.DataAnnotations;

namespace G10_ProjectDotNet.Areas.Identity.Pages.Account.Manage
{
    public class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value is bool && (bool)value;
        }
    }
}