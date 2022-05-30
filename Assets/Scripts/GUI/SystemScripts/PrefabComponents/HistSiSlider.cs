
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HistSi.ValueSources;
using HistSi.GUI;

namespace HistSi.GUI
{
    public sealed class HistSiSlider : Slider, IRemovable,ICommandRuner,IValueListener<float>
    {
        [SerializeField]
        private ValueProvider<float> ValueSource;
        [SerializeField]
        private Animation onDestroyAnimation;
        public Animation OnDestroyAnimation => onDestroyAnimation;
        public GameObject DestroyedObject => gameObject;
        IGetterValue<float> IValueListener<float>.ValueSource => ValueSource;
        public void Remove()
        {
            Removable.Remove(this, delegate { interactable = false; });
        }
        Coroutine IRemovable.StartCoroutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
        public void RunCommand( Commands.ButtonCommand command)
        {
            command.CommandRun();
        }
        public void RunCommandList(ButtonCommandsQueue commandsList)
        {
            commandsList.ExecuteCommands();
        }
        void IValueListener<float>.OnValueChanged() => OnValueChanged();
        private void OnValueChanged()
        {
            if (ValueSource.Value != m_Value)
            {
                Set(ValueSource.Value, false);
            }
        }
        protected sealed override void Awake()
        {
            base.Awake();
            if (ValueSource != null)
            {
                onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(
                    delegate (float value)
                    {
                        if (Application.isPlaying) ValueSource.Value = value;
                    }));
                ValueSource.ValueChangeEvent += OnValueChanged;
            }
            else
            {
                throw new HistSiException("Value Source does not assigned");
            }
        }
        protected sealed override void Start()
        {
            base.Start();
            OnValueChanged();
        }
        protected sealed override void OnDestroy()
        {
            base.OnDestroy();
            ValueSource.ValueChangeEvent -= OnValueChanged;
        }
    }
}
