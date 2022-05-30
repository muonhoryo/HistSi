

namespace HistSi.ValueSources
{
    public sealed class Converter_FloatToString : Converter<float, string>
    {
        protected sealed override string ConvertValue(float value) => value.ToString();
    }
}
