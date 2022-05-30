


namespace HistSi.ValueSources
{
    public sealed class Converter_StringToInt32 : Converter<string, int>
    {
        protected sealed override int ConvertValue(string value) => 
            value.StringToValue(delegate (string x, out int y)
          {
              return int.TryParse(x, out y);
          });
    }
}
