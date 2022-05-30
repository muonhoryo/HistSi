


namespace HistSi.ValueSources
{
    public sealed class Converter_StringToFloat : Converter<string, float>
    {
        protected sealed override float ConvertValue(string value) =>
            value.StringToValue(delegate (string x, out float y)
          {
              return float.TryParse(x, out y);
          });
    }
}
