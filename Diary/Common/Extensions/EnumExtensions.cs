using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diary.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string EnumDisplayNameFor(this Enum item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayName = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayName != null)
            {
                return displayName.Name;
            }

            return item.ToString();
        }
    }
}