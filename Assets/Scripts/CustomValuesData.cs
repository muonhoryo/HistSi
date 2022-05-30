
using System;
using System.Collections.Generic;
using UnityEngine;
using MuonhoryoLibrary.Serialization;

namespace HistSi.ValueSources
{
    [Serializable]
    [CreateAssetMenu]
    public sealed class CustomValuesData: ScriptableObject
    {
        public sealed class CustomVariableSerializator<TKey,TValue>: ISerializator
        {
            public static readonly CustomVariableSerializator<TKey, TValue> Instance =
                new CustomVariableSerializator<TKey, TValue>();
            public CustomVariableSerializator() { }
            public string Serialize<T>(T obj)
            {
                var kvp=(Serialization.Pair<TKey, TrackedValue<TValue>>)(object)obj;
                return JsonUtility.ToJson(new Serialization.Pair<TKey,TValue>(kvp.first,
                    kvp.second!=null?kvp.second.Value:default),true);
            }
            private object Deserialize(string json)
            {
                var kvp = JsonUtility.FromJson <Serialization.Pair<TKey, TValue>>(json);
                return new Serialization.Pair<TKey, TrackedValue<TValue>>
                    (kvp.first, new TrackedValue<TValue>(kvp.second));
            }
            T ISerializator.Deserialize<T>(string json)
            {
                return (T)Deserialize(json);
            }
        }

        public string NewKey_Int32;
        public int CurrentValue_Int32;
        public const string serializationPath_Int32 = 
            HistSi.SerializationDirectory + "/CustomValues_Int32.json";
        /// <summary>
        /// ONLY FOR SERIALIZATION!!!
        /// </summary>
        public Dictionary<string, TrackedValue<int>> CustomValues_Int32;
        public Dictionary<string, TrackedValue<int>> CustomValuesInt32
        {
            get
            {
                if (CustomValues_Int32 == null)
                {
                    CustomValues_Int32 = DictionarySerializator.ReadOrCreateNewFile<string,TrackedValue<int>>
                        (serializationPath_Int32,new CustomVariableSerializator<string, int>());
                }
                return CustomValues_Int32;
            }
        }

        public string NewKey_Float;
        public float CurrentValue_Float;
        public const string serializationPath_Float =
            HistSi.SerializationDirectory + "/CustomValues_Float.json";
        /// <summary>
        /// ONLY FOR SERIALIZATION!!!
        /// </summary>
        public Dictionary<string, TrackedValue<float>> CustomValues_Float;
        public Dictionary<string, TrackedValue<float>> CustomValuesFloat
        {
            get
            {
                if (CustomValues_Float == null)
                {
                    CustomValues_Float = DictionarySerializator.ReadOrCreateNewFile<string, TrackedValue <float>>
                        (serializationPath_Float, new CustomVariableSerializator<string, float>());
                }
                return CustomValues_Float;
            }
        }

        public string NewKey_Bool;
        public bool CurrentValue_Bool;
        public const string serializationPath_Bool =
            HistSi.SerializationDirectory + "/CustomValues_Bool.json";
        /// <summary>
        /// ONLY FOR SERIALIZATION!!!
        /// </summary>
        public Dictionary<string, TrackedValue<bool>> CustomValues_Bool;
        public Dictionary<string,TrackedValue<bool>> CustomValuesBool
        {
            get
            {
                if (CustomValues_Bool == null)
                {
                    CustomValues_Bool = DictionarySerializator.ReadOrCreateNewFile<string, TrackedValue<bool>>
                        (serializationPath_Bool, new CustomVariableSerializator<string, bool>());
                }
                return CustomValues_Bool;
            }
        }

        public string NewKey_String;
        public string CurrentValue_String;
        public const string serializationPath_String =
            HistSi.SerializationDirectory + "/CustomValues_String.json";
        /// <summary>
        /// ONLY FOR SERIALIZATION!!!
        /// </summary>
        public Dictionary<string, TrackedValue<string>> CustomValues_String;
        public Dictionary<string, TrackedValue<string>> CustomValuesString
        {
            get
            {
                if (CustomValues_String == null)
                {
                    CustomValues_String = DictionarySerializator.ReadOrCreateNewFile<string, TrackedValue<string>>
                        (serializationPath_String, new CustomVariableSerializator<string, string>());
                }
                return CustomValues_String;
            }
        }
    }
}
