
using System.Collections.Generic;

namespace HistSi.ValueSources
{
    public sealed class CustomValueProvider_Bool : CustomValueProvider<bool>
    {
        protected sealed override Dictionary<string, TrackedValue<bool>> valueSourcesList =>
            HistSi.CustomValues.CustomValuesBool;
    }
}
