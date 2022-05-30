


namespace HistSi.ValueSources
{
    public sealed class Converter_Int32ToString : Converter<int, string>
    {
        protected sealed override string ConvertValue(int value) => value.ToString();
    }
}
