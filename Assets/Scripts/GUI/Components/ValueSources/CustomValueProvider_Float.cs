
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CustomValueProvider_Float : CustomValueProvider<float>
    {
        protected sealed override Dictionary<string, TrackedValue<float>> valueSourcesList =>
            HistSi.CustomValues.CustomValuesFloat;
    }
}
