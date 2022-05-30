
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using MuonhoryoLibrary.UnityEditor;
using HistSi.ValueSources;

namespace HistSi.CustomEditor
{
    public abstract class EditModeOnlyEditor<TTargetType> : Editor
        where TTargetType : MonoBehaviour
    {
        protected TTargetType ParsedTarget;
        protected bool isEnable { get; private set; } = false;
        private void OnEnable()
        {
            ParsedTarget = target as TTargetType;
            isEnable = true;
            OnInitializeEditor();
        }
        public sealed override void OnInspectorGUI()
        {
            if (!isEnable)
            {
                OnEnable();
            }
            if (Application.isPlaying)
            {
                RuntimeDraw();
            }
            else
            {
                EditModeDraw();
            }
        }
        private void OnDisable()
        {
            if (ParsedTarget != null)
            {
                OnCloseComponentEditor();
            }
            isEnable = false;
        }

        /// <summary>
        /// Called after parsing target and setting isEnable flag.
        /// </summary>
        protected virtual void OnInitializeEditor() { }
        /// <summary>
        /// Called at the hide component or closing inspector.
        /// </summary>
        protected virtual void OnCloseComponentEditor() { }
        /// <summary>
        /// Called on showing component in runtime.
        /// </summary>
        protected virtual void RuntimeDraw() { base.OnInspectorGUI(); }
        /// <summary>
        /// Called on showing component in edit mode.
        /// </summary>
        protected virtual void EditModeDraw() { base.OnInspectorGUI(); }
    }
    public abstract class SystemValueProviderEditor<TValueType, TSourceEnum> : EditModeOnlyEditor
        <SystemValueProvider<TValueType, TSourceEnum>> where TSourceEnum : Enum
    {
        protected sealed override void OnCloseComponentEditor()
        {
            EditorUtility.SetDirty(ParsedTarget);
        }
        protected sealed override void RuntimeDraw()
        {
            EditorGUILayout.TextField(ParsedTarget.SourceType.ToString(), ParsedTarget.Value.ToString());
        }
        protected sealed override void EditModeDraw()
        {
            ParsedTarget.SourceType = (TSourceEnum)EditorGUILayout.EnumPopup
                ("Value Source", ParsedTarget.SourceType);
        }
    }
    public abstract class CustomValueProviderEditor<TValueType> : EditModeOnlyEditor
        <CustomValueProvider<TValueType>>
    {
        protected sealed override void RuntimeDraw()
        {
            EditorGUILayout.TextField("Value name ", ParsedTarget.ValueName);
            EditorGUILayout.TextField("Value ", ParsedTarget.Value.ToString());
        }
        protected sealed override void EditModeDraw()
        {
            string oldStr = ParsedTarget.ValueName;
            ParsedTarget.ValueName = EditorGUILayout.TextField("Value name ", ParsedTarget.ValueName);
            if (ParsedTarget.ValueName != oldStr)
            {
                EditorUtility.SetDirty(ParsedTarget);
            }
        }
    }
    public abstract class TextDependenceEditor<TValueType> : EditModeOnlyEditor<TextDependence<TValueType>>
    {
        protected sealed override void OnCloseComponentEditor()
        {
            EditorUtility.SetDirty(ParsedTarget);
        }
        protected sealed override void EditModeDraw()
        {
            ParsedTarget.DrawInterface("Value Source " + typeof(TValueType).Name);
            ParsedTarget.ValueText = EditorGUILayout.ObjectField("Text Field", ParsedTarget.ValueText,
                typeof(Text), true) as Text;
        }
        protected sealed override void RuntimeDraw()
        {
            EditorDraws.ReadOnlyDrawInterface(ParsedTarget.ValueSource, "Value Source " +
                typeof(TValueType).Name);
            EditorGUILayout.TextField("Value ", ParsedTarget.ValueSource.Value.ToString());
            EditorGUILayout.ObjectField("Text Field", ParsedTarget.ValueText, typeof(Text), true);
        }
    }
    public abstract class PairMathOperationEditor<TValueType> :
        EditModeOnlyEditor<PairMathOperation<TValueType>> where TValueType : struct
    {
        private readonly static string firstLabel = "First operand " + typeof(TValueType);
        private readonly static string secondLabel = "Second operand " + typeof(TValueType);
        private readonly static string operationLabel = "Operation type ";
        protected abstract void DrawConstValue(TValueType oldValue,
            string label = "");
        protected sealed override void OnInitializeEditor()
        {
            if (ParsedTarget.serializationContainer == null)
            {
                ParsedTarget.serializationContainer = ParsedTarget.gameObject.AddComponent
                    <PairMathOpSerializationContainer>();
                EditorUtility.SetDirty(ParsedTarget.gameObject);
                EditorUtility.SetDirty(ParsedTarget);
                ParsedTarget.serializationContainer.Owner = ParsedTarget;
            }
        }
        protected sealed override void EditModeDraw()
        {
            if (EditorDraws.DrawInterfaceComponent<IGetterValue<TValueType>>
                (ref ParsedTarget.serializationContainer.FirstOperand, firstLabel) ||
                EditorDraws.DrawInterfaceComponent<IGetterValue<TValueType>>
                (ref ParsedTarget.serializationContainer.SecondOperand, secondLabel))
            {
                EditorUtility.SetDirty(ParsedTarget);
            }
            /*bool isChanged = false;
            ParsedTarget.OperationType = (int)(MathOperations.PairMathOperationType)EditorGUILayout.EnumPopup(
                operationLabel, (MathOperations.PairMathOperationType)ParsedTarget.OperationType);
            UnityEngine.Object component = ParsedTarget.serializationContainer.FirstOperand;
            component=EditorGUILayout.ObjectField(firstLabel,component, typeof(UnityEngine.Object),true);
            if (component != ParsedTarget.serializationContainer.FirstOperand)
            {
                if (component == null)
                {
                    ParsedTarget.serializationContainer.FirstOperand = null;
                }
                else if(component is Component)
                {
                    ParsedTarget.serializationContainer.FirstOperand = component as Component;
                }
                else
                {
                    ParsedTarget.serializationContainer.FirstOperand =
                        (component as GameObject).GetComponent<IGetterValue<TValueType>>() as Component;
                }
                isChanged = true;
            }
            component = ParsedTarget.serializationContainer.SecondOperand;
            component = EditorGUILayout.ObjectField(secondLabel, component, typeof(UnityEngine.Object), true);
            if (component != ParsedTarget.serializationContainer.SecondOperand)
            {
                if (component == null)
                {
                    ParsedTarget.serializationContainer.SecondOperand = null;
                }
                else if (component is Component)
                {
                    ParsedTarget.serializationContainer.SecondOperand = component as Component;
                }
                else
                {
                    ParsedTarget.serializationContainer.SecondOperand =
                        (component as GameObject).GetComponent<IGetterValue<TValueType>>() as Component;
                }
                isChanged = true;
            }*/
        }
        protected sealed override void RuntimeDraw()
        {
            EditorGUILayout.EnumPopup(operationLabel,
                (MathOperations.PairMathOperationType)ParsedTarget.OperationType);
            EditorGUILayout.ObjectField("First source ",
                ParsedTarget.ValueSource_1 as Component, typeof(Component), true);
            EditorGUILayout.ObjectField("Second source ",
                ParsedTarget.ValueSource_2 as Component, typeof(Component), true);
            DrawConstValue(ParsedTarget.ValueSource_1.Value, firstLabel);
            DrawConstValue(ParsedTarget.ValueSource_2.Value, secondLabel);
            EditorGUILayout.TextField("Output value " + ParsedTarget.Value.ToString());
        }
    }
    public abstract class ConverterEditor<TInput, TOutput> :
        EditModeOnlyEditor<ValueSources.Converter<TInput, TOutput>>
    {
        protected sealed override void OnCloseComponentEditor()
        {
            EditorUtility.SetDirty(ParsedTarget);
        }
        protected sealed override void EditModeDraw()
        {
            ParsedTarget.DrawInterface("Input source " + typeof(TInput).Name);
        }
        protected sealed override void RuntimeDraw()
        {
            ParsedTarget.ReadOnlyDrawInterface("Input source " + typeof(TInput).Name);
            EditorGUILayout.TextField("Input value " + ParsedTarget.ValueSource.Value.ToString());
            EditorGUILayout.TextField("Output value " + ParsedTarget.Value.ToString());
        }
    }
    public abstract class ArrayMathOperationsEditor<TValueType> : EditModeOnlyEditor
        <ArrayMathOperation<TValueType>> where TValueType : struct
    {
        protected sealed override void OnInitializeEditor()
        {
            if (ParsedTarget.SourcesComponents == null)
            {
                ParsedTarget.SourcesComponents = new Component[1];
            }
        }
        protected sealed override void EditModeDraw()
        {
            int oldType = ParsedTarget.OperationType;
            ParsedTarget.OperationType = (int)(MathOperations.ArrayMathOperationType)
                EditorGUILayout.EnumPopup("Operation type ",
                (MathOperations.ArrayMathOperationType)ParsedTarget.OperationType);
            SerializedObject serializedObj = new SerializedObject(ParsedTarget);
            EditorGUILayout.PropertyField(serializedObj.FindProperty("sourcesComponents"));
            serializedObj.ApplyModifiedProperties();
            if (oldType != ParsedTarget.OperationType)
            {
                EditorUtility.SetDirty(ParsedTarget);
            }
        }
        protected sealed override void RuntimeDraw()
        {
            EditorGUILayout.TextField("Operation type ",
                ((MathOperations.ArrayMathOperationType)ParsedTarget.OperationType).ToString());
            SerializedObject serializedObj = new SerializedObject(ParsedTarget);
            EditorGUILayout.PropertyField(serializedObj.FindProperty("sourcesComponents"));
        }
    }



