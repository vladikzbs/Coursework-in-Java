using Coursework_in_Java.Models.Inspectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Coursework_in_Java.AppKernel.HelpersExtensions
{
    public static class HtmlHelperExtensions
    {
        public static bool HasInspector(this HtmlHelper helper, InspectorModel inspector)
        {
            if (inspector == null)
                return false;

            if (inspector.Name == "Default")
                return false;

            if (string.IsNullOrEmpty(inspector.Name) || string.IsNullOrEmpty(inspector.Surname) || string.IsNullOrEmpty(inspector.Patronymic))
                return false;

            return true;
        }

        public static string YesNo(this HtmlHelper helper, bool property)
        {
            string yes = "Так";
            string no = "Ні";

            if (property == true)
            {
                return yes;
            }
            else
            {
                return no;
            }
        }

        public static string YesNo(this HtmlHelper helper, bool? property)
        {
            string yes = "Так";
            string no = "Ні";

            if (property == null)
            {
                return no;
            }
            else if (property == true)
            {
                return yes;
            }
            else
            {
                return no;
            }
        }
    }
}