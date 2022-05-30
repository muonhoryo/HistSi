
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MuonhoryoLibrary.UnityEditor;
using HistSi.CustomEditor;

namespace HistSi.GUI.Commands
{
    /// <summary>
    /// Base of command component. Allows customize work behavior of ICommandRuner in inspector without coding.
    /// </summary>
    public abstract class ButtonCommand : MonoBehaviour
    {
        public abstract void CommandRun();
    }
}
namespace HistSi.ValueSources
{
    /// <summary>
    /// Base of source of value component. Allows customize,where to get the value.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class ValueProvider<TValueType> : MonoBehaviour, IValueField<TValueType>
    {
        public abstract TValueType Value { get; set; }

        public abstract event Action ValueChangeEvent;
    }
    public abstract class TrackedValueProvider<TValueType> : ValueProvider<TValueType>,
        IValueListener<TValueType>
    {
        public abstract IGetterValue<TValueType> ValueSource { get; }
        public sealed override TValueType Value
        {
            get => ValueSource.Value;
            set => ((ISetterValue<TValueType>)ValueSource).Value = value;
        }
        public sealed override event Action ValueChangeEvent = () => { };
        void IValueListener<TValueType>.OnValueChanged()
        {
            OnValueChanged();
        }
        private void OnValueChanged()
        {
            ValueChangeEvent();
        }
        protected void Awake()
        {
            ValueSource.ValueChangeEvent += OnValueChanged;
        }
        private void Start()
        {
            OnValueChanged();
        }
        private void OnDestroy()
        {
            ValueSource.ValueChangeEvent -= OnValueChanged;
        }
    }
    public abstract class SystemValueProvider<TValueType, TSourceEnum> : TrackedValueProvider<TValueType>
        where TSourceEnum : Enum
    {
        [SerializeField]
        public TSourceEnum SourceType;
    }
    public abstract class CustomValueProvider<TValueType> : TrackedValueProvider<TValueType>,
        ISerializationCallbackReceiver
    {
        protected IValueField<TValueType> valueSource;
        private Dictionary<string, TrackedValue<TValueType>> ValueSourcesList
        {
            get
            {
                if (HistSi.CustomValues == null)
                {
                    throw new HistSiException("Custom values is not assigned!");
                }
                else
                {
                    return valueSourcesList;
                }
            }
        }
        protected abstract Dictionary<string, TrackedValue<TValueType>> valueSourcesList { get; }
        public override IGetterValue<TValueType> ValueSource => valueSource;
        public string ValueName;
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (ValueSourcesList != null)
            {
                foreach (var key in ValueSourcesList.Keys)
                {
                    if (ValueSourcesList[key] == valueSource)
                    {
                        ValueName = key;
                        return;
                    }
                }
            }
        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (ValueSourcesList != null && valueSourcesList.ContainsKey(ValueName))
            {
                valueSource = valueSourcesList[ValueName];
                ValueName = null;
            }
        }
    }
    public abstract class ConstSource<TValueType> : MonoBehaviour, IGetterValue<TValueType>
    {
        [SerializeField]
        private TValueType value;
        public TValueType Value => value;
        public event Action ValueChangeEvent
        {
            add { }
            remove { }
        }
    }
    /// <summary>
    /// Base of text field with dependence of any value. When the value changed,text is updated with that value.
    /// In inspector assigned text field and ValueSource.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class TextDependence<TValueType> : MonoBehaviour, IValueListener<TValueType>,
        IInterfaceDrawer<IGetterValue<TValueType>>
    {
        public IGetterValue<TValueType> DrawedInterface { get; set; }
        public Component InterfaceComponent
        { get => interfaceComponent; set => interfaceComponent = value; }
        [SerializeField]
        private Component interfaceComponent;
        public Text ValueText;
        public IGetterValue<TValueType> ValueSource => DrawedInterface;
        private void OnValueChanged()
        {
            ValueText.text = ValueSource.Value.ToString();
        }
        void IValueListener<TValueType>.OnValueChanged() => OnValueChanged();
        protected void Awake()
        {
            this.CheckedInterfaceInit();
            ValueSource.ValueChangeEvent += OnValueChanged;
        }
        protected void Start()
        {
            OnValueChanged();
        }
        protected void OnDestroy()
        {
            ValueSource.ValueChangeEvent -= OnValueChanged;
        }
    }
    /// <summary>
    /// Base of maths operations with two operand. Avaible operations are indicated in MathOperationType enum.
    /// In inspector assigned operands and type of operation. Operands can be IGetterValue(type of TValueType) 
    /// or TValueType. Calculates the value when he is received.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class PairMathOperation<TValueType> : MonoBehaviour, IGetterValue<TValueType>,
        IValueListener<TValueType, TValueType> where TValueType : struct
    {
        public int OperationType;
        protected abstract IList<Func<TValueType, TValueType, TValueType>> Operations { get; }
        public virtual TValueType Value => Operations[OperationType](ValueSource_1.Value, ValueSource_2.Value);
        public IGetterValue<TValueType> ValueSource_1 { get; set; }
        public IGetterValue<TValueType> ValueSource_2 { get; set; }
        public PairMathOpSerializationContainer serializationContainer;
        public event Action ValueChangeEvent;
        void IValueListener<TValueType, TValueType>.OnValueChanged() => OnValueChanged();
        protected virtual void OnValueChanged()
        {
            ValueChangeEvent();
        }
        protected void InitializeFields()
        {
            if (serializationContainer == null)
            {
                throw new HistSiException("Serialization data cannot be null.");
            }
            ValueSource_1 = serializationContainer.FirstOperand as IGetterValue<TValueType>;
            ValueSource_2 = serializationContainer.SecondOperand as IGetterValue<TValueType>;
            if (ValueSource_1 == null || ValueSource_2 == null)
            {
                throw new HistSiException("Operands is not assigned.");
            }
        }
        private void Awake()
        {
            InitializeFields();
            Destroy(serializationContainer);
            ValueSource_1.ValueChangeEvent += OnValueChanged;
            ValueSource_2.ValueChangeEvent += OnValueChanged;
        }
        private void OnDestroy()
        {
            ValueSource_1.ValueChangeEvent -= OnValueChanged;
            ValueSource_2.ValueChangeEvent -= OnValueChanged;
        }
    }
    /// <summary>
    /// Base of converter component. Allows get value from ValueSource in type of TOutput.
    /// In inspector assigned ValueSource.
    /// </summary>
    /// <typeparam name="TInput"></typeparam>
    /// <typeparam name="TOutput"></typeparam>
    /// <summary>
    /// Math operation with two operand. Cashes the math result and update his when any operand was changed.
    /// </summary>
    /// <typeparam name="TValueType"></typeparam>
    public abstract class CashingPairMathOperation<TValueType> : PairMathOperation<TValueType>
        where TValueType : struct
    {
        private TValueType value;
        public override TValueType Value => value;
        protected sealed override void OnValueChanged()
        {
            value = Operations[OperationType](ValueSource_1.Value, ValueSource_2.Value);
            base.OnValueChanged();
        }
        private void Start()
        {
            OnValueChanged();
        }
    }
    public abstract class ArrayMathOperation<TValueType> : MonoBehaviour, IGetterValue<TValueType>,
        IArrayValueListener<TValueType> where TValueType : struct
    {
        public int OperationType;
        protected abstract IList<Func<IGetterValue<TValueType>[], TValueType>> Operations { get; }
        public virtual TValueType Value => Operations[OperationType](ValueSources);
        public IGetterValue<TValueType>[] ValueSources { get; set; }
        [SerializeField]
        private Component[] sourcesComponents;
        public Component[] SourcesComponents
        {
            get
            {
                HistSiException.CheckRuntimeAppealing();
                return sourcesComponents;
            }
            set
            {
                HistSiException.CheckRuntimeAppealing();
                sourcesComponents = value;
            }
        }
        public event Action ValueChangeEvent;
        void IArrayValueListener<TValueType>.OnValueChanged() => OnValueChanged();
        protected virtual void OnValueChanged()
        {
            ValueChangeEvent();
        }
        private void InitializeSources()
        {
            ValueSources = new IGetterValue<TValueType>[sourcesComponents.Length];
            for (int i = 0; i < ValueSources.Length; i++)
            {
                ValueSources[i] = sourcesComponents[i] as IGetterValue<TValueType>;
            }
        }
        private void Awake()
        {
            InitializeSources();
            foreach (var source in ValueSources)
            {
                source.ValueChangeEvent += OnValueChanged;
            }
            SourcesComponents = null;
        }
        private void OnDestroy()
        {
            foreach (var source in ValueSources)
            {
                source.ValueChangeEvent -= OnValueChanged;
            }
        }
    }
    public abstract class CashingArrayMathOperation<TValueType> : ArrayMathOperation<TValueType>
        where TValueType : struct
    {
        private TValueType value;
        public sealed override TValueType Value => value;
        protected sealed override void OnValueChanged()
        {
            value = Operations[OperationType](ValueSources);
            base.OnValueChanged();
        }
        private void Start()
        {
            OnValueChanged();
        }
    }
    public abstract class Converter<TInput, TOutput> : MonoBehaviour, IGetterValue<TOutput>,
        IValueListener<TInput>, IInterfaceDrawer<IGetterValue<TInput>>
    {
        void IValueListener<TInput>.OnValueChanged() => OnValueChanged();
        public IGetterValue<TInput> DrawedInterface { get; set; }
        public Component InterfaceComponent
        {
            get
            {
                HistSiException.CheckRuntimeAppealing();
                return interfaceComponent;
            }
            set
            {
                HistSiException.CheckRuntimeAppealing();
                interfaceComponent = value;
            }
        }
        [SerializeField]
        private Component interfaceComponent;
        public IGetterValue<TInput> ValueSource => DrawedInterface;
        protected virtual void OnValueChanged()
        {
            ValueChangeEvent();
        }
        protected abstract TOutput ConvertValue(TInput value);
        public virtual TOutput Value => ConvertValue(DrawedInterface.Value);
        public event Action ValueChangeEvent;
        private void Awake()
        {
            this.CheckedInterfaceInit();
            ValueSource.ValueChangeEvent += OnValueChanged;
        }
        private void Start()
        {
            OnValueChanged();
        }
        private void OnDestroy()
        {
            ValueSource.ValueChangeEvent -= OnValueChanged;
        }
    }
    public abstract class CashingConverter<TInput, TOutput> : Converter<TInput, TOutput>
    {
        protected sealed override void OnValueChanged()
        {
            value = ConvertValue(DrawedInterface.Value);
            base.OnValueChanged();
        }
        private TOutput value;
        public sealed override TOutput Value => value;
    }
}
