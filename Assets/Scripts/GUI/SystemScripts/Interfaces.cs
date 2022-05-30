
using System;
using System.Collections;
using UnityEngine;

namespace HistSi.GUI
{
    /// <summary>
    /// Tag of Command. After execution this command in default realization
    /// ICommandRuner he is stops execution of commands in his commands list.
    /// </summary>
    public interface IFinalCommand { }
    /// <summary>
    /// Execute command or commands list.
    /// </summary>
    public interface ICommandRuner
    {
        /// <summary>
        /// Execute commandsList.
        /// </summary>
        /// <param name="commandsList"></param>
        void RunCommandList(ButtonCommandsQueue commandsList);
        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="command"></param>
        void RunCommand(Commands.ButtonCommand command);
    }
    public interface IButtonLocker
    {
        byte LockedLayer { get; }
    }
    public interface IRemovable
    {
        Animation OnDestroyAnimation { get; }
        GameObject DestroyedObject { get; }
        void Remove();
        Coroutine StartCoroutine(IEnumerator routine);
    }
}
namespace HistSi.ValueSources
{
    public interface IValueListener<T>
    {
        IGetterValue<T> ValueSource { get; }
        void OnValueChanged();
    }
    public interface IValueListener<T1, T2>
    {
        IGetterValue<T1> ValueSource_1 { get; }
        IGetterValue<T2> ValueSource_2 { get; }
        void OnValueChanged();
    }
    public interface IArrayValueListener<T>
    {
        IGetterValue<T>[] ValueSources { get; }
        void OnValueChanged();
    }
    public interface IGetterValue<T>
    {
        event Action ValueChangeEvent;
        T Value { get; }
    }
    public interface ISetterValue<T>
    {
        T Value {set; }
    }
    public interface IValueField<T> : IGetterValue<T>, ISetterValue<T> { }
}
