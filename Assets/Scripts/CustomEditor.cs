
using UnityEngine;
using UnityEditor;
using MuonhoryoLibrary.UnityEditor;
using HistSi.GUI;
using HistSi.ValueSources;

namespace HistSi.CustomEditor 
{
    [UnityEditor.CustomEditor(typeof(HistSiSlider))]
    public sealed class HistSiSliderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
    [UnityEditor.CustomEditor(typeof(HistSiInitialization))]
    public sealed class HistsiInitializationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            HistSiInitialization parsedTarget = target as HistSiInitialization;
            if (EditorUtility.IsDirty(parsedTarget))
            {
                parsedTarget.OnAfterDeserialize();
            }
        }
    }
    [UnityEditor.CustomEditor(typeof(CustomValuesData))]
    public sealed class CustomValuesEditor : Editor, IDictionaryEditorDrawHelper<string, TrackedValue<int>>,
        IDictionaryEditorDrawHelper<string, TrackedValue<float>>,
        IDictionaryEditorDrawHelper<string, TrackedValue<bool>>,
        IDictionaryEditorDrawHelper<string, TrackedValue<string>>
    {
        private CustomValuesData ValueSource;
        private void OnEnable()
        {
            ValueSource = target as CustomValuesData;
        }
        public bool isShowing_Int32 = false;
        public bool isShowing_Float = false;
        public bool isShowing_Bool = false;
        public bool isShowing_String = false;
        bool IDictionaryEditorDrawHelper<string, TrackedValue<int>>.isShowingList
        {
            get => isShowing_Int32;
            set => isShowing_Int32 = value;
        }
        bool IDictionaryEditorDrawHelper<string, TrackedValue<float>>.isShowingList
        {
            get => isShowing_Float;
            set => isShowing_Float = value;
        }
        bool IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.isShowingList
        {
            get => isShowing_Bool;
            set => isShowing_Bool = value;
        }
        bool IDictionaryEditorDrawHelper<string, TrackedValue<string>>.isShowingList
        {
            get => isShowing_String;
            set => isShowing_String = value;
        }
        string IDictionaryEditorDrawHelper<string, TrackedValue<int>>.SerializationPath =>
            CustomValuesData.serializationPath_Int32;
        string IDictionaryEditorDrawHelper<string, TrackedValue<float>>.SerializationPath =>
            CustomValuesData.serializationPath_Float;
        string IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.SerializationPath =>
            CustomValuesData.serializationPath_Bool;
        string IDictionaryEditorDrawHelper<string, TrackedValue<string>>.SerializationPath =>
            CustomValuesData.serializationPath_String;
        string IDictionaryEditorDrawHelper<string, TrackedValue<int>>.NewKey
        {
            get => ValueSource.NewKey_Int32;
            set => ValueSource.NewKey_Int32 = value;
        }
        string IDictionaryEditorDrawHelper<string, TrackedValue<float>>.NewKey
        {
            get => ValueSource.NewKey_Float;
            set => ValueSource.NewKey_Float = value;
        }
        string IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.NewKey
        {
            get => ValueSource.NewKey_Bool;
            set => ValueSource.NewKey_Bool = value;
        }
        string IDictionaryEditorDrawHelper<string, TrackedValue<string>>.NewKey
        {
            get => ValueSource.NewKey_String;
            set => ValueSource.NewKey_String = value;
        }
        TrackedValue<int> IDictionaryEditorDrawHelper<string, TrackedValue<int>>.CurrentValue
        {
            get => ValueSource.CurrentValue_Int32;
            set => ValueSource.CurrentValue_Int32 = value;
        }
        TrackedValue<float> IDictionaryEditorDrawHelper<string, TrackedValue<float>>.CurrentValue
        {
            get => ValueSource.CurrentValue_Float;
            set => ValueSource.CurrentValue_Float = value;
        }
        TrackedValue<bool> IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.CurrentValue
        {
            get => ValueSource.CurrentValue_Bool;
            set => ValueSource.CurrentValue_Bool = value;
        }
        TrackedValue<string> IDictionaryEditorDrawHelper<string, TrackedValue<string>>.CurrentValue
        {
            get => ValueSource.CurrentValue_String;
            set => ValueSource.CurrentValue_String = value;
        }
        private int currentEditIndex_Int32;
        private int currentEditIndex_Float;
        private int currentEditIndex_Bool;
        private int currentEditIndex_String;
        int IDictionaryEditorDrawHelper<string, TrackedValue<int>>.CurrentEditIndex
        {
            get => currentEditIndex_Int32;
            set => currentEditIndex_Int32 = value;
        }
        int IDictionaryEditorDrawHelper<string, TrackedValue<float>>.CurrentEditIndex
        {
            get => currentEditIndex_Float;
            set => currentEditIndex_Float = value;
        }
        int IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.CurrentEditIndex
        {
            get => currentEditIndex_Bool;
            set => currentEditIndex_Bool = value;
        }
        int IDictionaryEditorDrawHelper<string, TrackedValue<string>>.CurrentEditIndex
        {
            get => currentEditIndex_String;
            set => currentEditIndex_String = value;
        }
        string IDictionaryEditorDrawHelper<string, TrackedValue<int>>.NewKeyPropertyName
        { get => "NewKey_Int32"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<float>>.NewKeyPropertyName
        { get => "NewKey_Float"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.NewKeyPropertyName
        { get => "NewKey_Bool"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<string>>.NewKeyPropertyName
        { get => "NewKey_String"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<int>>.CurrentValuePropertyName
        { get => "CurrentValue_Int32"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<float>>.CurrentValuePropertyName
        { get => "CurrentValue_Float"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<bool>>.CurrentValuePropertyName
        { get => "CurrentValue_Bool"; }
        string IDictionaryEditorDrawHelper<string, TrackedValue<string>>.CurrentValuePropertyName
        { get => "CurrentValue_String"; }
        public override void OnInspectorGUI()
        {
            CustomValuesData obj = target as CustomValuesData;
            SerializedObject serializedObj = new SerializedObject(obj);
            string text = "CustomValues_";
            if (Application.isPlaying)
            {
                EditorDraws.ReadOnlyDrawDictionary(obj.CustomValues_Int32, serializedObj, this,
                    CustomValuesData.CustomVariableSerializator<string, int>.Instance,
                    text + typeof(int).Name);

                EditorDraws.ReadOnlyDrawDictionary(obj.CustomValues_Float, serializedObj, this,
                    CustomValuesData.CustomVariableSerializator<string, float>.Instance,
                    text + "Float");

                EditorDraws.ReadOnlyDrawDictionary(obj.CustomValues_Bool, serializedObj, this,
                    CustomValuesData.CustomVariableSerializator<string, bool>.Instance,
                    text + typeof(bool).Name);

                EditorDraws.ReadOnlyDrawDictionary(obj.CustomValues_String, serializedObj, this,
                    CustomValuesData.CustomVariableSerializator<string, string>.Instance,
                    text + typeof(string).Name);
            }
            else
            {
                string removeText = "Remove";
                string newElementText = "New element's key";
                string addText = "Add";

                EditorDraws.DrawDictionary(obj.CustomValues_Int32, serializedObj, this,
                    new CustomValuesData.CustomVariableSerializator<string, int>(),
                    inspectorLabelText: text + typeof(int).Name,
                    removeButtonText: removeText,
                    newElementKeyText: newElementText,
                    addButtonText: addText);

                EditorDraws.DrawDictionary(obj.CustomValues_Float, serializedObj, this,
                    new CustomValuesData.CustomVariableSerializator<string, float>(),
                    inspectorLabelText: text + "Float",
                    removeButtonText: removeText,
                    newElementKeyText: newElementText,
                    addButtonText: addText);

                EditorDraws.DrawDictionary(obj.CustomValues_Bool, serializedObj, this,
                    new CustomValuesData.CustomVariableSerializator<string, bool>(),
                    inspectorLabelText: text + typeof(bool).Name,
                    removeButtonText: removeText,
                    newElementKeyText: newElementText,
                    addButtonText: addText);

                EditorDraws.DrawDictionary(obj.CustomValues_String, serializedObj, this,
                    new CustomValuesData.CustomVariableSerializator<string, string>(),
                    inspectorLabelText: text + typeof(string).Name,
                    removeButtonText: removeText,
                    newElementKeyText: newElementText,
                    addButtonText: addText);
            }
        }
    }
}