    [UnityEditor.CustomEditor(typeof(SystemValueProvider_Float))]
    public sealed class SystemValueProviderEditor_Float : SystemValueProviderEditor<float,
        SystemTrackedValues.SourceType_Float>
    { }
    [UnityEditor.CustomEditor(typeof(SystemValueProvider_Int32))]
    public sealed class SystemValueProviderEditor_Int32 : SystemValueProviderEditor<int,
        SystemTrackedValues.SourceType_Int>
    { }
    [UnityEditor.CustomEditor(typeof(SystemValueProvider_String))]
    public sealed class SystemValueProviderEditor_String : SystemValueProviderEditor<string,
        SystemTrackedValues.SourceType_String>
    { }
    [UnityEditor.CustomEditor(typeof(SystemValueProvider_Bool))]
    public sealed class SystemValueProviderEditor_Bool : SystemValueProviderEditor<bool,
        SystemTrackedValues.SourceType_Bool>
    { }
    //TextDependence's editors
    [UnityEditor.CustomEditor(typeof(TextDependence_Float))]
    public sealed class TextDependenceEditor_Float : TextDependenceEditor<float> { }
    [UnityEditor.CustomEditor(typeof(TextDependence_Int32))]
    public sealed class TextDependenceEditor_Int32 : TextDependenceEditor<int> { }
    [UnityEditor.CustomEditor(typeof(TextDependence_String))]
    public sealed class TextDependenceEditor_String : TextDependenceEditor<string> { }
    [UnityEditor.CustomEditor(typeof(TextDependence_Bool))]
    public sealed class TextDependenceEditor_Bool : TextDependenceEditor<bool> { }
    //Math operations editors
    [UnityEditor.CustomEditor(typeof(PairMathOperation_Float))]
    public sealed class PairMathOperationEditor_Float : PairMathOperationEditor<float>
    {
        protected override void DrawConstValue(float oldValue, string label = "")
        {
            EditorGUILayout.FloatField(label, oldValue);
        }
    }
    [UnityEditor.CustomEditor(typeof(PairMathOperation_Int32))]
    public sealed class PairMathOperationEditor_Int32 : PairMathOperationEditor<int>
    {
        protected override void DrawConstValue(int oldValue, string label = "")
        {
            EditorGUILayout.IntField(label, oldValue);
        }
    }

