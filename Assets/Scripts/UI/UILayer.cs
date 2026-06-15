using System.Collections.Generic;
using Utility;

namespace UI
{
    public class UILayer
    {
        public string Name { get; }

        private readonly List<IFocusable> _focusables = new();

        public IReadOnlyList<IFocusable> Focusables => _focusables;

        public int CurrentIndex { get; private set; }

        public IFocusable Current =>
            _focusables.Count == 0
                ? null
                : _focusables[CurrentIndex];

        public UILayer(string name)
        {
            Name = name;
        }

        public void Add(IFocusable focusable)
        {
            _focusables.Add(focusable);
        }

        public void Clear()
        {
            _focusables.Clear();
            CurrentIndex = 0;
        }

        public IFocusable Next()
        {
            if (_focusables.Count == 0)
                return null;

            Current?.Unfocus();

            CurrentIndex = (CurrentIndex + 1) % _focusables.Count;

            Current?.Focus();

            return Current;
        }

        public IFocusable Previous()
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

        public IFocusable FocusFirst()
        {
            if (_focusables.Count == 0)
                return null;

            CurrentIndex = 0;

            Current.Focus();

            return Current;
        }
    }
}