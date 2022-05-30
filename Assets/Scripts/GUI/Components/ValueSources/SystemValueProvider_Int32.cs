


namespace HistSi.ValueSources
{
    public sealed class SystemValueProvider_Int32 : 
        SystemValueProvider<int, SystemTrackedValues.SourceType_Int>
    {
        public sealed override IGetterValue<int> ValueSource =>
            SystemTrackedValues.SystemTrackedValues_Int[(int)SourceType];
    }
}
