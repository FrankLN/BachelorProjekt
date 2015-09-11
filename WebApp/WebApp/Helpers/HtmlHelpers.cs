using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApp.Helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString ToolTipFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            var exp = (MemberExpression)expression.Body;
            foreach (Attribute attribute in exp.Expression.Type.GetProperty(exp.Member.Name).GetCustomAttributes(false))
            {
                if (typeof(ViewModels.TooltipAttribute) == attribute.GetType())
                {
                    return MvcHtmlString.Create(((ViewModels.TooltipAttribute)attribute).Description);
                }
            }
            return MvcHtmlString.Create("");
        }
    }
}