    [UnityEditor.CustomEditor(typeof(ArrayMathOperation_Int32))]
    public sealed class ArrayMathOperationsEditor_Int32 : ArrayMathOperationsEditor<int>
    { }
    [UnityEditor.CustomEditor(typeof(ArrayMathOperation_Float))]
    public sealed class ArrayMathOperationsEditor_Float : ArrayMathOperationsEditor<float>
    { }
    //Converter's editors
    [UnityEditor.CustomEditor(typeof(Converter_FloatToInt32))]
    public sealed class ConverterEditor_FloatToInt32 : ConverterEditor<float, int> { }
    [UnityEditor.CustomEditor(typeof(Converter_FloatToString))]
    public sealed class ConverterEditor_FloatToString : ConverterEditor<float, string> { }
    [UnityEditor.CustomEditor(typeof(Converter_Int32ToFloat))]
    public sealed class ConverterEditor_Int32ToFloat : ConverterEditor<int, float> { }
    [UnityEditor.CustomEditor(typeof(Converter_Int32ToString))]
    public sealed class ConverterEditor_Int32ToString : ConverterEditor<int, string> { }
    [UnityEditor.CustomEditor(typeof(Converter_StringToFloat))]
    public sealed class ConverterEditor_StringToFloat : ConverterEditor<string, float> { }
    [UnityEditor.CustomEditor(typeof(Converter_StringToInt32))]
    public sealed class ConverterEditor_StringToInt32 : ConverterEditor<string, int> { }
    //CustomValues editor
    [UnityEditor.CustomEditor(typeof(CustomValueProvider_Float))]
    public sealed class CustomValueProviderEditor_Float : CustomValueProviderEditor<float>
    { }
    [UnityEditor.CustomEditor(typeof(CustomValueProvider_Int32))]
    public sealed class CustomValueProviderEditor_Int32 : CustomValueProviderEditor<int>
    { }
    [UnityEditor.CustomEditor(typeof(CustomValueProvider_String))]
    public sealed class CustomValueProviderEditor_String : CustomValueProviderEditor<string>
    { }
    [UnityEditor.CustomEditor(typeof(CustomValueProvider_Bool))]
    public sealed class CustomValueProviderEditor_Bool : CustomValueProviderEditor<bool>
    { }
}
