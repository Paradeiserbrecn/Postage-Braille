using System;
using System.Collections.Generic;
using Utility;

namespace UI
{
    [Serializable]
    public class UILayer
    {
        public string Name { get; }

        public List<Focusable> Focusables { get; } = new();

        public int CurrentIndex { get; private set; }

        public Focusable Current =>
            Focusables.Count == 0
                ? null
                : Focusables[CurrentIndex];

        public UILayer(string name)
        {
            Name = name;
        }

        public void Add(Focusable focusable)
        {
            Focusables.Add(focusable);
        }

        public void Clear()
        {
            Focusables.Clear();
            CurrentIndex = 0;
        }

        public Focusable FocusNext()
        {
            if (Focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex = (CurrentIndex + 1) % Focusables.Count;

            Current?.Focus();

            return Current;
        }

        public Focusable FocusPrevious()
        {
            if (Focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex =
                (CurrentIndex - 1 + Focusables.Count)
                % Focusables.Count;

            Current?.Focus();

            return Current;
        }

        public Focusable FocusFirst()
        {
            if (Focusables.Count == 0)
                return null;

            CurrentIndex = 0;

            Current.Focus();

            return Current;
        }

        public void Unfocus()
        {
            CurrentIndex = 0;
            Current?.Unfocus();
        }
        
        public void ConfirmCurrent()
        {
            Current?.ConfirmAction();
        }

        public void Confirm(int index)
        {
            if (index < 0 || index >= Focusables.Count)
                return;

            Focusables[index].ConfirmAction();
        }
    }
}