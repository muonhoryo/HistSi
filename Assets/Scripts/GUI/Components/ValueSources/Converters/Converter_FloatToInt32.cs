

namespace HistSi.ValueSources
{
    public sealed class Converter_FloatToInt32 : Converter<float, int>
    {
        protected sealed override int ConvertValue(float value) => (int)value;
    }
}
