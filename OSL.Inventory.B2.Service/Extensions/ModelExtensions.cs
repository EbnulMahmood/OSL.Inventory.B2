using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OSL.Inventory.B2.Service.Extensions
{
    public static class ModelExtensions
    {
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.Trim().Length > 0;
        }

        public static void MergeError(this ModelStateDictionary modelState, IDictionary<string, string> dictionary)
        {
            Guard.AgainstNullParameter(modelState, "modelState");
            Guard.AgainstNullParameter(dictionary, "dictionary");

            foreach (var item in dictionary)
            {
                modelState.AddModelError(item.Key, item.Value);
            }
        }
    }

    public static class Guard
    {
        public static void AgainstNullParameter(object parameter, string parameterName)
        {
            if (parameter == null) throw new ArgumentNullException(parameterName);

        }
    }
}
