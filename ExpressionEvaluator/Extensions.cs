using System.Globalization;
using System.Text;

namespace ExpressionEvaluator
{
    static class Extensions
    {
        public static T As<T>(this object value) => (T)value;

        public static string Flush(this StringBuilder buffer)
        {
            var s = buffer.ToString();

            buffer.Clear();

            return s;
        }

        public static double? ToInvariantDoubleOrNull(this string value)
        {
            var result = 0.0;

            return double.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result) ? result : (double?)null;
        }
    }
}
