


namespace HistSi.ValueSources
{
    public sealed class SystemValueProvider_Float : 
        SystemValueProvider<float, SystemTrackedValues.SourceType_Float>
    {
        public sealed override IGetterValue<float> ValueSource =>
            SystemTrackedValues.SystemTrackedValues_Float[(int)SourceType];
    }
}
