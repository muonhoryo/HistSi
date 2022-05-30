
using MuonhoryoLibrary.Unity;
using MuonhoryoLibrary.UnityEditor;

namespace HistSi.ValueSources
{
    public static class Extensions
    {
        public static void CheckedInterfaceInit<TInterfaceType>(this IInterfaceDrawer<TInterfaceType> drawer)
            where TInterfaceType:class
        {
            drawer.InitInterface();
            if (drawer.DrawedInterface == null)
            {
                throw new HistSiException("Value Source Monobehavior does not inhert <" +
                    typeof(TInterfaceType) + ">");
            }
        }
        /// <summary>
        /// Tries to parse value to T type. If cannot parse,return default.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="parser"></param>
        /// <returns></returns>
        public static T StringToValue<T>(this string value, HistSi.TryParser<T> parser) where T : struct
        {
            if (parser(value, out T x))
            {
                return x;
            }
            else
            {
                throw new HistSiException("Cannot convert " + value + " to " + typeof(T));
            }
        }
    }
}
