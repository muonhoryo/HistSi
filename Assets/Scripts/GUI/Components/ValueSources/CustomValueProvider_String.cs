
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CustomValueProvider_String : CustomValueProvider<string>
    {
        protected sealed override Dictionary<string, TrackedValue<string>> valueSourcesList =>
            HistSi.CustomValues.CustomValuesString;
    }
}
