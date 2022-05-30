
using System.Collections.Generic;

namespace HistSi.GUI
{
    public static class ButtonsLocker
    {
        private readonly static List<byte> LockLayers = new List<byte> { };
        private readonly static Dictionary<byte, byte> LockersCount = new Dictionary<byte, byte> { };
        private readonly static Dictionary<byte, List<LockableButton>> Buttons =
            new Dictionary<byte, List<LockableButton>> { };
        public static bool IsExistLayer(byte layer)
        {
            return Buttons.ContainsKey(layer);
        }
        public static List<byte> GetLockLayers()
        {
            List<byte> list = new List<byte> { };
            list.AddRange(LockLayers);
            return list;
        }
        public static byte GetLockerCount(byte layer)
        {
            return LockersCount[layer];
        }
        public static List<LockableButton> GetButtons(byte layer)
        {
            List<LockableButton> buttons = new List<LockableButton> { };
            buttons.AddRange(Buttons[layer]);
            return buttons;
        }
        public static void AddButton(LockableButton button)
        {
            void CreateNewLayer(byte lockLayer)
            {
                LockersCount.Add(lockLayer, 0);
                Buttons.Add(lockLayer, new List<LockableButton> { });
                LockLayers.Add(lockLayer);
            }
            if (!Buttons.ContainsKey(button.LockLayer))
            {
                CreateNewLayer(button.LockLayer);
            }
            Buttons[button.LockLayer].Add(button);
        }
        public static void RemoveButton(LockableButton button)
        {
            if (Buttons.ContainsKey(button.LockLayer) && Buttons[button.LockLayer].Contains(button))
            {
                Buttons[button.LockLayer].Remove(button);
            }
        }
        public static void IncrementLockerCount(byte layer)
        {
            if (LockersCount[layer] == byte.MaxValue)
            {
                throw new HistSiException("To many lockers on layer:" + layer);
            }
            else
            {
                LockersCount[layer]++;
            }
        }
        public static void DecrementLockerCount(byte layer)
        {
            if (LockersCount[layer] == 0)
            {
                throw new HistSiException("Main menu buttons was not locked");
            }
            else
            {
                LockersCount[layer]--;
            }
        }
        public static void LockButtons(byte layer)
        {
            if (IsExistLayer(layer))
            {
                if (GetLockerCount(layer) == 0)
                {
                    foreach (LockableButton button in GetButtons(layer)) button.DeactivateButton();
                }
                IncrementLockerCount(layer);
            }
            else
            {
                throw new HistSiException("Lock layer at index " + layer + " does not exist");
            }
        }
        public static void UnlockButtons(byte layer)
        {
            if (GetLockerCount(layer) == 1)
            {
                foreach (LockableButton button in GetButtons(layer)) button.ActivateButton();
            }
            DecrementLockerCount(layer);
        }
    }
}
