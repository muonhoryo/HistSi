
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using HistSi.ValueSources;

namespace HistSi
{
    public class HistSiException : Exception
    {
        public HistSiException(string message) : base(message)
        {
            Debug.LogError(message);
        }
        public static void CheckRuntimeAppealing(string fieldName="unnamed")
        {
            if (Application.isPlaying)
            {
                throw new HistSiException("Trying to appeal to " + fieldName + " in playmode");
            }
        }
    }
    public static class HistSi
    {
        public delegate bool TryParser<T>(string x, out T y);
        public static CustomValuesData CustomValues;
        public static GameObject UICanvas;
        public const string SerializationDirectory = "Assets/Scripts/SerializationData";
    }
    public static class SystemTrackedValues
    {
        private sealed class AudioLevelValue : TrackedValue<float>
        {
            public AudioLevelValue() : base() { }
            public AudioLevelValue(float value) : base(value) { }
            protected override void SetValue(float value)
            {
                if (value < 0)
                {
                    this.value = 0;
                }
                else if (value > 1)
                {
                    this.value = 1;
                }
                else
                {
                    this.value = value;
                }
            }
        }
        //Float
        public static TrackedValue<float> MusicLevel = new AudioLevelValue(1f);
        public static TrackedValue<float> SoundLevel = new AudioLevelValue(1f);
        public static readonly ReadOnlyCollection<TrackedValue<float>> SystemTrackedValues_Float =
            new ReadOnlyCollection<TrackedValue<float>>
            (new TrackedValue<float>[]
            {
                MusicLevel,
                SoundLevel
            });
        //Integer
        public static readonly ReadOnlyCollection<TrackedValue<int>> SystemTrackedValues_Int =
            new ReadOnlyCollection<TrackedValue<int>>
            (new TrackedValue<int>[]
            {
            });
        //String
        public static readonly ReadOnlyCollection<TrackedValue<string>> SystemTrackedValues_String =
            new ReadOnlyCollection<TrackedValue<string>>
            (new TrackedValue<string>[]
            {
            });
        //Boolean
        public static readonly ReadOnlyCollection<TrackedValue<bool>> SystemTrackedValues_Bool =
            new ReadOnlyCollection<TrackedValue<bool>>
            (new TrackedValue<bool>[]
            {
            });

        [Serializable]
        public enum SourceType_Float
        {
            MusicLevel,
            SoundLevel
        }
        [Serializable]
        public enum SourceType_Int { }
        [Serializable]
        public enum SourceType_String { }
        [Serializable]
        public enum SourceType_Bool { }
    }
}