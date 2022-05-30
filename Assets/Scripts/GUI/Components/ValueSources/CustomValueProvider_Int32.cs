
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CustomValueProvider_Int32 : CustomValueProvider<int>
    {
        protected sealed override Dictionary<string, TrackedValue<int>> valueSourcesList =>
            HistSi.CustomValues.CustomValuesInt32;
    }
}
