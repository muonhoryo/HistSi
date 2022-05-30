
using System;
using UnityEngine;
using HistSi.ValueSources;

namespace HistSi
{
    public class TrackedValue<T>:IValueField<T>
    {
        public TrackedValue()
        {
            Value = default;
        }
        public TrackedValue(T value)
        {
            this.value = value;
        }


        [SerializeField]
        protected T value;
        public T Value
        {
            get => GetValue();
            set
            {
                SetValue(value);
                ValueChangeEvent();
            }
        }
        protected virtual void SetValue(T value)
        {
            this.value = value;
        }
        protected virtual T GetValue() => value;
        public event Action ValueChangeEvent;
        public static implicit operator T(TrackedValue<T> x)
        {
            return x.value;
        }
        public static implicit operator TrackedValue<T>(T x)
        {
            return new TrackedValue<T>(x);
        }
    }
}