using System.Collections.Generic;
using Utility;

namespace UI
{
    public class UILayer
    {
        public string Name { get; }

        private readonly List<Focusable> _focusables = new();

        public IReadOnlyList<Focusable> Focusables => _focusables;

        public int CurrentIndex { get; private set; }

        public Focusable Current =>
            _focusables.Count == 0
                ? null
                : _focusables[CurrentIndex];

        public UILayer(string name)
        {
            Name = name;
        }

        public void Add(Focusable focusable)
        {
            _focusables.Add(focusable);
        }

        public void Clear()
        {
            _focusables.Clear();
            CurrentIndex = 0;
        }

        public Focusable Next()
        {
            if (_focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex = (CurrentIndex + 1) % _focusables.Count;

            Current?.Focus();

            return Current;
        }

        public Focusable Previous()
        {
            if (_focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex =
                (CurrentIndex - 1 + _focusables.Count)
                % _focusables.Count;

            Current?.Focus();

            return Current;
        }

        public Focusable FocusFirst()
        {
            if (_focusables.Count == 0)
                return null;

            CurrentIndex = 0;

            Current.Focus();

            return Current;
        }
        
        public void SelectCurrent()
        {
            Current?.ConfirmAction();
        }

        public void Select(int index)
        {
            if (index < 0 || index >= _focusables.Count)
                return;

            _focusables[index].ConfirmAction();
        }
    }
}