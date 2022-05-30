
using UnityEngine;

namespace HistSi.GUI.Commands
{
    public sealed class ThrowExceptionCommand : ButtonCommand
    {
        [SerializeField]
        private string ExceptionMessage;
        public sealed override void CommandRun()
        {
            throw new HistSiException(ExceptionMessage);
        }
    }
}
