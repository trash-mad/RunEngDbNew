using DbElems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DbWindows
{
    public class OptionToDescription : MarkupExtension
    {
        private readonly Type _type;


        public static string GetDescription(OptionType source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

            public OptionToDescription(Type type)
        {
            _type = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_type)
                .Cast<object>()
                .Select(e => new { Value = (OptionType)e, DisplayName = GetDescription((OptionType)e)});
        }
    }
}
