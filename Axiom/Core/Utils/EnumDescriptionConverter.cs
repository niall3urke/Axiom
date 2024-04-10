using System;
using System.ComponentModel;
using System.Globalization;

namespace Axiom.Core.Utils
{
    public class EnumDescriptionConverter : EnumConverter
    {

        // Fields

        private readonly Type _enumType;

        // Constructors

        public EnumDescriptionConverter(Type type) : base(type)
        {
            _enumType = type;
        }

        // Methods

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
        {
            try
            {
                destType = typeof(string);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destType)
        {
            if (value == null)
                return string.Empty;

            var fi = _enumType.GetField(Enum.GetName(_enumType, value));

            var da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

            if (da != null)
            {
                return da.Description;
            }
            return value.ToString();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type srcType)
        {
            try
            {
                srcType = typeof(string);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            foreach (var fi in _enumType.GetFields())
            {
                var da = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

                if (da != null && (string)value == da.Description)
                    return Enum.Parse(_enumType, fi.Name);
            }

            return Enum.Parse(_enumType, (string)value);
        }


    }
}
