using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    // Class code found at http://stackoverflow.com/a/3711878
    public static class HtmlHelpers
    {
        public static MvcHtmlString ToolTipFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var exp = (MemberExpression)expression.Body;
            foreach (Attribute attribute in exp.Expression.Type.GetProperty(exp.Member.Name).GetCustomAttributes(false))
            {
                if (typeof(Helpers.TooltipAttribute) == attribute.GetType())
                {
                    return MvcHtmlString.Create(((Helpers.TooltipAttribute)attribute).Description);
                }
            }
            return MvcHtmlString.Create("");
        }
    }
}
