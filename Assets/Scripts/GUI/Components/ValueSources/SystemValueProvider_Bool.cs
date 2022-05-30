


namespace HistSi.ValueSources
{
    public sealed class SystemValueProvider_Bool :
        SystemValueProvider<bool, SystemTrackedValues.SourceType_Bool>
    {
        public sealed override IGetterValue<bool> ValueSource =>
            SystemTrackedValues.SystemTrackedValues_Bool[(int)SourceType];
    }
}
