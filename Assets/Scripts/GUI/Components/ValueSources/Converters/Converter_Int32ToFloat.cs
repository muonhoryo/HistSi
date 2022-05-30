


namespace HistSi.ValueSources
{
    public sealed class Converter_Int32ToFloat : Converter<int, float>
    {
        protected sealed override float ConvertValue(int value)=>value;
    }
}
