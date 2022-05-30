


namespace HistSi.ValueSources
{
    public sealed class SystemValueProvider_String :
        SystemValueProvider<string, SystemTrackedValues.SourceType_String>
    {
        public sealed override IGetterValue<string> ValueSource =>
            SystemTrackedValues.SystemTrackedValues_String[(int)SourceType];
    }
}
