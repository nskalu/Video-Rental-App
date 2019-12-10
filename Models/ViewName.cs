using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vidly.Models
{
    public static class ViewName
    {
        //i havent used this class yet
        public static string CurrentViewName(this HtmlHelper html)
        {
            return System.IO.Path.GetFileNameWithoutExtension(
                ((RazorView)html.ViewContext.View).ViewPath
            );
        } 
    }
}